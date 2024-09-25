using Microsoft.Playwright;
using PlaywrightCsharp.SupportCode.Pages.Components;

namespace PlaywrightCsharp.SupportCode.Pages.Trello;

public class TrelloIndex {
    private readonly IPage _page;
    public LoginPage loginPage;
    public HomePage homePage;
    public HeaderComponent header;
    public BoardPage boardPage;
    public WorkspaceNavComponent workSpaceNav;

    public TrelloIndex(IPage page) {
        _page = page;
        loginPage = new LoginPage(_page);
        homePage = new HomePage(_page);
        header = new HeaderComponent(_page);
        boardPage = new BoardPage(_page);
        workSpaceNav = new WorkspaceNavComponent(_page);
    }
}