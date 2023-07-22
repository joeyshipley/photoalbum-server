using Application.Infrastructure.External;
using FluentAssertions;
using Tests.Infrastructure;

namespace Tests.Application;

public class UrlProviderTests
    : UnitTestOf<UrlProvider>
{
    [Test]
    public void UserAlbumsUrl_WhenAllIsWell()
    {
        // Act
        var result = UnderTest.UserManyAlbumsUrl(1001);

        // Assert
        result.Should().Be("https://jsonplaceholder.typicode.com/users/1001/albums");
    }

    [Test]
    public void AlbumManyPhotosUrl_WhenAllIsWell()
    {
        // Act
        var result = UnderTest.AlbumManyPhotosUrl(1001);

        // Assert
        result.Should().Be("https://jsonplaceholder.typicode.com/albums/1001/photos");
    }

    [Test]
    public void AlbumUrl_WhenAllIsWell()
    {
        // Act
        var result = UnderTest.AlbumSingleUrl(1001);

        // Assert
        result.Should().Be("https://jsonplaceholder.typicode.com/albums/1001");
    }

    [Test]
    public void PhotoUrl_WhenAllIsWell()
    {
        // Act
        var result = UnderTest.PhotoSingleUrl(1001);

        // Assert
        result.Should().Be("https://jsonplaceholder.typicode.com/photos/1001");
    }
}