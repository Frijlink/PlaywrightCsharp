using static PlaywrightCsharp.SupportCode.Settings.Configuration;

namespace PlaywrightCsharp.SupportCode.Api;

public class Context
{
    public static async Task<IAPIRequestContext> CreateContext(IAPIRequest apiRequest)
    {
        var headers = new HeaderConstructor();
        headers.AddHeaders("Accept", "application/json");
        return await apiRequest.NewContextAsync(new() {
            BaseURL = GetEnvironmentVariable("TRELLO_API_URL"),
            ExtraHTTPHeaders = headers.GetHeaders(),
        });
    }
}