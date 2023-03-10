using Microsoft.Playwright;

public class LoginPage
{
    private readonly IPage _page;
    private readonly ILocator _userInput;
    private readonly ILocator _passwordInput;
    private readonly ILocator _loginBtn;
    private readonly ILocator _loginSubmitBtn;

    public LoginPage(IPage page)
    {
        _page = page;
        _userInput = page.Locator("css=input#user");
        _passwordInput = page.Locator("css=input#password");
        _loginBtn = page.Locator("css=input#login");
        _loginSubmitBtn = page.Locator("css=button#login-submit");
    }

    public async Task Login(string user, string password)
    {
        await _userInput.FillAsync(user);
        await _loginBtn.ClickAsync();
        await _passwordInput.FillAsync(password);
        await _loginSubmitBtn.ClickAsync();
    }
}