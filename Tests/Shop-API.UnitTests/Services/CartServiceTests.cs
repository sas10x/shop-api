using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Services;
using StackExchange.Redis;
using FakeItEasy;
using Xunit;

namespace CartServiceTest.Unit.Tests;

public class CartServiceTests
{
    private readonly IConnectionMultiplexer _mockRedis;
    private readonly IDatabase _mockDatabase;
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        // Mock dependencies
        _mockRedis = A.Fake<IConnectionMultiplexer>();
        _mockDatabase = A.Fake<IDatabase>();

        // Set up the mocked IConnectionMultiplexer to return the mocked IDatabase
        A.CallTo(() => _mockRedis.GetDatabase(A<int>.Ignored, A<object>.Ignored))
            .Returns(_mockDatabase);

        // Initialize the service
        _cartService = new CartService(_mockRedis);
    }

    [Fact]
    public async Task DeleteCartAsync_ShouldReturnTrue_WhenKeyIsDeleted()
    {
        // Arrange
        string key = "cart123";
        A.CallTo(() => _mockDatabase.KeyDeleteAsync(key, CommandFlags.None))
            .Returns(true);

        // Act
        var result = await _cartService.DeleteCartAsync(key);

        // Assert
        Assert.True(result);
        A.CallTo(() => _mockDatabase.KeyDeleteAsync(key, CommandFlags.None)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteCartAsync_ShouldReturnFalse_WhenKeyIsNotDeleted()
    {
        // Arrange
        string key = "cart123";
        A.CallTo(() => _mockDatabase.KeyDeleteAsync(key, CommandFlags.None))
            .Returns(false);

        // Act
        var result = await _cartService.DeleteCartAsync(key);

        // Assert
        Assert.False(result);
        A.CallTo(() => _mockDatabase.KeyDeleteAsync(key, CommandFlags.None)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetCartAsync_ShouldReturnCart_WhenDataExists()
    {
        // Arrange
        string key = "cart123";
        var shoppingCart = new ShoppingCart { Id = key };
        string serializedCart = JsonSerializer.Serialize(shoppingCart);

        A.CallTo(() => _mockDatabase.StringGetAsync(key, CommandFlags.None))
            .Returns(serializedCart);

        // Act
        var result = await _cartService.GetCartAsync(key);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(key, result.Id);
        A.CallTo(() => _mockDatabase.StringGetAsync(key, CommandFlags.None)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetCartAsync_ShouldReturnNull_WhenDataDoesNotExist()
    {
        // Arrange
        string key = "cart123";
        A.CallTo(() => _mockDatabase.StringGetAsync(key, CommandFlags.None))
            .Returns(RedisValue.Null);

        // Act
        var result = await _cartService.GetCartAsync(key);

        // Assert
        Assert.Null(result);
        A.CallTo(() => _mockDatabase.StringGetAsync(key, CommandFlags.None)).MustHaveHappenedOnceExactly();
    }

    // [Fact]
    // public async Task SetCartAsync_ShouldReturnCart_WhenSetIsSuccessful()
    // {
    //     // Arrange
    //     var shoppingCart = new ShoppingCart { Id = "cart123" };
    //     string serializedCart = JsonSerializer.Serialize(shoppingCart);

    //     A.CallTo(() => _mockDatabase.StringSetAsync(
    //             shoppingCart.Id, serializedCart, TimeSpan.FromDays(30), When.Always, CommandFlags.None))
    //         .Returns(true);

    //     A.CallTo(() => _mockDatabase.StringGetAsync(shoppingCart.Id, CommandFlags.None))
    //         .Returns(serializedCart);

    //     // Act
    //     var result = await _cartService.SetCartAsync(shoppingCart);

    //     // Assert
    //     Assert.NotNull(result);
    //     Assert.Equal(shoppingCart.Id, result.Id);

    //     A.CallTo(() => _mockDatabase.StringSetAsync(
    //         shoppingCart.Id, serializedCart, TimeSpan.FromDays(30), When.Always, CommandFlags.None))
    //         .MustHaveHappenedOnceExactly();

    //     A.CallTo(() => _mockDatabase.StringGetAsync(shoppingCart.Id, CommandFlags.None))
    //         .MustHaveHappenedOnceExactly();
    // }

    // [Fact]
    // public async Task SetCartAsync_ShouldReturnNull_WhenSetFails()
    // {
    //     // Arrange
    //     var shoppingCart = new ShoppingCart { Id = "cart123" };

    //     A.CallTo(() => _mockDatabase.StringSetAsync(
    //             shoppingCart.Id, A<string>.Ignored, TimeSpan.FromDays(30), When.Always, CommandFlags.None))
    //         .Returns(false);

    //     // Act
    //     var result = await _cartService.SetCartAsync(shoppingCart);

    //     // Assert
    //     Assert.Null(result);

    //     A.CallTo(() => _mockDatabase.StringSetAsync(
    //         shoppingCart.Id, A<string>.Ignored, TimeSpan.FromDays(30), When.Always, CommandFlags.None))
    //         .MustHaveHappenedOnceExactly();
    // }
}
