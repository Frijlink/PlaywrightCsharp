using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task LogginInAndOutOnTrelloDotCom()
    {
        TrelloIndex trello = new TrelloIndex(Page);

        await trello.homePage.GoTo();
        await trello.header.GetLoginButton().ClickAsync();
        await trello.loginPage.Login(
            "tmp",
            "tmp"
        );

        await Expect(trello.homePage.GetSectionHeader()).ToContainTextAsync("YOUR WORKSPACES");

        await trello.header.LogOut();
        await Expect(trello.header.GetLoginButton()).ToBeVisibleAsync();
    }
}