
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using PlatformConsoleApp.DTOs;

namespace PlatformConsoleApp.Services;

public class AuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AuthenticationService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> LoginAsync()
    {
        // 1. Retrieve credentials from appsettings.json
        var username = _configuration["ApiCredentials:Username"];
        var password = _configuration["ApiCredentials:Password"];

        if (string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                "API username or password is missing."
            );
        }

        // 2. Create login request
        var loginRequest = new LoginRequestDto
        {
            Username = username,
            Password = password
        };

        // 3. Retrieve login endpoint
        var endpoint = _configuration["ApiSettings:LoginEndpoint"];

        if (string.IsNullOrWhiteSpace(endpoint))
        {
            throw new InvalidOperationException(
                "Login endpoint configuration is missing."
            );
        }

        Console.WriteLine("Sending login request...");

        // 4. Send POST request
        using var response = await _httpClient.PostAsJsonAsync(
            endpoint,
            loginRequest
        );

        // 5. Check HTTP response
        response.EnsureSuccessStatusCode();

        // 6. Deserialize JSON string containing JWT
        var token = await response.Content.ReadFromJsonAsync<string>();

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new InvalidOperationException(
                "Login succeeded but no token was returned."
            );
        }

        Console.WriteLine("Authentication successful!");

        // 7. Return JWT token
        return token;
    }
}
