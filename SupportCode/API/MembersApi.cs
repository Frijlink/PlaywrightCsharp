using Microsoft.Playwright;

public class MembersApi
{
    private readonly IAPIRequestContext _request;
    MyConfig config = new MyConfig();
    HeaderConstructor headers = new HeaderConstructor();

    public MembersApi(IAPIRequestContext request)
    {
        _request = request;
    }

    public async Task<System.Text.Json.JsonElement> GetBoardsFromMember(string apiKey, string apiToken)
    {
        string url = $"/1/members/me/boards?key={apiKey}&token={apiToken}";
        headers.AddHeaders("Accept", "application/json");
        IAPIResponse response = await _request.GetAsync(url, new() {
            Headers = headers.GetHeaders()
        });
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }

    public async Task<System.Text.Json.JsonElement> GetMemberOrganizations(string memberId, string apiKey, string apiToken)
    {
        string url = $"{config.API_URL}/1/members/{memberId}/organizations?key={apiKey}&token={apiToken}";
        headers.AddHeaders("Accept", "application/json");
        IAPIResponse response = await _request.GetAsync(url, new() {
            Headers = headers.GetHeaders()
        });
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }
}