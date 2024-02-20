using Command.Infrastructure.Persistence.Persistence;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
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
            var getInventoryItemsResponse_beforeAdding = await this.GetInventoryItemsAsync();

            var addInventoryItemRequest = new AddInventoryItemRequest("سرسیلندر");
            var createCustomerResponse = await _commandHttpClient.PostAsJsonAsync("/AddInventoryItem", addInventoryItemRequest);
            createCustomerResponse.EnsureSuccessStatusCode();
            await Task.Delay(1000);
            var getInventoryItemsResponse_afterAdding = await this.GetInventoryItemsAsync();
            (getInventoryItemsResponse_afterAdding.InventoryItemDTOs.Count() - getInventoryItemsResponse_beforeAdding.InventoryItemDTOs.Count()).Should().Be(1);
        }
        [Fact]
        public async void when_deleting_inventoryitem_count_should_decrease()
        {
            var getInventoryItemsResponse_beforeDeleting = await this.GetInventoryItemsAsync();
            if (getInventoryItemsResponse_beforeDeleting.InventoryItemDTOs.Count() > 0)
            {
                var inventoryItemToDelete = getInventoryItemsResponse_beforeDeleting.InventoryItemDTOs.First();
                var code = int.Parse(inventoryItemToDelete.InventoryItemCode);
                var deleteResponse = await _commandHttpClient.DeleteAsync($"/DeleteInventoryItem?code={code}");
                deleteResponse.EnsureSuccessStatusCode();
                await Task.Delay(2000);

                var getInventoryItemsResponse_afterDeleting = await this.GetInventoryItemsAsync();
                (getInventoryItemsResponse_afterDeleting.InventoryItemDTOs.Count() - getInventoryItemsResponse_beforeDeleting.InventoryItemDTOs.Count()).Should().Be(-1);
            }
        }
        private async Task<GetAllVehiclesResponse> GetVehicles()
        {
            var response = await _queryHttpClient.GetAsync("/GetAllVehicles");
            response.EnsureSuccessStatusCode();
            var x = await response.Content.ReadFromJsonAsync<GetAllVehiclesResponse>();
            return x;
        }
        [Fact]
        public async void after_creating_sellInvoice_dataCount_should_increase()
        {
            var getSellInvoicesResponse_before = await this.GetSellInvoicesAsync();
            var vehicles = await this.GetVehicles();
            var randomVehicle = vehicles.Data.Last();
            CreateSellInvoiceRequest request = new(randomVehicle.VehicleId, randomVehicle.CustomerId);
            var createSellInvoiceResponse = await _commandHttpClient.PostAsJsonAsync($"/CreateSellInvoice", request);
            createSellInvoiceResponse.EnsureSuccessStatusCode();
            await Task.Delay(2000);
            var getSellInvoicesResponse_after = await this.GetSellInvoicesAsync();
            (getSellInvoicesResponse_after.Data.Count - getSellInvoicesResponse_before.Data.Count).Should().Be(1);
        }
        [Fact]
        public async void after_updating_sellInvoice_getData_should_be_updated()
        {
            await this.CreateMiscellaneousInvoice();
            await Task.Delay(2000);
            var getInventoryItems_before = await this.GetInventoryItemsAsync();
            var getServices_before = await this.GetServicesAsync();
            var getSellInvoicesResponse_before = await this.GetSellInvoicesAsync();
            var randomSellInvoice = getSellInvoicesResponse_before.Data.OrderBy(a => a.SellInvoiceDate).Last();
            var randomInventoryItem = getInventoryItems_before.InventoryItemDTOs.First();
            string description = "سه ماهه دیگر ماشینتو یه بار دیگه بیار اینجا پیش خودم";
            var randomService = getServices_before.ServiceDTOs.First();
            var request = new UpdateInvoiceServicesAndInventoryItemsRequest(randomSellInvoice.SellInvoiceId, new()
            {
                new(randomInventoryItem.InventoryItemId,randomInventoryItem.InventoryItemCount-2)
            }, new()
            {
                new(randomService.ServiceId,6000)
            }, new() { },
            new() { },
            false,
            description);
            var updateSellInvoice = await _commandHttpClient.PutAsJsonAsync($"/UpdateInvoiceServicesAndInventoryItems", request);
            updateSellInvoice.EnsureSuccessStatusCode();
            await Task.Delay(2000);
            var getInventoryItems_after = await this.GetInventoryItemsAsync();
            var target_inventoryItem = getInventoryItems_after.InventoryItemDTOs.First(a => a.InventoryItemId == randomInventoryItem.InventoryItemId);
            var getSellInvoicecs_after = await this.GetSellInvoicesAsync();
            var target_sellInvoice = getSellInvoicecs_after.Data.First(a => a.SellInvoiceId == randomSellInvoice.SellInvoiceId);
            target_sellInvoice.ToPay.Should().Be((long)(((randomInventoryItem.InventoryItemCount - 2) * randomInventoryItem.InventoryItemSellPrice) + (6000)));
        }

        private async Task CreateMiscellaneousInvoice()
        {
            CreateMiscellaneousSellInvoiceRequest request = new();
            var createSellInvoiceResponse = await _commandHttpClient.PostAsJsonAsync($"/CreateMiscellaneousSellInvoice", request);
            createSellInvoiceResponse.EnsureSuccessStatusCode();
        }
        [Fact]
        public async void after_creating_miscellaneous_sellInvoice_dataCount_should_increase()
        {
            var getSellInvoicesResponse_before = await this.GetSellInvoicesAsync();
            var vehicles = await this.GetVehicles();
            var randomVehicle = vehicles.Data.Last();
            await this.CreateMiscellaneousInvoice();
            await Task.Delay(2000);
            var getSellInvoicesResponse_after = await this.GetSellInvoicesAsync();
            (getSellInvoicesResponse_after.Data.Count - getSellInvoicesResponse_before.Data.Count).Should().Be(1);
        }

        [Fact]
        public async void after_creating_buy_invoice_dataCount_should_increase()
        {
            var buyPrice = 250;
            var sellPrice = 280;
            var buyCount = 3000;
            int code = 12345;
            var sellerName = "آقای مفلوک";
            var getInventoryItemsResponse_beforeCreatingBuyInvoice = await this.GetInventoryItemsAsync();
            if (getInventoryItemsResponse_beforeCreatingBuyInvoice.InventoryItemDTOs.Count > 0)
            {
                var getBuyInvoicesResponse_beforeCreatingNewBuyInvoice = await this.GetBuyInvoices();
                var radnomInventoryItem = getInventoryItemsResponse_beforeCreatingBuyInvoice.InventoryItemDTOs.Last();
                var request = new CreateBuyInvoiceRequest(new()
            {
                new(radnomInventoryItem.InventoryItemId,buyPrice,sellPrice,buyCount,0)
            }, code, sellerName, DateTime.UtcNow);
                var createBuyInvoiceResponse = await _commandHttpClient.PostAsJsonAsync($"/CreateBuyInvoice", request);
                createBuyInvoiceResponse.EnsureSuccessStatusCode();
                await Task.Delay(2000);
                var getBuyInvoicesResponse_afterCreatingNewBuyInvoice = await this.GetBuyInvoices();

                (getBuyInvoicesResponse_afterCreatingNewBuyInvoice.Data.Count - getBuyInvoicesResponse_beforeCreatingNewBuyInvoice.Data.Count).Should().Be(1);
            }
        }
        private async Task<GetInventoryItemsResponse> GetInventoryItemsAsync()
        {
            var response = await _queryHttpClient.GetAsync("/GetInventoryItems");
            response.EnsureSuccessStatusCode();
            var x = await response.Content.ReadFromJsonAsync<GetInventoryItemsResponse>();
            return x;
        }
        private async Task<GetServicesResponse> GetServicesAsync()
        {
            var response = await _queryHttpClient.GetAsync("/GetServices");
            response.EnsureSuccessStatusCode();
            var x = await response.Content.ReadFromJsonAsync<GetServicesResponse>();
            return x;
        }
        private async Task<GetBuyInvoicesResponse> GetBuyInvoices()
        {
            var response = await _queryHttpClient.GetAsync("/GetBuyInvoices");
            response.EnsureSuccessStatusCode();
            var x = await response.Content.ReadFromJsonAsync<GetBuyInvoicesResponse>();
            return x;
        }
        private async Task<GetSellInvoicesResponse> GetSellInvoicesAsync()
        {
            var response = await _queryHttpClient.GetAsync("/GetSellInvoices");
            response.EnsureSuccessStatusCode();
            var x = await response.Content.ReadFromJsonAsync<GetSellInvoicesResponse>();
            return x;
        }
        [Fact]
        public async void AfterUpdatingBuyInventoryItem_DataShouldBeUpdated()
        {
            var randomInventoryItem = (await this.GetInventoryItemsAsync()).InventoryItemDTOs.First();
            Guid inventoryItemId = randomInventoryItem.InventoryItemId;
            string name = "تایر مشکلی اسپانیایی";
            var buyPrice = 500;
            var sellPrice = 2000;
            var count = 212;
            var countLowerBound = 0;
            var request = new UpdateInventoryItemRequest(inventoryItemId, name, buyPrice, sellPrice, count, countLowerBound);
            var response = await _commandHttpClient.PutAsJsonAsync("/UpdateInventoryItem", request);
            response.EnsureSuccessStatusCode();
            await Task.Delay(2000);
            var x = await response.Content.ReadFromJsonAsync<UpdateInventoryItemResponse>();
            var inventoryItem_afterUpdate = (await this.GetInventoryItemsAsync()).InventoryItemDTOs.First(a => a.InventoryItemId == inventoryItemId);
            inventoryItem_afterUpdate.InventoryItemName.Should().Be(name);
            inventoryItem_afterUpdate.InventoryItemCode.Should().Be(randomInventoryItem.InventoryItemCode);
            inventoryItem_afterUpdate.InventoryItemCount.Should().Be(count);
            inventoryItem_afterUpdate.InventoryItemSellPrice.Should().Be(sellPrice);
            inventoryItem_afterUpdate.InventoryItemBuyPrice.Should().Be(buyPrice);

        }
        [Fact]
        public async void after_adding_new_service_count_should_increase()
        {
            var name = "سرویس تعویض تسمه";
            var price = 100000;
            var services_before = await this.GetServicesAsync();
            var request = new AddServiceRequest(name, price);
            var response = await _commandHttpClient.PostAsJsonAsync("/AddService", request);
            response.EnsureSuccessStatusCode();
            await Task.Delay(2000);
            var services_after = await this.GetServicesAsync();
            (services_after.ServiceDTOs.Count - services_before.ServiceDTOs.Count).Should().Be(1);
        }
        [Fact]
        public async void when_creating_buy_invoice_inventoryItem_Should_Update()
        {
            var buyPrice = 250;
            var sellPrice = 280;
            var buyCount = 3000;
            var sellerName = "آقای مفلوک";
            int code = 12345;
            var getInventoryItemsResponse_beforeCreatingBuyInvoice = await this.GetInventoryItemsAsync();
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

                var getInventoryItemsResponse_after = await this.GetInventoryItemsAsync();
                var sut2 = getInventoryItemsResponse_after.InventoryItemDTOs.First(a => a.InventoryItemId == sut.InventoryItemId);
                sut2.InventoryItemCount.Should().Be(sut.InventoryItemCount + buyCount);
                sut.InventoryItemSellPrice.Should().Be(sellPrice);
                sut.InventoryItemBuyPrice.Should().Be(buyPrice);
            }
        }
    }
}