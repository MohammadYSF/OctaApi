using Command.Infrastructure.Persistence.Persistence;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
using OctaShared.RabbitMqBus;
using System.Net.Http.Json;

namespace InventoryItemIntegrationTest
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
    public class InventoryItemIntegrationTest : IClassFixture<CommandWebApplicationFactory<Command.Presentation.Api.Program>>
        , IClassFixture<QueryWebApplicationFactory<Query.Presentation.Api.Program>>
    {
        private readonly System.Net.Http.HttpClient _commandHttpClient;
        private readonly System.Net.Http.HttpClient _queryHttpClient;
        private readonly CommandWebApplicationFactory<Command.Presentation.Api.Program>
            _commandFactory;
        private readonly QueryWebApplicationFactory<Query.Presentation.Api.Program>
  _queryFactory;
        public InventoryItemIntegrationTest(CommandWebApplicationFactory<Command.Presentation.Api.Program> commandFactory, QueryWebApplicationFactory<Query.Presentation.Api.Program> queryFactory)
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
        public async void after_adding_new_inventoryItem_count_should_increase()
        {
            var response = await _queryHttpClient.GetAsync("/GetInventoryItems");
            response.EnsureSuccessStatusCode();
            var getInventoryItemsResponse_beforeAdding = await response.Content.ReadFromJsonAsync<GetInventoryItemsResponse>();

            var addInventoryItemRequest = new AddInventoryItemRequest("سرسیلندر");
            var createCustomerResponse = await _commandHttpClient.PostAsJsonAsync("/AddInventoryItem", addInventoryItemRequest);
            createCustomerResponse.EnsureSuccessStatusCode();
            await Task.Delay(1000);
            var response2 = await _queryHttpClient.GetAsync("/GetInventoryItems");
            response.EnsureSuccessStatusCode();
            var getInventoryItemsResponse_afterAdding = await response2.Content.ReadFromJsonAsync<GetInventoryItemsResponse>();
            (getInventoryItemsResponse_afterAdding.InventoryItemDTOs.Count() - getInventoryItemsResponse_beforeAdding.InventoryItemDTOs.Count()).Should().Be(1);
        }
        [Fact]
        public async void when_deleting_inventoryitem_count_should_decrease()
        {
            var response = await _queryHttpClient.GetAsync("/GetInventoryItems");
            response.EnsureSuccessStatusCode();
            var getInventoryItemsResponse_beforeDeleting = await response.Content.ReadFromJsonAsync<GetInventoryItemsResponse>();
            if (getInventoryItemsResponse_beforeDeleting.InventoryItemDTOs.Count() > 0)
            {
                var inventoryItemToDelete = getInventoryItemsResponse_beforeDeleting.InventoryItemDTOs.First();
                var code = int.Parse(inventoryItemToDelete.InventoryItemCode);
                var deleteResponse = await _commandHttpClient.DeleteAsync($"/DeleteInventoryItem?code={code}");
                deleteResponse.EnsureSuccessStatusCode();
                await Task.Delay(2000);
                var response2 = await _queryHttpClient.GetAsync("/GetInventoryItems");
                response2.EnsureSuccessStatusCode();
                var getInventoryItemsResponse_afterDeleting = await response2.Content.ReadFromJsonAsync<GetInventoryItemsResponse>();
                (getInventoryItemsResponse_afterDeleting.InventoryItemDTOs.Count() - getInventoryItemsResponse_beforeDeleting.InventoryItemDTOs.Count()).Should().Be(-1);
            }
        }
    }
}