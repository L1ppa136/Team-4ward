using Inventory_Management_System.Contracts;
using Inventory_Management_System.Controllers;
using Inventory_Management_System.Service.Authentication;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Test_InventoryManagementSystem;

public class AuthControllerTest
{
    private Mock<IAuthenticationService> _authServiceMock;

    public AuthControllerTest()
    {
        _authServiceMock = new Mock<IAuthenticationService>();
    }

    [Test]
    public async Task Test_Login_ReturnsCorrectAuthenticationResponse()
    {
        // Arrange
        var controller = new AuthenticationController(_authServiceMock.Object);

        var request = new AuthenticationRequest("user1", "password123");

        var expectedResult = new AuthenticationResult(true, "user@email.com", "user1", "userToken");
        
        _authServiceMock.Setup(authService => authService.LoginAsync("user1", "password123"))
            .ReturnsAsync(expectedResult);
        
        //Act 
        var response = await controller.Authenticate(request);

        // Assert Step 1
        Assert.That(response.Result, Is.InstanceOf<OkObjectResult>());

        var okObjectResult = response.Result as OkObjectResult;
        Assert.IsNotNull(okObjectResult);

        var authenticationResponse = okObjectResult?.Value as AuthenticationResponse;
        Assert.IsNotNull(authenticationResponse);

        // Assert Step 2
        Assert.That(authenticationResponse?.UserName, Is.EqualTo("user1"));
        Assert.That(authenticationResponse?.Email, Is.EqualTo("user@email.com"));
        Assert.That(authenticationResponse?.Token, Is.EqualTo("userToken"));
    }
}