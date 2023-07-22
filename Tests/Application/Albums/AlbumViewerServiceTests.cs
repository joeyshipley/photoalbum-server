using Application.Albums.External;
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
            .Setup(x => x.GetAsync<List<AlbumExternalSourceDto>>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<List<AlbumExternalSourceDto>>
            {
                Model = new List<AlbumExternalSourceDto> { new AlbumExternalSourceDto { Id = 1001 } },
            });
        var request = new AlbumViewerCollectionRequest();

        // Act
        var result = await UnderTest.ViewAll(request);

        // Assert
        result.Albums.Count.Should().Be(1);
        result.Errors.Should().BeEmpty();
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
            .Setup(x => x.GetAsync<List<AlbumExternalSourceDto>>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<List<AlbumExternalSourceDto>>
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
            .Setup(x => x.GetAsync<AlbumExternalSourceDto>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<AlbumExternalSourceDto>
            {
                Model = new AlbumExternalSourceDto { Id = albumId },
            });

        var request = new AlbumViewerRequest { AlbumId = albumId };

        // Act
        var result = await UnderTest.View(request);

        // Assert
        result.Album.Id.Should().Be(albumId);
        result.Errors.Should().BeEmpty();
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
            .Setup(x => x.GetAsync<AlbumExternalSourceDto>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<AlbumExternalSourceDto>
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