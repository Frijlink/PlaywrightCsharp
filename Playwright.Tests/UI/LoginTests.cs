using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginTests : PageTest
{
    [Test]
    public async Task LogginInAndOutOnTrelloDotCom()
    {
        MyConfig config = new MyConfig();
        TrelloIndex trello = new TrelloIndex(Page);

        await trello.homePage.GoTo();
        await trello.header.GetLoginButton().ClickAsync();
        await trello.loginPage.Login(
            config.USER_NAME,
            config.PASSWORD
        );

        await Expect(trello.homePage.GetSectionHeader()).ToContainTextAsync("YOUR WORKSPACES");

        await trello.header.LogOut();
        await Expect(trello.header.GetLoginButton()).ToBeVisibleAsync();
    }
}