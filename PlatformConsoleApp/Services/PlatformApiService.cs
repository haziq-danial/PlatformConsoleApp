
using Microsoft.Extensions.Configuration;
using PlatformConsoleApp.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PlatformConsoleApp.Services;

public class PlatformApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public PlatformApiService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<List<PlatformDto>> GetPlatformWellActualAsync(
        string token)
    {
        return await GetPlatformsAsync(
            token,
            "ApiSettings:ActualEndpoint"
        );
    }

    public async Task<List<PlatformDto>> GetPlatformWellDummyAsync(
        string token)
    {
        return await GetPlatformsAsync(
            token,
            "ApiSettings:DummyEndpoint"
        );
    }

    private async Task<List<PlatformDto>> GetPlatformsAsync(
        string token,
        string endpointKey)
    {
        // 1. Retrieve the endpoint from appsettings.json
        var endpoint = _configuration[endpointKey];

        if (string.IsNullOrWhiteSpace(endpoint))
        {
            throw new InvalidOperationException(
                $"Endpoint configuration '{endpointKey}' is missing."
            );
        }

        // 2. Create the HTTP GET request
        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            endpoint
        );

        // 3. Attach JWT authentication
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        // 4. Display request diagnostics
        Console.WriteLine($"Request URL: {_httpClient.BaseAddress}{endpoint}");
        Console.WriteLine(
            $"Authorization scheme: {request.Headers.Authorization?.Scheme}"
        );
        Console.WriteLine(
            $"Token provided: {!string.IsNullOrWhiteSpace(token)}"
        );

        // 5. Send the request
        using var response = await _httpClient.SendAsync(request);

        Console.WriteLine(
            $"Response Status: {(int)response.StatusCode} ({response.StatusCode})"
        );

        // 6. Handle unsuccessful responses
        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine("API Error Response:");
            Console.WriteLine(errorBody);

            throw new HttpRequestException(
                $"API request failed: {response.StatusCode}"
            );
        }

        // 7. Deserialize the JSON response into PlatformDto objects
        var platforms = await response.Content
            .ReadFromJsonAsync<List<PlatformDto>>();

        Console.WriteLine(
            $"Platforms retrieved: {platforms?.Count ?? 0}"
        );

        // 8. Return the platform list
        return platforms ?? new List<PlatformDto>();
    }
}
