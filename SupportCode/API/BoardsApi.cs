using Microsoft.Playwright;

public class BoardsApi
{
    private readonly IAPIRequestContext _request;
    HeaderConstructor headers = new HeaderConstructor();

    public BoardsApi(IAPIRequestContext request)
    {
        _request = request;
    }

    public async Task<System.Text.Json.JsonElement> CreateBoard(
        string apiKey,
        string apiToken,
        string name,
        string colour,
        string visibility
    ) {
        var url = "/1/boards/";
        headers.AddHeaders("Accept", "application/json");
        var data = new Dictionary<string, object>() {
            { "name", name },
            { "key", apiKey },
            { "token", apiToken },
            { "prefs_background", colour },
            { "prefs_permissionLevel", visibility }
        };
        var response = await _request.PostAsync(url, new()
            {
                Headers = headers.GetHeaders(),
                DataObject = data
            }
        );
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }

    public async Task<System.Text.Json.JsonElement> GetBoard(string boardId, string key, string token)
    {
        var url = $"/1/boards/{boardId}?key={key}&token={token}";
        var response = await _request.GetAsync(url, new()
            {
                Headers = headers.GetHeaders(),
            }
        );
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }

    public async Task<System.Text.Json.JsonElement> UpdateBoard(
        string id,
        Dictionary<string, object> parameters
    ) {
        var url = $"/1/boards/{id}";
        var response = await _request.PutAsync(url, new()
            {
                Headers = headers.GetHeaders(),
                Params = parameters
            }
        );
        return (System.Text.Json.JsonElement)await response.JsonAsync();
    }

    public async Task<int> DeleteBoard(string boardId, string key, string token) {
        var url = $"/1/boards/{boardId}?key={key}&token={token}";
        var response = await _request.DeleteAsync(url, new()
            {
                Headers = headers.GetHeaders()
            }
        );
        return response.Status;
    }
}
