using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using static Configuration;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class MembersTests : PageTest
{
    [Test]
    public async Task RetrieveAmountOfBoardsFromMember()
    {
        var requestContext = await CreateContext();
        var API = new ApiIndex(requestContext);
        var token = GetEnvironmentVariable("TRELLO_API_TOKEN");
        var key = GetEnvironmentVariable("TRELLO_API_KEY");

        var boards = await API.membersApi.GetBoardsFromMember(key, token);
        Assert.That(boards.ToString() == "[]");
    }

    public async Task<IAPIRequestContext> CreateContext()
    {
        var headers = new HeaderConstructor();
        headers.AddHeaders("Accept", "application/json");
        return await this.Playwright.APIRequest.NewContextAsync(new() {
            BaseURL = GetEnvironmentVariable("TRELLO_API_URL"),
            ExtraHTTPHeaders = headers.GetHeaders(),
        });
    }
}