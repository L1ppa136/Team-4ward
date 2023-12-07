using System.Net;
using System.Text;
using Inventory_Management_System.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Inventory_Integration_Test;

public class AuthenticationTest : IDisposable
{
    private InventoryManagementFactory _factory;
    private HttpClient _client;
    private readonly ITestOutputHelper _output;
    
    public AuthenticationTest()
    {
        _factory = new InventoryManagementFactory();
        _client = _factory.CreateClient();
        _output = new TestOutputHelper();
    }

    [Fact]
    public async Task Test_Registration()
    {
        var loginRequest = new AuthenticationRequest("user1", "password1");
        
        var loginResponse = await _client.PostAsync("/Authentication/Login",
            new StringContent(JsonConvert.SerializeObject(loginRequest), 
                Encoding.UTF8, "application/json"));

        var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await loginResponse.Content.ReadAsStringAsync());
        
        if (loginResponse.StatusCode == HttpStatusCode.OK)
        {
            Assert.NotNull(authResponse.Token);
            Assert.Equal("user1@email.com", authResponse.Email);
            Assert.Equal("user1", authResponse.UserName);
        }
        else
        {
            var registrationRequest = 
                new RegistrationRequest("user2@email.com", "user2", "password2");

            var registrationResponse = await _client.PostAsync("/Authentication/Register",
                new StringContent(JsonConvert.SerializeObject(registrationRequest), 
                    Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, registrationResponse.StatusCode);
        }
 
    }
    
    [Fact]
    public async Task Test_SetRole()
    {
        var loginRequest = new AuthenticationRequest("admin", "Admin123");
        
        var loginResponse = await _client.PostAsync("/Authentication/Login",
            new StringContent(JsonConvert.SerializeObject(loginRequest), 
                Encoding.UTF8, "application/json"));

        var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await loginResponse.Content.ReadAsStringAsync());
        
            Assert.NotNull(authResponse.Token);
            Assert.Equal("admin@admin.com", authResponse.Email);
            Assert.Equal("admin", authResponse.UserName);
        //Role request user1-re, az alapján változtassuk
        var setRoleRequest = new SetRoleRequest("user1", "Forklift Driver");

        var setRoleResponse = await _client.PostAsync("/Admin/SetRole", new StringContent(JsonConvert.SerializeObject(setRoleRequest), 
                Encoding.UTF8, "application/json"));
        
        var setRoleResponseArr = JsonConvert.DeserializeObject(await setRoleResponse.Content.ReadAsStringAsync());

        _output.WriteLine(setRoleResponseArr.ToString().IsNullOrEmpty() ? setRoleResponseArr.ToString() : "");
        
        Assert.Equal("admin@admin.com", setRoleResponse.Content.Headers.);
        Assert.Equal("admin", authResponse.UserName);
    }

    public void Dispose()
    {
        _factory.Dispose();
        _client.Dispose();
    }
}