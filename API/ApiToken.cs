using PlaywrightCsharp.Playwright.Tests;

namespace PlaywrightCsharp.Api;

public class ApiToken(IAPIRequestContext request)
{
    private readonly IAPIRequestContext _request = request;
    readonly HeaderConstructor headers = new();

    public async Task<System.Text.Json.JsonElement> GetTokenInfo(string apiKey, string apiToken)
    {
        var url = $"{BasePage.API_URL}/1/tokens/{apiToken}?key={apiKey}&token={apiToken}";
        headers.AddHeaders("Accept", "application/json");
        var response = await _request.GetAsync(url, new() {
            Headers = headers.GetHeaders()
        });
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }
}