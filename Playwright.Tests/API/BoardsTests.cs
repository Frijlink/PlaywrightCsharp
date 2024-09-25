using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using PlaywrightCsharp.SupportCode.Api;
using PlaywrightCsharp.SupportCode.Utilities;
using static PlaywrightCsharp.SupportCode.Api.Context;
using static PlaywrightCsharp.SupportCode.Settings.Configuration;

namespace PlaywrightTests.API;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class BoardsTests : PageTest
{
    private IAPIRequestContext? requestContext;
    private ApiIndex? API;
    private string token = string.Empty;
    private string key = string.Empty;
    private string boardId = string.Empty;
    private string organizationId = string.Empty;

    [SetUp]
    public async Task Init()
    {
        requestContext = await CreateContext(Playwright.APIRequest);
        API = new ApiIndex(requestContext);
        token = GetEnvironmentVariable("TRELLO_API_TOKEN");
        key = GetEnvironmentVariable("TRELLO_API_KEY");
        var tokenInfo = await API.apiToken.GetTokenInfo(key, token);
        var memberId = tokenInfo.GetProperty("idMember").ToString();
        var orgs = await API.membersApi.GetMemberOrganizations(memberId, key, token);
        organizationId = orgs[0].GetProperty("id").ToString();
    }

    [TearDown]
    public async Task DeleteAllBoards()
    {
        requestContext ??= await CreateContext(Playwright.APIRequest);
        API ??= new ApiIndex(requestContext);

        var boards = await API.membersApi.GetBoardsFromMember(key, token);
        foreach (var board in boards.EnumerateArray())
        {
            var id = board.GetProperty("id").ToString();
            var responseStatus = await API.boardsApi.DeleteBoard(id, key, token);
            Assert.That(responseStatus, Is.EqualTo(200));
        }
    }

    [Test, Category("API")]
    public async Task CreateAndDeleteTrelloBoardThroughApi()
    {
        requestContext ??= await CreateContext(Playwright.APIRequest);
        API ??= new ApiIndex(requestContext);

        var boardName = TestDataGenerator.GenerateBoardName();
        var updatedBoardName = TestDataGenerator.GenerateBoardName();
        var backgroundColour = "purple";
        var updatedBackgroundColour = "pink";
        var visibility = "org";
        var updatedVisibility = "private";

        // Create Board
        var responseBodyCreate = await API.boardsApi.CreateBoard(key, token, boardName, backgroundColour, visibility);
        var respCreatePrefs = responseBodyCreate.GetProperty("prefs");
        boardId = responseBodyCreate.GetProperty("id").ToString();
        Assert.That(responseBodyCreate.GetProperty("idOrganization").ToString(), Is.EqualTo(organizationId));
        Assert.That(responseBodyCreate.GetProperty("name").ToString(), Is.EqualTo(boardName));
        Assert.That(responseBodyCreate.GetProperty("closed").ToString(), Is.EqualTo("False"));
        Assert.That(respCreatePrefs.GetProperty("background").ToString(), Is.EqualTo(backgroundColour));
        Assert.That(respCreatePrefs.GetProperty("permissionLevel").ToString(), Is.EqualTo(visibility));

        // Read Board
        var responseBodyRead = await API.boardsApi.GetBoard(boardId, key, token);
        var respReadPrefs = responseBodyCreate.GetProperty("prefs");
        Assert.That(responseBodyRead.GetProperty("idOrganization").ToString(), Is.EqualTo(organizationId));
        Assert.That(responseBodyRead.GetProperty("name").ToString(), Is.EqualTo(boardName));
        Assert.That(responseBodyRead.GetProperty("closed").ToString(), Is.EqualTo("False"));
        Assert.That(respReadPrefs.GetProperty("background").ToString(), Is.EqualTo(backgroundColour));
        Assert.That(respReadPrefs.GetProperty("permissionLevel").ToString(), Is.EqualTo(visibility));

        // Update Board
        var parameters = new Dictionary<string, object>
        {
            { "key", key },
            { "token", token },
            { "name", updatedBoardName },
            { "prefs/background", updatedBackgroundColour },
            { "prefs/visibility", updatedVisibility }
        };
        var responseBodyUpdate = await API.boardsApi.UpdateBoard(boardId, parameters);
        var respUpdatePrefs = responseBodyUpdate.GetProperty("prefs");
        Assert.That(responseBodyUpdate.GetProperty("idOrganization").ToString(), Is.EqualTo(organizationId));
        Assert.That(responseBodyUpdate.GetProperty("name").ToString(), Is.EqualTo(updatedBoardName));
        Assert.That(responseBodyUpdate.GetProperty("closed").ToString(), Is.EqualTo("False"));
        Assert.That(respUpdatePrefs.GetProperty("background").ToString(), Is.EqualTo(updatedBackgroundColour));
        // Assert.That(respUpdatePrefs.GetProperty("permissionLevel").ToString(), Is.EqualTo(updatedVisibility));

        // Close Board
        var closeBoardParams = new Dictionary<string, object>
        {
            { "key", key },
            { "token", token },
            { "closed", "true" }
        };
        await API.boardsApi.UpdateBoard(boardId, closeBoardParams);
        var responseBodyClose = await API.boardsApi.GetBoard(boardId, key, token);
        Assert.That(responseBodyClose.GetProperty("closed").ToString(), Is.EqualTo("True"));


        // Delete Board
        var responseStatusDelete = await API.boardsApi.DeleteBoard(boardId, key, token);
        Assert.That(responseStatusDelete, Is.EqualTo(200));
    }
}