using API.Infrastructure.IoC;

namespace Tests.Infrastructure;

public class ComponentTestOf<T>
    where T : class
{
    protected T UnderTest;

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