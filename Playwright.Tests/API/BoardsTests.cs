using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class BoardsTests : PageTest
{
    MyConfig config = new MyConfig();
    private IAPIRequestContext? requestContext;
    private ApiIndex? API;
    private string token = "";
    private string key = "";
    private string boardId = "";
    private string organizationId = "";

    [SetUp]
    public async Task Init()
    {
        requestContext = await CreateContext();
        API = new ApiIndex(requestContext);
        token = config.API_TOKEN;
        key = config.API_KEY;
        System.Text.Json.JsonElement tokenInfo = await API.apiToken.GetTokenInfo(key, token);
        string memberId = tokenInfo.GetProperty("idMember").ToString();
        System.Text.Json.JsonElement orgs = await API.membersApi.GetMemberOrganizations(memberId, key, token);
        organizationId = orgs[0].GetProperty("id").ToString();
    }

    [TearDown]
    public async Task DeleteAllBoards()
    {
        if (requestContext == null) requestContext = await CreateContext();
        if (API == null) API = new ApiIndex(requestContext);
        var boards = await API.membersApi.GetBoardsFromMember(key, token);
        foreach (var board in boards.EnumerateArray())
        {
            var responseStat = await API.boardsApi.DeleteBoard(board.ToString(), key, token);
            Assert.AreEqual(responseStat, 20);
        }
    }

    [Test]
    public async Task CreateAndDeleteTrelloBoardThroughApi()
    {
        if (requestContext == null) requestContext = await CreateContext();
        if (API == null) API = new ApiIndex(requestContext);

        TestDataGenerator generator = new TestDataGenerator();
        string boardName = generator.GenerateBoardName();
        string updatedBoardName = generator.GenerateBoardName();
        string backgroundColour = "purple";
        string updatedBackgroundColour = "pink";
        string visibility = "org";
        string updatedVisibility = "private";

        // Create Board
        var responseBodyCreate = await API.boardsApi.CreateBoard(key, token, boardName, backgroundColour, visibility);
        var respCreatePrefs = responseBodyCreate.GetProperty("prefs");
        boardId = responseBodyCreate.GetProperty("id").ToString();
        StringAssert.AreEqualIgnoringCase(responseBodyCreate.GetProperty("idOrganization").ToString(), organizationId);
        StringAssert.AreEqualIgnoringCase(responseBodyCreate.GetProperty("name").ToString(), boardName);
        StringAssert.AreEqualIgnoringCase(responseBodyCreate.GetProperty("closed").ToString(), "false");
        StringAssert.AreEqualIgnoringCase(respCreatePrefs.GetProperty("background").ToString(), backgroundColour);
        StringAssert.AreEqualIgnoringCase(respCreatePrefs.GetProperty("permissionLevel").ToString(), visibility);

        // Read Board
        var responseBodyRead = await API.boardsApi.GetBoard(boardId, key, token);
        var respReadPrefs = responseBodyCreate.GetProperty("prefs");
        StringAssert.AreEqualIgnoringCase(responseBodyRead.GetProperty("idOrganization").ToString(), organizationId);
        StringAssert.AreEqualIgnoringCase(responseBodyRead.GetProperty("name").ToString(), boardName);
        StringAssert.AreEqualIgnoringCase(responseBodyRead.GetProperty("closed").ToString(), "false");
        StringAssert.AreEqualIgnoringCase(respReadPrefs.GetProperty("background").ToString(), backgroundColour);
        StringAssert.AreEqualIgnoringCase(respReadPrefs.GetProperty("permissionLevel").ToString(), visibility);

        // Update Board
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("key", key);
        parameters.Add("token", token);
        parameters.Add("name", updatedBoardName);
        parameters.Add("prefs/background", updatedBackgroundColour);
        parameters.Add("prefs/visibility", updatedVisibility);
        var responseBodyUpdate = await API.boardsApi.UpdateBoard(boardId, parameters);
        var respUpdatePrefs = responseBodyUpdate.GetProperty("prefs");
        StringAssert.AreEqualIgnoringCase(responseBodyUpdate.GetProperty("idOrganization").ToString(), organizationId);
        StringAssert.AreEqualIgnoringCase(responseBodyUpdate.GetProperty("name").ToString(), updatedBoardName);
        StringAssert.AreEqualIgnoringCase(responseBodyUpdate.GetProperty("closed").ToString(), "false");
        StringAssert.AreEqualIgnoringCase(respUpdatePrefs.GetProperty("background").ToString(), updatedBackgroundColour);
        // StringAssert.AreEqualIgnoringCase(respUpdatePrefs.GetProperty("permissionLevel").ToString(), updatedVisibility);

        // Close Board
        Dictionary<string, object> closeBoardParams = new Dictionary<string, object>();
        closeBoardParams.Add("key", key);
        closeBoardParams.Add("token", token);
        closeBoardParams.Add("closed", "true");
        await API.boardsApi.UpdateBoard(boardId, closeBoardParams);
        var responseBodyClose = await API.boardsApi.GetBoard(boardId, key, token);
        StringAssert.AreEqualIgnoringCase(responseBodyClose.GetProperty("closed").ToString(), "true");

        // Deletet Board
        var responseStatusDelete = await API.boardsApi.DeleteBoard(boardId, key, token);
        Assert.AreEqual(responseStatusDelete, 200);
    }

    public async Task<IAPIRequestContext> CreateContext()
    {
        HeaderConstructor headers = new HeaderConstructor();
        headers.AddHeaders("Accept", "application/json");
        return await this.Playwright.APIRequest.NewContextAsync(new() {
            BaseURL = config.API_URL,
            ExtraHTTPHeaders = headers.GetHeaders(),
        });
    }
}