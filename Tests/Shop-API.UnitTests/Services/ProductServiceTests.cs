using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using FakeItEasy;
using Infrastructure.Services;
using Xunit;

namespace ProductServiceTest.Unit.Tests;
public class ProductServiceTests
{
    private readonly IProductRepository _mockRepository;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        // Create a mock repository
        _mockRepository = A.Fake<IProductRepository>();
        _productService = new ProductService(_mockRepository);
    }

    [Fact]
    public async Task GetBrandsAsync_ShouldCallRepositoryMethod_AndReturnBrands()
    {
        // Arrange
        var brands = new List<string> { "BrandA", "BrandB" };
        A.CallTo(() => _mockRepository.GetBrandsAsync()).Returns(Task.FromResult((IReadOnlyList<string>)brands));

        // Act
        var result = await _productService.GetBrandsAsync();

        // Assert
        A.CallTo(() => _mockRepository.GetBrandsAsync()).MustHaveHappenedOnceExactly();
        Assert.Equal(brands, result);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldCallRepositoryMethod_AndReturnProduct()
    {
        // Arrange
        var product = new Product { 
          Id = 1, 
          Name = "ProductA",
          Description = "ProductA",
          PictureUrl = "ProductA",
          Type = "ProductA",
          Brand = "ProductA"
        };
        A.CallTo(() => _mockRepository.GetProductByIdAsync(1)).Returns(Task.FromResult(product));

        // Act
        var result = await _productService.GetProductByIdAsync(1);

        // Assert
        A.CallTo(() => _mockRepository.GetProductByIdAsync(1)).MustHaveHappenedOnceExactly();
        Assert.Equal(product, result);
    }

    [Fact]
    public async Task GetProductsAsync_ShouldCallRepositoryMethod_AndReturnProducts()
    {
        // Arrange
        var product = new Product { 
          Id = 1, 
          Name = "ProductA",
          Description = "ProductA",
          PictureUrl = "ProductA",
          Type = "ProductA",
          Brand = "ProductA"
        };
        var products = new List<Product> { product };
        A.CallTo(() => _mockRepository.GetProductsAsync(null, null, null)).Returns(Task.FromResult((IReadOnlyList<Product>)products));

        // Act
        var result = await _productService.GetProductsAsync(null, null, null);

        // Assert
        A.CallTo(() => _mockRepository.GetProductsAsync(null, null, null)).MustHaveHappenedOnceExactly();
        Assert.Equal(products, result);
    }

    [Fact]
    public async Task GetTypesAsync_ShouldCallRepositoryMethod_AndReturnTypes()
    {
        // Arrange
       IReadOnlyList<string> types = new[] { "Hat" };

        A.CallTo(() => _mockRepository.GetTypesAsync()).Returns(Task.FromResult((IReadOnlyList<string>)types));

        // Act
        var result = await _productService.GetTypesAsync();

        // Assert
        A.CallTo(() => _mockRepository.GetTypesAsync()).MustHaveHappenedOnceExactly();
        Assert.Equal(types, result);
    }

    [Fact]
    public void AddProduct_ShouldCallRepositoryMethod()
    {
        // Arrange
        var product = new Product { 
          Id = 1, 
          Name = "ProductA",
          Description = "ProductA",
          PictureUrl = "ProductA",
          Type = "ProductA",
          Brand = "ProductA"
        };

        // Act
        _productService.AddProduct(product);

        // Assert
        A.CallTo(() => _mockRepository.AddProduct(product)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void DeleteProduct_ShouldCallRepositoryMethod()
    {
        // Arrange
       var product = new Product { 
          Id = 1, 
          Name = "ProductA",
          Description = "ProductA",
          PictureUrl = "ProductA",
          Type = "ProductA",
          Brand = "ProductA"
        };

        // Act
        _productService.DeleteProduct(product);

        // Assert
        A.CallTo(() => _mockRepository.DeleteProduct(product)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldCallRepositoryMethod_AndReturnTrue()
    {
        // Arrange
        A.CallTo(() => _mockRepository.SaveChangesAsync()).Returns(Task.FromResult(true));

        // Act
        var result = await _productService.SaveChangesAsync();

        // Assert
        A.CallTo(() => _mockRepository.SaveChangesAsync()).MustHaveHappenedOnceExactly();
        Assert.True(result);
    }

    [Fact]
    public void ProductExists_ShouldCallRepositoryMethod_AndReturnTrue()
    {
        // Arrange
        A.CallTo(() => _mockRepository.ProductExists(1)).Returns(true);

        // Act
        var result = _productService.ProductExists(1);

        // Assert
        A.CallTo(() => _mockRepository.ProductExists(1)).MustHaveHappenedOnceExactly();
        Assert.True(result);
    }

    [Fact]
    public void UpdateProduct_ShouldCallRepositoryMethod()
    {
        // Arrange
        var product = new Product { 
          Id = 1, 
          Name = "ProductA",
          Description = "ProductA",
          PictureUrl = "ProductA",
          Type = "ProductA",
          Brand = "ProductA"
        };

        // Act
        _productService.UpdateProduct(product);

        // Assert
        A.CallTo(() => _mockRepository.UpdateProduct(product)).MustHaveHappenedOnceExactly();
    }
}
