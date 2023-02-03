using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class MembersTests : PageTest
{
    MyConfig config = new MyConfig();

    [Test]
    public async Task RetrieveAmountOfBoardsFromMember()
    {
        var requestContext = await CreateContext();
        var API = new ApiIndex(requestContext);
        var token = config.API_TOKEN;
        var key = config.API_KEY;

        var boards = await API.membersApi.GetBoardsFromMember(key, token);
        Assert.That(boards.ToString() == "[]");
    }

    public async Task<IAPIRequestContext> CreateContext()
    {
        var headers = new HeaderConstructor();
        headers.AddHeaders("Accept", "application/json");
        return await this.Playwright.APIRequest.NewContextAsync(new() {
            BaseURL = config.API_URL,
            ExtraHTTPHeaders = headers.GetHeaders(),
        });
    }
}