using Inventory_Management_System.Service.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Test_InventoryManagementSystem.ControllerTests;

public class ImsAppFactory: WebApplicationFactory<Program>
{
    public Mock<IAuthenticationService> AuthenticationServiceMock { get; }

    public ImsAppFactory()
    {
        AuthenticationServiceMock = new Mock<IAuthenticationService>();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(AuthenticationServiceMock.Object);
        });
    }
}