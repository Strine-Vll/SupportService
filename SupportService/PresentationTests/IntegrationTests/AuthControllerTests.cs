using Application.Dtos.UserDtos;
using Newtonsoft.Json;
using PresentationTests.BaseIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PresentationTests.IntegrationTests;

[TestFixture]
public class AuthControllerTests : BaseIntegrationTest
{
    [Test]
    public async Task Register_ShouldReturnOk_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Name = "Test1 User",
            Email = "test1@example.com",
            Password = "Password123"
        };

        var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/auth", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    public async Task Login_ShouldReturnOkWithToken_WhenCredentialsAreValid()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Name = "Test2 User",
            Email = "test2@example.com",
            Password = "Password123"
        };

        var registerContent = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");
        var registerResponse = await _client.PostAsync("/api/auth", registerContent);

        Assert.AreEqual(HttpStatusCode.OK, registerResponse.StatusCode);

        var authenticationRequest = new AuthenticationRequest
        {
            Email = "test2@example.com",
            Password = "Password123"
        };

        var loginContent = new StringContent(JsonConvert.SerializeObject(authenticationRequest), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/auth/login", loginContent);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        var responseData = await response.Content.ReadAsStringAsync();
        var tokenObj = JsonConvert.DeserializeObject<dynamic>(responseData);
        Assert.IsNotNull(tokenObj.token);
    }
}
