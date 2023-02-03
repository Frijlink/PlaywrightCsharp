using Microsoft.Playwright;

public class HeaderComponent
{
    private readonly IPage _page;
    private readonly ILocator _logInBtn;
    private readonly ILocator _memberInfoBtn;
    private readonly ILocator _memberInfoLogoutBtn;
    private readonly ILocator _logoutSubmitBtn;

    public HeaderComponent(IPage page)
    {
        _page = page;
        _logInBtn = page.Locator("css=a[class^=\"Buttonsstyles\"][href=\"/login\"]");
        _memberInfoBtn = page.Locator("css=[data-testid=\"header-member-menu-button\"]");
        _memberInfoLogoutBtn = page.Locator("css=[data-testid=\"account-menu-logout\"]");
        _logoutSubmitBtn = page.Locator("css=button#logout-submit");
    }

    public ILocator GetLoginButton()
    {
        return _logInBtn;
    }

    public async Task LogOut()
    {
        await _memberInfoBtn.ClickAsync();
        await _memberInfoLogoutBtn.ClickAsync();
        await _logoutSubmitBtn.ClickAsync();
    }
}