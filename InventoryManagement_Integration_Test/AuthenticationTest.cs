using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Inventory_Management_System.Contracts;
using Inventory_Management_System.Model.Enums;
using Inventory_Management_System.Model.Good;
using Inventory_Management_System.Model.HandlingUnit;
using Inventory_Management_System.Model.Location;
using InventoryManagement_Integration_Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        //Register userSetRoleTest -> alapból user role -> átállítom Forklift Driverré
        var loginRequest = new AuthenticationRequest("admin", "Admin123");
        
        var loginResponse = await _client.PostAsync("/Authentication/Login",
            new StringContent(JsonConvert.SerializeObject(loginRequest), 
                Encoding.UTF8, "application/json"));

        var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await loginResponse.Content.ReadAsStringAsync());
        var adminToken = authResponse.Token;
        Assert.NotNull(authResponse.Token);
        Assert.Equal("admin@admin.com", authResponse.Email);
        Assert.Equal("admin", authResponse.UserName);

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
        

        //Role request user1-re, az alapján változtassuk, a user1-et lekell képezni a testek elején
        var setRoleRequest = new SetRoleRequest("user1", "Forklift Driver");

        var setRoleResponse = await _client.PatchAsync("/Admin/SetRole", new StringContent(JsonConvert.SerializeObject(setRoleRequest), 
            Encoding.UTF8, "application/json"));
        
        //setRoleResponse.EnsureSuccessStatusCode();
        //_output.WriteLine(setRoleResponse);
        Assert.Equal(HttpStatusCode.OK, setRoleResponse.StatusCode);

        var roleRequest = new RoleRequest("user1");

        var roleResponse = await _client.PostAsync(
            "/Authentication/Roles",
            new StringContent(JsonConvert.SerializeObject(roleRequest),
                Encoding.UTF8,
                "application/json"));
        
        var roleResponseStrArr = JsonConvert.DeserializeObject<string[]>(await roleResponse.Content.ReadAsStringAsync());
        
        Assert.Equal("Forklift Driver",roleResponseStrArr![0]);
    }

    [Fact]
    public async Task Test_NoAuthorization()
    {
        var loginRequest = new AuthenticationRequest("user1", "password1");
        
        var loginResponse = await _client.PostAsync("/Authentication/Login",
            new StringContent(JsonConvert.SerializeObject(loginRequest), 
                Encoding.UTF8, "application/json"));
        var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await loginResponse.Content.ReadAsStringAsync());
        Assert.NotNull(authResponse.Token);
        var userToken = authResponse.Token;
        
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
        
        var setRoleRequest = new SetRoleRequest("user2", "Forklift Driver");

        var setRoleResponse = await _client.PatchAsync("/Admin/SetRole", new StringContent(JsonConvert.SerializeObject(setRoleRequest), 
            Encoding.UTF8, "application/json"));
        
        Assert.Equal(HttpStatusCode.Forbidden, setRoleResponse.StatusCode);
    }

    [Fact]
    public async Task Test_Forklift_Driver()
    {
        //put component stock in DB
        var loginRequest = new AuthenticationRequest("user1", "password1");
        
        var loginResponse = await _client.PostAsync("/Authentication/Login",
            new StringContent(JsonConvert.SerializeObject(loginRequest), 
                Encoding.UTF8, "application/json"));
        var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(await loginResponse.Content.ReadAsStringAsync());
        Assert.NotNull(authResponse.Token);
        var userToken = authResponse.Token;
        
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        var forkLiftDriverResponse = await _client.GetAsync("/ForkliftDriver/GetComponentStock");
        
        var boxResponse = JsonConvert.DeserializeObject<List<ComponentLocation>>(await forkLiftDriverResponse.Content.ReadAsStringAsync());

        if (boxResponse.IsNullOrEmpty())
        {
            Assert.Empty(boxResponse);
        }else{
            var firstComponentLocationType = boxResponse?.First();
            Assert.Equal(LocationType.RawMaterial, firstComponentLocationType.LocationType);
        }
    }
    
    public void Dispose()
    {
        _factory.Dispose();
        _client.Dispose();
    }
}