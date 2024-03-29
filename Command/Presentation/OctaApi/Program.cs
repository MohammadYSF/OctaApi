using Command.Core.Application;
using Command.Core.Application.EventHandlers.Vehicle;
using Command.Infrastructure.Persistence.Persistence;
using OctaShared.Contracts;
using OctaShared.Events;
using OctaShared.RabbitMqBus;
namespace Command.Presentation.Api;
public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.ConfigurePersistence(builder.Configuration);
        builder.Services.ConfigureApplication();
        builder.Services.ConfigureBus(builder.Configuration);
        string authUrl = builder.Configuration.GetSection("AuthUrl").Value;
        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Authority = authUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "api1";
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.ValidateIssuer = false;
                options.TokenValidationParameters.ValidateIssuerSigningKey = false;
            });
        builder.Services.AddAuthorization(options =>
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "api1");
            })
        );
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors(options =>
        {
            options.AllowAnyOrigin();
            options.AllowAnyHeader();
            options.AllowAnyMethod();
        });
        //}

        //app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        var eventBus = app.Services.GetRequiredService<IEventBus>();
        eventBus.Subscribe<VehicleAddedToCustomerEvent, VehicleAggregateEventHandler>();
        app.MapControllers();

        app.Run();
    }
}
public partial class Program { }