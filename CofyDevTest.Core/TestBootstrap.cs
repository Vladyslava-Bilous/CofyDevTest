using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CofyDevTest.Core
{
    public  sealed class TestBootstrap
    {
        //Can be passed as Environment variable from pipeline
        private static readonly string Environment = "beta";

        private static readonly Lazy<TestBootstrap> Inst = new(() => new TestBootstrap(), true);

        public static TestBootstrap Instance => Inst.Value;

        public IServiceProvider ServiceProvider { get; }

        private TestBootstrap()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"config.{Environment}.json", true, false)
                .Build();

            ServiceProvider = ConfigureServices(configuration);
        }

        private IServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            var testConfig = configuration.GetSection("TestConfiguration").Get<TestConfiguration>();
            if (testConfig == null)
                throw new NullReferenceException(
                    "Test Configuration is null. Please, check the files with config: config.{environment}.json");
            services.AddSingleton(testConfig);
            return services.BuildServiceProvider();
        }

        public void Dispose()
        {
            if (ServiceProvider is ServiceProvider provider)
                provider.Dispose();
        }
    }
}
