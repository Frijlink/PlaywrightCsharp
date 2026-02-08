using PlaywrightCsharp.Pages.Trello;

namespace PlaywrightCsharp.Playwright.Tests.UI;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginTests : BasePage
{
    [Test, Category("UI")]
    public async Task LogginInAndOutOnTrelloDotCom()
    {
        TrelloIndex trello = new(Page);

        await trello.homePage.GoTo();
        await trello.header.GetLoginButton().ClickAsync();
        await trello.loginPage.Login(USERNAME, PASSWORD);

        await Expect(trello.homePage.GetSectionHeader()).ToContainTextAsync("YOUR WORKSPACES");

        await trello.header.LogOut();
        await Expect(trello.header.GetLoginButton()).ToBeVisibleAsync();
    }
}