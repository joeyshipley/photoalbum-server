using Application.Infrastructure.External;
using Application.Photos;
using Application.Photos.External;
using Application.Photos.Mutators;
using Application.Photos.Mutators.RequestsResults;
using Application.Photos.Persistence;
using FluentAssertions;
using Moq;
using Tests.Infrastructure.TestBases;

namespace Tests.Application.Photos;

public class PhotoLikeServiceTests
    : UnitTestOf<PhotoLikeService>
{
    [Test]
    public async Task Like_WhenPhotoDetailsDoNotExist()
    {
        // Arrange
        var request = new LikeRequest { PhotoId = 101 };

        Mocker.GetMock<IApiCaller>()
            .Setup(x => x.GetAsync<PhotoExternalSourceDto>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<PhotoExternalSourceDto>
            {
                Model = new PhotoExternalSourceDto { Id = 101 },
            });

        var entityRepository = Mocker.GetMock<IPhotoRepository>();
        entityRepository
            .Setup(x => x.Find(It.IsAny<int>()))
            .ReturnsAsync((PhotoDetailsEntity) null);

        PhotoDetailsEntity entityToSave = null;
        entityRepository
            .Setup(x => x.Upsert(It.IsAny<PhotoDetailsEntity>()))
            .Callback((PhotoDetailsEntity toSave) =>
            {
                entityToSave = toSave;
            })
            .ReturnsAsync(new PhotoDetailsEntity { PhotoId = 101, Likes = 1, });

        // Act
        var result = await UnderTest.Like(request);

        // Assert
        result.Errors.Should().BeEmpty();

        entityToSave.Should().NotBeNull();
        entityToSave.PhotoId.Should().Be(101);
        entityToSave.Likes.Should().Be(1);

        result.PhotoLikeDetails.Should().NotBeNull();
        result.PhotoLikeDetails.PhotoId.Should().Be(101);
        result.PhotoLikeDetails.Likes.Should().Be(1);
    }

    [Test]
    public async Task Like_WhenPhotoDetailsAlreadyExists()
    {
        // Arrange
        var request = new LikeRequest { PhotoId = 101 };

        Mocker.GetMock<IApiCaller>()
            .Setup(x => x.GetAsync<PhotoExternalSourceDto>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<PhotoExternalSourceDto>
            {
                Model = new PhotoExternalSourceDto { Id = 101 },
            });

        var entityRepository = Mocker.GetMock<IPhotoRepository>();
        entityRepository
            .Setup(x => x.Find(It.IsAny<int>()))
            .ReturnsAsync(new PhotoDetailsEntity { PhotoId = 101, Likes = 1, });

        PhotoDetailsEntity entityToSave = null;
        entityRepository
            .Setup(x => x.Upsert(It.IsAny<PhotoDetailsEntity>()))
            .Callback((PhotoDetailsEntity toSave) =>
            {
                entityToSave = toSave;
            })
            .ReturnsAsync(new PhotoDetailsEntity { PhotoId = 101, Likes = 2, });

        // Act
        var result = await UnderTest.Like(request);

        // Assert
        result.Errors.Should().BeEmpty();

        entityToSave.Should().NotBeNull();
        entityToSave.PhotoId.Should().Be(101);
        entityToSave.Likes.Should().Be(2);

        result.PhotoLikeDetails.Should().NotBeNull();
        result.PhotoLikeDetails.PhotoId.Should().Be(101);
        result.PhotoLikeDetails.Likes.Should().Be(2);
    }

    [Test]
    public async Task Like_WhenRequestIsInvalid()
    {
        // Arrange
        var request = new LikeRequest { PhotoId = 0 };

        // Act
        var result = await UnderTest.Like(request);

        // Assert
        result.PhotoLikeDetails.Should().BeNull();
        result.Errors.Count.Should().Be(1);
        result.Errors.Any(x => x.Key == "INVALID_PHOTO_ID").Should().BeTrue("INVALID_PHOTO_ID error was not found.");
    }
    
    [Test]
    public async Task Like_WhenPhotoDoesNotExistExternally()
    {
        // Arrange
        var request = new LikeRequest { PhotoId = 999999999 };

        Mocker.GetMock<IApiCaller>()
            .Setup(x => x.GetAsync<PhotoExternalSourceDto>(It.IsAny<string>()))
            .ReturnsAsync(new ApiCallerResponse<PhotoExternalSourceDto>
            {
                Errors = new List<string> { "Nope!" }
            });

        // Act
        var result = await UnderTest.Like(request);

        // Assert
        result.PhotoLikeDetails.Should().BeNull();
        result.Errors.Count.Should().Be(1);
        result.Errors.Any(x => x.Key == "PHOTO_DOES_NOT_EXIST").Should().BeTrue("PHOTO_DOES_NOT_EXIST error was not found.");
    }
}