using Microsoft.Playwright.NUnit;
using PlaywrightCsharp.SupportCode.Pages.Trello;
using PlaywrightCsharp.SupportCode.Utilities;
using static PlaywrightCsharp.SupportCode.Settings.Configuration;

namespace PlaywrightCsharp.Playwright.Tests.UI;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class CreateAndDeleteTrelloBoardTests : PageTest
{
    TrelloIndex? trello;

    [SetUp]
    public async Task Init()
    {
        trello = new TrelloIndex(Page);

        await trello.homePage.GoTo();
        await trello.header.GetLoginButton().ClickAsync();
        await trello.loginPage.Login(
            GetEnvironmentVariable("TRELLO_USERNAME"),
            GetEnvironmentVariable("TRELLO_PASSWORD")
        );

        await Expect(trello.homePage.GetSectionHeader()).ToContainTextAsync("YOUR WORKSPACES");
    }

    [Test, Category("UI")]
    public async Task CreateAndDeleteTrelloBoard()
    {
        trello ??= new TrelloIndex(Page);

        var boardName = TestDataGenerator.GenerateBoardName();
        var updatedBoardName = TestDataGenerator.GenerateBoardName();

        // Create
        await trello.homePage.CreateNewBoard(boardName, "🌈");
        await trello.boardPage.WaitForPageLoaded();
        await trello.workSpaceNav.WaitForNav();

        await Expect(trello.boardPage.GetMainTitle()).ToContainTextAsync(boardName);

        // Update
        await trello.boardPage.UpdateBoardName(updatedBoardName);
        await Expect(trello.boardPage.GetMainTitle()).ToContainTextAsync(updatedBoardName);

        // Close
        await trello.workSpaceNav.CloseCurrentBoard();
        await Expect(trello.boardPage.GetCloseBoardMessage()).ToContainTextAsync("This board is closed. Reopen the board to make changes.");

        // Delete
        await trello.boardPage.DeleteBoard();

        // Don't see the board
        await trello.header.GetTrelloBtn().ClickAsync();
        await Expect(trello.homePage.GetSectionHeader()).ToContainTextAsync("YOUR WORKSPACES");
        // await Expect(await trello.homePage.GetAllBoardNames()).not.ToContainTextAsync(updatedBoardName);
        Console.WriteLine(await trello.homePage.GetAllBoardNames());
    }
}