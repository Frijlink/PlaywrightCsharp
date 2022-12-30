using Microsoft.Playwright;

public class ApiToken
{
    private readonly IAPIRequestContext _request;
    HeaderConstructor headers = new HeaderConstructor();

    public ApiToken(IAPIRequestContext request)
    {
        _request = request;
    }

    public async Task<System.Text.Json.JsonElement> GetTokenInfo(string apiKey, string apiToken)
    {
        MyConfig config = new MyConfig();
        string url = $"{config.API_URL}/1/tokens/{apiToken}?key={apiKey}&token={apiToken}";
        headers.AddHeaders("Accept", "application/json");
        IAPIResponse response = await _request.GetAsync(url, new() {
            Headers = headers.GetHeaders()
        });
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }
}