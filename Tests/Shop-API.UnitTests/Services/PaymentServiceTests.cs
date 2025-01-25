using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using FakeItEasy;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace PaymentServiceTest.Unit.Tests;

public class PaymentServiceTests
{
    private readonly ICartService _fakeCartService;
    private readonly IUnitOfWork _fakeUnitOfWork;
    private readonly IConfiguration _fakeConfig;
    private readonly PaymentService _paymentService;

    public PaymentServiceTests()
    {
        _fakeCartService = A.Fake<ICartService>();
        _fakeUnitOfWork = A.Fake<IUnitOfWork>();
        _fakeConfig = A.Fake<IConfiguration>();

        A.CallTo(() => _fakeConfig["StripeSettings:SecretKey"]).Returns("test_secret_key");

        _paymentService = new PaymentService(_fakeConfig, _fakeCartService, _fakeUnitOfWork);
    }

    [Fact]
    public async Task CreateOrUpdatePaymentIntent_ValidCart_ReturnsUpdatedCart()
    {
        // Arrange
        var cartId = "testCartId";
        var cart = new ShoppingCart
        {
            Id = cartId,
            Items = new List<CartItem>
            {
                new CartItem { 
                  ProductId = 1, 
                  Quantity = 2, 
                  Price = 50,
                  ProductName = "",
                  PictureUrl = "",
                  Brand = "",
                  Type = ""   
                }
            },
            DeliveryMethodId = 1
        };

        var deliveryMethod = new DeliveryMethod
        {
            Price = 10m,
            ShortName = "ShortNameValue",
            DeliveryTime = "DeliveryTimeValue",
            Description = "DescriptionValue"
        };

       var product = new Product { 
          Id = 1, 
          Name = "ProductA",
          Description = "ProductA",
          PictureUrl = "ProductA",
          Type = "ProductA",
          Brand = "ProductA"
        };

        A.CallTo(() => _fakeCartService.GetCartAsync(cartId)).Returns(cart);
        A.CallTo(() => _fakeUnitOfWork.Repository<DeliveryMethod>().GetByIdAsync(1)).Returns(deliveryMethod);
        A.CallTo(() => _fakeUnitOfWork.Repository<Product>().GetByIdAsync(1)).Returns(product);

        // Act
        var result = await _paymentService.CreateOrUpdatePaymentIntent(cartId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cartId, result.Id);
        A.CallTo(() => _fakeCartService.SetCartAsync(cart)).MustHaveHappenedOnceExactly();
    }

}
