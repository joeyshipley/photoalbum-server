using Application.Infrastructure.External;
using Application.Photos;
using Application.Photos.Viewer;
using FluentAssertions;
using Moq;
using Tests.Infrastructure;

namespace Tests.Application.Photos;

public class PhotoViewerServiceTests
    : UnitTestOf<PhotoViewerService>
{
    [Test]
    public async Task View_WhenAllIsWell()
    {
        // Arrange
        var photoId = 1001;
        var apiCallerMock = Mocker.GetMock<IApiCaller>();
        apiCallerMock
            .Setup(x => x.GetAsync<PhotoEntry>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<PhotoEntry>
            {
                Model = new PhotoEntry { Id = photoId },
            });

        var request = new PhotoViewerRequest { Id = photoId };

        // Act
        var result = await UnderTest.View(request);

        // Assert
        result.Photo.Id.Should().Be(photoId);
    }

    [Test]
    public async Task View_WhenErrorReturnedFromApi()
    {
        // Arrange
        var photoId = 1001;
        var apiCallerMock = Mocker.GetMock<IApiCaller>();
        apiCallerMock
            .Setup(x => x.GetAsync<PhotoEntry>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<PhotoEntry>
            {
                Errors = new List<string> { "Nope!" }
            });

        var request = new PhotoViewerRequest { Id = photoId };

        // Act
        var result = await UnderTest.View(request);

        // Assert
        result.Photo.Should().BeNull();
        result.Errors.Count.Should().Be(1);
        result.Errors.Any(x => x.Key == "API_FAILURE").Should().BeTrue("API_FAILURE error was not found.");
    }
}