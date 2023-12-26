using Query.Application;
using Query.Application.Core;
using Query.Application.EventHandlers.BuyInvoice;
using Query.Application.EventHandlers.Customer;
using Query.Application.EventHandlers.InventoryItem;
using Query.Application.EventHandlers.SellInvoice;
using Query.Application.Events;
using Query.Application.Events.BuyInvoice;
using Query.Application.Events.Customer;
using Query.Application.Events.InventoryItem;
using Query.Application.Events.SellInvoice;
using Query.Application.Events.Vehicles;
using Query.Application.Repositories;
using Query.Infrastructure.RabbitMqBus;
using Query.Persistence;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureBus(builder.Configuration);
builder.Services.ConfigureApplication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<BuyInvoiceCreatedEvent, BuyInvoiceEventHandler>();
eventBus.Subscribe<CustomerCreatedEvent, CustomerEventHandler>();
eventBus.Subscribe<VehicleCreatedEvent, CustomerEventHandler>();
eventBus.Subscribe<VehicleCreatedEvent, CustomerEventHandler>();
eventBus.Subscribe<InventoryItemCreatedEvent, InventoryItemEventHandler>();
eventBus.Subscribe<InventoryItemUpdatedEvent, InventoryItemEventHandler>();
eventBus.Subscribe<SellInvoiceCreatedEvent, SellInvoiceEventHandler>();
eventBus.Subscribe<SellInvoiceDeletedEvent, SellInvoiceEventHandler>();
eventBus.Subscribe<SellInvoicePaymentCreatedEvent, SellInvoiceEventHandler>();
eventBus.Subscribe<ServiceAddedToSellInvoiceEvent, SellInvoiceEventHandler>();
eventBus.Subscribe<InventoryItemAddedToSellInvoiceEvent, SellInvoiceEventHandler>();
eventBus.Subscribe<ServiceRemovedFromSellInvoiceEvent, SellInvoiceEventHandler>();
eventBus.Subscribe<InventoryItemRemovedFromSellInvoicecEvent, SellInvoiceEventHandler>();

app.MapControllers();

app.Run();
