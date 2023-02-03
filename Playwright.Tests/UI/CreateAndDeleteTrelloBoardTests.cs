using Microsoft.Playwright.NUnit;
using static Configuration;

namespace PlaywrightTests;

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

    [Test]
    public async Task CreateAndDeleteTrelloBoard()
    {
        if (trello is null) trello = new TrelloIndex(Page);

        var dataGenerator = new TestDataGenerator();
        var boardName = dataGenerator.GenerateBoardName();
        var updatedBoardName = dataGenerator.GenerateBoardName();

        // Create
        await trello.homePage.CreateNewBoard(boardName, "Purple");
        await trello.boardPage.WaitForPageLoaded();
        await trello.workSpaceNav.waitForNav();

        await Expect(trello.boardPage.GetMainTitle()).ToContainTextAsync(boardName);

        // Update
        await trello.boardPage.UpdateBoardName(updatedBoardName);
        await Expect(trello.boardPage.GetMainTitle()).ToContainTextAsync(updatedBoardName);

        // Close
        await trello.workSpaceNav.CloseCurrentBoard();
        await Expect(trello.boardPage.GetCloseBoardMessage()).ToContainTextAsync($"{updatedBoardName} is closed.");

        // Delete
        await trello.boardPage.DeleteBoard();

        await Expect(trello.homePage.GetSectionHeader()).ToContainTextAsync("YOUR WORKSPACES");
        // await Expect(await trello.homePage.GetAllBoardNames()).not.ToContainTextAsync(updatedBoardName);
        Console.WriteLine(await trello.homePage.GetAllBoardNames());
    }
}