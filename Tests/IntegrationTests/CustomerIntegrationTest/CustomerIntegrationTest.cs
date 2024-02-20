using Command.Infrastructure.Persistence.Persistence;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using OctaShared.DTOs.Request;
using OctaShared.DTOs.Response;
using OctaShared.RabbitMqBus;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using static ServiceStack.Diagnostics.Events;
namespace CustomerIntegrationTest
{
    public static class JwtTokenProvider
    {
        public static string Issuer { get; } = "Sample_Auth_Server";
        public static SecurityKey SecurityKey { get; } =
            new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes("This_is_a_super_secure_key_and_you_know_it")
            );
        public static SigningCredentials SigningCredentials { get; } =
            new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
        internal static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
    }
    public class TestJwtToken
    {
        public List<Claim> Claims { get; } = new();
        public int ExpiresInMinutes { get; set; } = 30;
        public TestJwtToken WithRole(string roleName)
        {
            Claims.Add(new Claim(ClaimTypes.Role, roleName));
            return this;
        }
        public string Build()
        {
            var token = new JwtSecurityToken(
                JwtTokenProvider.Issuer,
                JwtTokenProvider.Issuer,
                Claims,
                expires: DateTime.Now.AddMinutes(ExpiresInMinutes),
                signingCredentials: JwtTokenProvider.SigningCredentials
            );
            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(token);
        }
    }
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

                services.Configure<JwtBearerOptions>(
    JwtBearerDefaults.AuthenticationScheme,
        options =>
        {
            options.Configuration = new OpenIdConnectConfiguration
            {
                Issuer = JwtTokenProvider.Issuer,
            };
            // ValidIssuer and ValidAudience is not required, but it helps to define them as otherwise they can be overriden by for example the `user-jwts` tool which will cause the validation to fail
            options.TokenValidationParameters.ValidIssuer = JwtTokenProvider.Issuer;
            options.TokenValidationParameters.ValidAudience = JwtTokenProvider.Issuer;
            options.Configuration.SigningKeys.Add(JwtTokenProvider.SecurityKey);
        }
);
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


                services.Configure<JwtBearerOptions>(
JwtBearerDefaults.AuthenticationScheme,
options =>
{
    options.Configuration = new OpenIdConnectConfiguration
    {
        Issuer = JwtTokenProvider.Issuer,
    };
    // ValidIssuer and ValidAudience is not required, but it helps to define them as otherwise they can be overriden by for example the `user-jwts` tool which will cause the validation to fail
    options.TokenValidationParameters.ValidIssuer = JwtTokenProvider.Issuer;
    options.TokenValidationParameters.ValidAudience = JwtTokenProvider.Issuer;
    options.Configuration.SigningKeys.Add(JwtTokenProvider.SecurityKey);
}
);

            });



            builder.UseEnvironment("Development");
        }
    }
    public class CustomerIntegrationTest : IClassFixture<CommandWebApplicationFactory<Command.Presentation.Api.Program>>
        , IClassFixture<QueryWebApplicationFactory<Query.Presentation.Api.Program>>
    {
        private readonly string _token = string.Empty;
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
            var token = new TestJwtToken().WithRole("Admin").Build();
            _token = token;
            _commandHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            _queryHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        }

        private async Task<GetCustomersResponse> GetCustomers()
        {

            var response = await _queryHttpClient.GetAsync("/GetCustomers");
            response.EnsureSuccessStatusCode();
            var x = await response.Content.ReadFromJsonAsync<GetCustomersResponse>();
            return x;
        }
        private async Task<GetAllVehiclesResponse> GetVehicles()
        {
            var response = await _queryHttpClient.GetAsync("/GetAllVehicles");
            response.EnsureSuccessStatusCode();
            var x = await response.Content.ReadFromJsonAsync<GetAllVehiclesResponse>();
            return x;
        }
        [Fact]
        public async void after_adding_new_customer_customer_count_should_increase()
        {
            var getCustomersResponse_beforeAddingCustomer = await this.GetCustomers();

            var addCustomerRequest = new AddCustomerRequest("مهران", "فردوسی پور", "09217641919", DateTime.UtcNow, new List<OctaShared.DTOs.VehicleDTO>()
            {
                new OctaShared.DTOs.VehicleDTO("زانتیا","21ح981","زرشکی")
            });
            var createCustomerResponse = await _commandHttpClient.PostAsJsonAsync<AddCustomerRequest>("/AddCustomer", addCustomerRequest);
            createCustomerResponse.EnsureSuccessStatusCode();
            await Task.Delay(1000);

            var getCustomersResponse_afterAddingCustomer = await this.GetCustomers();
            (getCustomersResponse_afterAddingCustomer.Count - getCustomersResponse_beforeAddingCustomer.Count).Should().Be(1);
        }
        [Fact]
        public async void after_adding_new_customer_with_2vehicle_count_should_increase_by_2()
        {
            var getVehiclesResponse_before = await this.GetVehicles();

            var addCustomerRequest = new AddCustomerRequest("اشکان", "نایبی", "09145670091", DateTime.UtcNow, new List<OctaShared.DTOs.VehicleDTO>()
            {
                new OctaShared.DTOs.VehicleDTO("زانتیا","21ح981","زرشکی"),
                new OctaShared.DTOs.VehicleDTO("پیکان","87م123","زرشکی"),
            });
            var createCustomerResponse = await _commandHttpClient.PostAsJsonAsync<AddCustomerRequest>("/AddCustomer", addCustomerRequest);
            createCustomerResponse.EnsureSuccessStatusCode();
            await Task.Delay(1000);

            var getVehiclesResponse_after = await this.GetVehicles();
            (getVehiclesResponse_after.Count - getVehiclesResponse_before.Count).Should().Be(2);
        }

    }
}