using Microsoft.Playwright;
using static Configuration;

public class ApiToken
{
    private readonly IAPIRequestContext _request;
    readonly HeaderConstructor headers = new();

    public ApiToken(IAPIRequestContext request)
    {
        _request = request;
    }

    public async Task<System.Text.Json.JsonElement> GetTokenInfo(string apiKey, string apiToken)
    {
        var url = $"{GetEnvironmentVariable("TRELLO_API_URL")}/1/tokens/{apiToken}?key={apiKey}&token={apiToken}";
        headers.AddHeaders("Accept", "application/json");
        var response = await _request.GetAsync(url, new() {
            Headers = headers.GetHeaders()
        });
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }
}