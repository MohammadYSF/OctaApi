using Command.Infrastructure.Persistence.Persistence;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using OctaApi.Application.Features.CustomerFeatures.GetCustomers;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
using OctaShared.RabbitMqBus;
using ServiceStack;
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
        [Fact]
        public async void after_creating_buy_invoice_dataCount_should_increase()
        {
            var buyPrice = 250;
            var sellPrice = 280;
            var buyCount = 3000;
            int code = 12345;
            var sellerName = "آقای مفلوک";
            var response = await _queryHttpClient.GetAsync("/GetInventoryItems");
            response.EnsureSuccessStatusCode();
            var getInventoryItemsResponse_beforeCreatingBuyInvoice = await response.Content.ReadFromJsonAsync<GetInventoryItemsResponse>();
            if (getInventoryItemsResponse_beforeCreatingBuyInvoice.InventoryItemDTOs.Count > 0)
            {
                var response2 = await _queryHttpClient.GetAsync("/GetBuyInvoices");
                response2.EnsureSuccessStatusCode();
                var getBuyInvoicesResponse_beforeCreatingNewBuyInvoice = await response2.Content.ReadFromJsonAsync<GetBuyInvoicesResponse>();
                var radnomInventoryItem = getInventoryItemsResponse_beforeCreatingBuyInvoice.InventoryItemDTOs.Last();
                var request = new CreateBuyInvoiceRequest(new()
            {
                new(radnomInventoryItem.InventoryItemId,buyPrice,sellPrice,buyCount,0)
            }, code, sellerName, DateTime.UtcNow);
                var createBuyInvoiceResponse = await _commandHttpClient.PostAsJsonAsync($"/CreateBuyInvoice", request);
                createBuyInvoiceResponse.EnsureSuccessStatusCode();
                await Task.Delay(2000);
                var response3 = await _queryHttpClient.GetAsync("/GetBuyInvoices");
                response3.EnsureSuccessStatusCode();
                var getBuyInvoicesResponse_afterCreatingNewBuyInvoice = await response3.Content.ReadFromJsonAsync<GetBuyInvoicesResponse>();

                (getBuyInvoicesResponse_afterCreatingNewBuyInvoice.Data.Count - getBuyInvoicesResponse_beforeCreatingNewBuyInvoice.Data.Count).Should().Be(1);
            }
        }
        [Fact]
        public async void when_creating_buy_invoice_inventoryItem_Should_Update()
        {
            var buyPrice = 250;
            var sellPrice = 280;
            var buyCount = 3000;
            var sellerName = "آقای مفلوک";
            int code = 12345;
            var response = await _queryHttpClient.GetAsync("/GetInventoryItems");
            response.EnsureSuccessStatusCode();
            var getInventoryItemsResponse_beforeCreatingBuyInvoice = await response.Content.ReadFromJsonAsync<GetInventoryItemsResponse>();
            if (getInventoryItemsResponse_beforeCreatingBuyInvoice.InventoryItemDTOs.Count > 0)
            {
                var sut = getInventoryItemsResponse_beforeCreatingBuyInvoice.InventoryItemDTOs.Last();
                var request = new CreateBuyInvoiceRequest(new()
            {
                new(sut.InventoryItemId,buyPrice,sellPrice,buyCount,0)
            }, code, sellerName, DateTime.UtcNow);
                var createBuyInvoiceResponse = await _commandHttpClient.PostAsJsonAsync($"/CreateBuyInvoice", request);
                createBuyInvoiceResponse.EnsureSuccessStatusCode();
                await Task.Delay(2000);
                var response2 = await _queryHttpClient.GetAsync("/GetInventoryItems");
                response2.EnsureSuccessStatusCode();
                var getInventoryItemsResponse_after = await response2.Content.ReadFromJsonAsync<GetInventoryItemsResponse>();
                var sut2 = getInventoryItemsResponse_after.InventoryItemDTOs.First(a => a.InventoryItemId == sut.InventoryItemId);
                sut2.InventoryItemCount.Should().Be(sut.InventoryItemCount + buyCount);
                sut.InventoryItemSellPrice.Should().Be(sellPrice);
                sut.InventoryItemBuyPrice.Should().Be(buyPrice);
            }
        }
    }
}