using Application.Photos.Viewer;
using FluentAssertions;
using Tests.Infrastructure;

namespace Tests.Application.Photos;

public class PhotoViewerServiceTests
    : TestFor<IPhotoViewerService>
{
    [Test]
    public void View_WhenAllIsWell()
    {
        // Arrange
        var request = new PhotoViewerRequest { Id = 1001 };

        // Act
        var result = UnderTest.View(request);

        // Assert
        result.Photo.Id.Should().Be(1001);
    }
}