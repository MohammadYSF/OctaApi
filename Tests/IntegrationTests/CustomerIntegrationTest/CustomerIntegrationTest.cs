using Command.Infrastructure.Persistence.Persistence;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
using OctaShared.DTOs.Request;
using OctaShared.RabbitMqBus;
using System.Net.Http.Json;

namespace CustomerIntegrationTest
{
    public class CommandWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            var configurationCommand = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                 path: "appsettingsCommand.json",
                 optional: false,
                 reloadOnChange: true)
           .Build();
            builder.ConfigureServices(services =>
            {
                services.ConfigurePersistence(configurationCommand);
                Command.Core.Application.ServiceExtensions.ConfigureApplication(services);
                services.ConfigureBus(configurationCommand);

            });

            builder.UseEnvironment("Development");
        }
    }
    public class QueryWebApplicationFactory<TProgram>
  : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            var configurationQuery = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                 path: "appsettingsQuery.json",
                 optional: false,
                 reloadOnChange: true)
           .Build();
            builder.ConfigureServices(services =>
            {
                Query.Application.ServiceExtensions.ConfigureApplication(services);
                Query.Persistence.ServiceExtentions.ConfigurePersistence(services, configurationQuery);

            });

            builder.UseEnvironment("Development");
        }
    }
    public class CustomerIntegrationTest : IClassFixture<CommandWebApplicationFactory<Command.Presentation.Api.Program>>
        , IClassFixture<QueryWebApplicationFactory<Query.Presentation.Api.Program>>
    {
        private readonly System.Net.Http.HttpClient _commandHttpClient;
        private readonly System.Net.Http.HttpClient _queryHttpClient;
        private readonly CommandWebApplicationFactory<Command.Presentation.Api.Program>
            _commandFactory;
        private readonly QueryWebApplicationFactory<Query.Presentation.Api.Program>
  _queryFactory;
        public CustomerIntegrationTest(CommandWebApplicationFactory<Command.Presentation.Api.Program> commandFactory, QueryWebApplicationFactory<Query.Presentation.Api.Program> queryFactory)
        {
            _commandFactory = commandFactory;
            _queryFactory = queryFactory;
            _commandHttpClient = _commandFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _queryHttpClient = _queryFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }



        [Fact]
        public async void after_adding_new_customer_customer_count_should_increase()
        {
            var response = await _queryHttpClient.GetAsync("/GetCustomers");
            response.EnsureSuccessStatusCode();
            var getCustomersResponse_beforeAddingCustomer = await response.Content.ReadFromJsonAsync<GetCustomersResponse>();

            var addCustomerRequest = new AddCustomerRequest("مهران", "فردوسی پور", "09217641909", DateTime.UtcNow, new List<OctaShared.DTOs.VehicleDTO>()
            {
                new OctaShared.DTOs.VehicleDTO("زانتیا","21ح981","زرشکی")
            });
            var createCustomerResponse = await _commandHttpClient.PostAsJsonAsync<AddCustomerRequest>("/AddCustomer", addCustomerRequest);
            createCustomerResponse.EnsureSuccessStatusCode();
            await Task.Delay(1000);
            var response2 = await _queryHttpClient.GetAsync("/GetCustomers");
            response.EnsureSuccessStatusCode();
            var getCustomersResponse_afterAddingCustomer = await response2.Content.ReadFromJsonAsync<GetCustomersResponse>();
            (getCustomersResponse_afterAddingCustomer.Count - getCustomersResponse_beforeAddingCustomer.Count).Should().Be(1);
        }
    }
}