using PlaywrightCsharp.Api;
using static PlaywrightCsharp.Api.Context;

namespace PlaywrightCsharp.Playwright.Tests.API;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class MembersTests : BasePage
{
    [Test, Category("API")]
    public async Task RetrieveAmountOfBoardsFromMember()
    {
        var requestContext = await CreateContext(Playwright.APIRequest);
        var API = new ApiIndex(requestContext);

        var boards = await API.membersApi.GetBoardsFromMember(KEY, TOKEN);
        Assert.That(boards.ToString(), Is.EqualTo("[]"));
    }
}