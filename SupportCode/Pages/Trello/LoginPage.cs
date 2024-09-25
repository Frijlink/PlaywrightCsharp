using Microsoft.Playwright;

namespace PlaywrightCsharp.SupportCode.Pages.Trello;

public class LoginPage
{
    private readonly IPage _page;
    private readonly ILocator _userInput;
    private readonly ILocator _passwordInput;
    private readonly ILocator _loginSubmitBtn;

    public LoginPage(IPage page)
    {
        _page = page;
        _userInput = _page.GetByTestId("username");
        _passwordInput = _page.GetByTestId("password");
        _loginSubmitBtn = _page.Locator("css=button#login-submit");
    }

    public async Task Login(string user, string password)
    {
        await _userInput.FillAsync(user);
        await _loginSubmitBtn.ClickAsync();
        await _passwordInput.FillAsync(password);
        await _loginSubmitBtn.ClickAsync();
    }
}