using Application.Infrastructure.External;
using FluentAssertions;
using Tests.Infrastructure;

namespace Tests.Application;

public class UrlProviderTests
    : UnitTestOf<UrlProvider>
{
    [Test]
    public void PhotoUrl_WhenAllIsWell()
    {
        // Act
        var result = UnderTest.PhotoUrl(1001);

        // Assert
        result.Should().Be("https://jsonplaceholder.typicode.com/photos/1001");
    }
}