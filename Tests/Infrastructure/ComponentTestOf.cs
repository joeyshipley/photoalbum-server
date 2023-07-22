using API.Infrastructure.IoC;
using Application.Infrastructure;
using Data.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Tests.Infrastructure.Fakes;

namespace Tests.Infrastructure;

public class ComponentTestOf<T>
    where T : class
{
    protected readonly List<Action<IServiceCollection>> DependencyFakes = new List<Action<IServiceCollection>>();
    protected T UnderTest;

    protected IApplicationContext TestContext;
    protected FakeTimeProvider TimeProvider;

    [SetUp]
    public void BaseBeforeEach()
    {
        initializeTestFakes();

        BeforeEach();

        UnderTest = Resolve<T>();
    }

    [TearDown]
    public async Task BaseAfterEach()
    {
        await deleteDbRecords();

        AfterEach();
    }

    protected virtual void BeforeEach() {}
    protected virtual void AfterEach() {}

    protected TResolveFor Resolve<TResolveFor>()
    {
        return DependencyRegistrations.Resolve<TResolveFor>(DependencyFakes);
    }

    private void initializeTestFakes()
    {
        TimeProvider = new FakeTimeProvider();
        TimeProvider.SetTime(DataDefaults.CurrentDate);

        DependencyFakes.Add((services) => 
        {
            services.AddScoped<IApplicationContext, InMemoryDbContext>();
            services.AddSingleton<IProvideTime>(TimeProvider);
        });

        TestContext = Resolve<IApplicationContext>();
    }

    private async Task deleteDbRecords()
    {
        TestContext.PhotoDetails.RemoveRange(TestContext.PhotoDetails.ToList());

        await TestContext.SaveChangesAsync();
    }
}