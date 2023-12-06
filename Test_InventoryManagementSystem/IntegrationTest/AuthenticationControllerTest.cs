using System.Net;
using System.Text;
using Inventory_Management_System.Contracts;
using Inventory_Management_System.Service.Authentication;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Test_InventoryManagementSystem.ControllerTests;

public class AuthenticationControllerTest : IDisposable
{
    private ImsAppFactory _factory;
    private HttpClient _client;

    public AuthenticationControllerTest()
    {
        _factory = new ImsAppFactory();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task LoginAsync_ValidCredentials_ReturnsLoggedInUser()
    {
        // Arrange
        var userName = "user1";
        var password = "password123";
        var email = "user1@email.com";
        var token = "userToken";

        var request = new AuthenticationRequest(userName, password);
        var requestJson = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8,
            "application/json");
        var expectedAuthResult = new AuthenticationResult(true, "user1@email.com", "user1", "userToken");
        
        _factory.AuthenticationServiceMock.Setup(authService => authService.LoginAsync("user1", "password123"))
            .ReturnsAsync(expectedAuthResult);
        
        //Act
        var response = await _client.PostAsync("/Authentication/Login", requestJson);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var actualAuthResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(jsonResponse);
        
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK) | Is.EqualTo(HttpStatusCode.NoContent));
        Assert.IsNotNull(actualAuthResponse?.Token);
        Assert.That(actualAuthResponse?.UserName, Is.EqualTo(userName));
        Assert.That(actualAuthResponse?.Email, Is.EqualTo(email));
        Assert.That(actualAuthResponse?.Token, Is.EqualTo(token));
        Assert.That(actualAuthResponse, Is.InstanceOf<AuthenticationResponse>());
    }
    
    
    
    public void Dispose()
    {
        _factory.Dispose();
        _client.Dispose();
    }
}