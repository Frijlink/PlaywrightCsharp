using PlaywrightCsharp.Playwright.Tests;

namespace PlaywrightCsharp.Api;

public class Context
{
    public static async Task<IAPIRequestContext> CreateContext(IAPIRequest apiRequest)
    {
        var headers = new HeaderConstructor();
        headers.AddHeaders("Accept", "application/json");
        return await apiRequest.NewContextAsync(new() {
            BaseURL = BasePage.API_URL,
            ExtraHTTPHeaders = headers.GetHeaders(),
        });
    }
}