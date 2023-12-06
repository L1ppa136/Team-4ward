using System.Net;
using System.Text;
using Inventory_Management_System.Contracts;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Xunit;

namespace Inventory_Integration_Test;

public class AuthenticationTest : IDisposable
{
    private InventoryManagementFactory _factory;
    private HttpClient _client;
    
    public AuthenticationTest()
    {
        _factory = new InventoryManagementFactory();
        _client = _factory.CreateClient();
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

    public void Dispose()
    {
        _factory.Dispose();
        _client.Dispose();
    }
}