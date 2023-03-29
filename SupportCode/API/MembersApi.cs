using Microsoft.Playwright;
using static Configuration;

public class MembersApi
{
    private readonly IAPIRequestContext _request;
    HeaderConstructor headers = new HeaderConstructor();

    public MembersApi(IAPIRequestContext request)
    {
        _request = request;
    }

    public async Task<System.Text.Json.JsonElement> GetBoardsFromMember(string apiKey, string apiToken)
    {
        var url = $"/1/members/me/boards?key={apiKey}&token={apiToken}";
        headers.AddHeaders("Accept", "application/json");
        var response = await _request.GetAsync(url, new() {
            Headers = headers.GetHeaders()
        });
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }

    public async Task<System.Text.Json.JsonElement> GetMemberOrganizations(string memberId, string apiKey, string apiToken)
    {
        var url = $"{GetEnvironmentVariable("TRELLO_API_URL")}/1/members/{memberId}/organizations?key={apiKey}&token={apiToken}";
        headers.AddHeaders("Accept", "application/json");
        var response = await _request.GetAsync(url, new() {
            Headers = headers.GetHeaders()
        });
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }
}