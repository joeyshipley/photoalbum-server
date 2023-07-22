using Application.Albums;
using Application.Albums.Viewer;
using Application.Albums.Viewer.RequestsResults;
using Application.Infrastructure.External;
using FluentAssertions;
using Moq;
using Tests.Infrastructure.TestBases;

namespace Tests.Application.Albums;

public class AlbumViewerServiceTests
    : UnitTestOf<AlbumViewerService>
{
    [Test]
    public async Task ViewAll_WhenAllIsWell()
    {
        // Arrange
        var apiCallerMock = Mocker.GetMock<IApiCaller>();
        apiCallerMock
            .Setup(x => x.GetAsync<List<AlbumEntry>>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<List<AlbumEntry>>
            {
                Model = new List<AlbumEntry> { new AlbumEntry { Id = 1001 } },
            });
        var request = new AlbumViewerCollectionRequest();

        // Act
        var result = await UnderTest.ViewAll(request);

        // Assert
        result.Albums.Count.Should().Be(1);
    }
    
    [Test]
    public async Task ViewAll_WhenInvalidRequest()
    {
        // Arrange
        var request = new AlbumViewerCollectionRequest { UserId = 0 };

        // Act
        var result = await UnderTest.ViewAll(request);

        // Assert
        result.Albums.Count.Should().Be(0);
        result.Errors.Count.Should().Be(1);
        result.Errors.Any(x => x.Key == "INVALID_USER_ID").Should().BeTrue("INVALID_USER_ID error was not found.");
    }
        
    [Test]
    public async Task ViewAll_WhenErrorReturnedFromApi()
    {
        // Arrange
        var apiCallerMock = Mocker.GetMock<IApiCaller>();
        apiCallerMock
            .Setup(x => x.GetAsync<List<AlbumEntry>>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<List<AlbumEntry>>
            {
                Errors = new List<string> { "Nope!" }
            });
        var request = new AlbumViewerCollectionRequest();

        // Act
        var result = await UnderTest.ViewAll(request);

        // Assert
        result.Albums.Count.Should().Be(0);
        result.Errors.Count.Should().Be(1);
        result.Errors.Any(x => x.Key == "API_FAILURE").Should().BeTrue("API_FAILURE error was not found.");
    }

    [Test]
    public async Task View_WhenAllIsWell()
    {
        // Arrange
        var albumId = 1001;
        var apiCallerMock = Mocker.GetMock<IApiCaller>();
        apiCallerMock
            .Setup(x => x.GetAsync<AlbumEntry>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<AlbumEntry>
            {
                Model = new AlbumEntry { Id = albumId },
            });

        var request = new AlbumViewerRequest { AlbumId = albumId };

        // Act
        var result = await UnderTest.View(request);

        // Assert
        result.Album.Id.Should().Be(albumId);
    }

    [Test]
    public async Task View_WhenInvalidRequest()
    {
        // Arrange
        var request = new AlbumViewerRequest { AlbumId = 0 };

        // Act
        var result = await UnderTest.View(request);

        // Assert
        result.Album.Should().BeNull();
        result.Errors.Count.Should().Be(1);
        result.Errors.Any(x => x.Key == "INVALID_ALBUM_ID").Should().BeTrue("INVALID_ALBUM_ID error was not found.");
    }

    [Test]
    public async Task View_WhenErrorReturnedFromApi()
    {
        // Arrange
        var albumId = 1001;
        var apiCallerMock = Mocker.GetMock<IApiCaller>();
        apiCallerMock
            .Setup(x => x.GetAsync<AlbumEntry>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<AlbumEntry>
            {
                Errors = new List<string> { "Nope!" }
            });

        var request = new AlbumViewerRequest { AlbumId = albumId };

        // Act
        var result = await UnderTest.View(request);

        // Assert
        result.Album.Should().BeNull();
        result.Errors.Count.Should().Be(1);
        result.Errors.Any(x => x.Key == "API_FAILURE").Should().BeTrue("API_FAILURE error was not found.");
    }
}