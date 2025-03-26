using PlaywrightCsharp.SupportCode.Api;
using static PlaywrightCsharp.SupportCode.Api.Context;
using static PlaywrightCsharp.SupportCode.Settings.Configuration;

namespace PlaywrightCsharp.Playwright.Tests.API;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class MembersTests : PageTest
{
    [Test, Category("API")]
    public async Task RetrieveAmountOfBoardsFromMember()
    {
        var requestContext = await CreateContext(Playwright.APIRequest);
        var API = new ApiIndex(requestContext);
        var token = GetEnvironmentVariable("TRELLO_API_TOKEN");
        var key = GetEnvironmentVariable("TRELLO_API_KEY");

        var boards = await API.membersApi.GetBoardsFromMember(key, token);
        Assert.That(boards.ToString(), Is.EqualTo("[]"));
    }
}