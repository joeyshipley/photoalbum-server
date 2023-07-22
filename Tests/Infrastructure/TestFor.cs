using API.Infrastructure.IoC;

namespace Tests.Infrastructure;

public class TestFor<T>
    where T : class
{
    protected T UnderTest;

    [OneTimeSetUp]
    public void BeforeTestSuite()
    {
    }

    [SetUp]
    public void BaseBeforeEach()
    {
        BeforeEach();
        UnderTest = Resolve<T>();
    }

    protected virtual void BeforeEach() {}

    protected TResolveFor Resolve<TResolveFor>()
    {
        return DependencyRegistrations.Resolve<TResolveFor>();
    }
}