using Microsoft.Playwright;

namespace PlaywrightCsharp.SupportCode.Pages.Components;

public class HeaderComponent
{
    private readonly IPage _page;
    private readonly ILocator _logInBtn;
    private readonly ILocator _memberInfoBtn;
    private readonly ILocator _memberInfoLogoutBtn;
    private readonly ILocator _logoutSubmitBtn;
    private readonly ILocator _trelloBtn;

    public HeaderComponent(IPage page)
    {
        _page = page;
        _logInBtn = _page.Locator("css=header a[class^=\"Buttonsstyles__Button\"][href*=\"login?application=trello\"]");
        _memberInfoBtn = _page.GetByTestId("header-member-menu-button");
        _memberInfoLogoutBtn = _page.GetByTestId("account-menu-logout");
        _logoutSubmitBtn = _page.Locator("css=button#logout-submit");
        _trelloBtn = _page.GetByLabel("Back to home");
    }

    public ILocator GetLoginButton()
    {
        return _logInBtn;
    }

    public ILocator getTrelloBtn()
    {
        return _trelloBtn;
    }

    public async Task LogOut()
    {
        await _memberInfoBtn.ClickAsync();
        await _memberInfoLogoutBtn.ClickAsync();
        await _logoutSubmitBtn.ClickAsync();
    }
}