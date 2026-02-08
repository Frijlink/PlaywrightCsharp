using PlaywrightCsharp.Playwright.Tests;

namespace PlaywrightCsharp.Api;

public class MembersApi(IAPIRequestContext request)
{
    readonly HeaderConstructor headers = new();

    public async Task<System.Text.Json.JsonElement> GetBoardsFromMember(string apiKey, string apiToken)
    {
        var url = $"/1/members/me/boards?key={apiKey}&token={apiToken}";
        headers.AddHeaders("Accept", "application/json");
        var response = await request.GetAsync(url, new() {
            Headers = headers.GetHeaders()
        });
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }

    public async Task<System.Text.Json.JsonElement> GetMemberOrganizations(string memberId, string apiKey, string apiToken)
    {
        var url = $"{BasePage.API_URL}/1/members/{memberId}/organizations?key={apiKey}&token={apiToken}";
        headers.AddHeaders("Accept", "application/json");
        var response = await request.GetAsync(url, new() {
            Headers = headers.GetHeaders()
        });
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }
}