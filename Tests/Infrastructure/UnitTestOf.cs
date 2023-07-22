using Application.Infrastructure.External;
using Moq;
using Moq.AutoMock;

namespace Tests.Infrastructure;

public class UnitTestOf<T>
    where T : class
{
    protected T UnderTest;
    protected AutoMocker Mocker;

    [SetUp]
    public void BaseBeforeEach()
    {
        Mocker = new AutoMocker(MockBehavior.Loose, DefaultValue.Empty);
        attachRealDependencies(Mocker);

        BeforeEach();
        
        UnderTest = Mocker.CreateInstance<T>();
    }

    protected virtual void BeforeEach() {}

    private void attachRealDependencies(AutoMocker autoMocker)
    {
        autoMocker.Use<IUrlProvider>(new UrlProvider());
    }
}