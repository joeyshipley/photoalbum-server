using Application.Photos.Persistence;
using FluentAssertions;
using Tests.Infrastructure;
using Tests.Infrastructure.Creationals.Entities;
using Tests.Infrastructure.TestBases;

namespace Tests.Data.Photos;

public class PhotoRepositoryTests 
    : ComponentTestOf<IPhotoRepository>
{
    [Test]
    public async Task Find_WhenAllIsWell()
    {
        // Arrange
        var photoDetail = PhotoDetailsBuilder
            .AsDefault()
            .WithPhotoId(1001)
            .Build();
        TestContext.PhotoDetails.Add(photoDetail);
        await TestContext.SaveChangesAsync();

        // Act
        var result = await UnderTest.Find(1001);

        // Assert
        result.Should().NotBeNull();
        result?.Id.Should().NotBe(default);
        result?.Id.Should().Be(photoDetail.Id);
        result?.PhotoId.Should().Be(1001);
        result?.CreatedOn.Should().NotBe(default);
        result?.LastUpdatedOn.Should().NotBe(default);
        result?.Likes.Should().Be(99);
    }

    [Test]
    public async Task Upsert_WhenAddingNewEntity()
    {
        // Arrange
        var entity = PhotoDetailsBuilder
            .AsDefault()
            .WithPhotoId(1001)
            .WithoutDates()
            .Build();

        // Act
        var result = await UnderTest.Upsert(entity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBe(default);

        var saved = TestContext.PhotoDetails.First(x => x.Id == entity.Id);
        saved.Should().NotBeNull();
        saved?.PhotoId.Should().Be(1001);
        saved?.CreatedOn.Should().Be(DataDefaults.CurrentDate);
        saved?.CreatedOn.Should().Be(DataDefaults.CurrentDate);
        saved?.Likes.Should().Be(99);
    }
    
    [Test]
    public async Task Upsert_WhenUpdatingAnExistingEntity()
    {
        // Arrange
        var photoDetail = PhotoDetailsBuilder
            .AsDefault()
            .WithCreationDate(DataDefaults.PriorDate)
            .WithPhotoId(1001)
            .Build();
        TestContext.PhotoDetails.Add(photoDetail);
        await TestContext.SaveChangesAsync();

        var entity = PhotoDetailsBuilder
            .AsDefault()
            .WithPhotoId(1001)
            .WithLikes(999)
            .WithoutDates()
            .Build();

        // Act
        var result = await UnderTest.Upsert(entity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(photoDetail.Id);
        result?.PhotoId.Should().Be(1001);
        result?.CreatedOn.Should().Be(DataDefaults.PriorDate);
        result?.LastUpdatedOn.Should().Be(DataDefaults.CurrentDate);
        result?.Likes.Should().Be(999);
    }
}