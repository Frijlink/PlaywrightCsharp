using Microsoft.Playwright;

public class WorkspaceNavComponent
{
    private readonly IPage _page;
    private readonly ILocator _nav;
    private readonly ILocator _currentBoard;
    private readonly ILocator _boardActionsMenuBtn;
    private readonly ILocator _closeBoardBtn;
    private readonly ILocator _closeBtn;

    public WorkspaceNavComponent(IPage page)
    {
        _page = page;
        _nav = _page.Locator("css=nav[data-testid=\"workspace-navigation-nav\"]");
        _currentBoard = _page.Locator("css=[aria-label$=\"(currently active)\"]");
        _boardActionsMenuBtn = _page.Locator("css=[aria-label=\"Board actions menu\"]");
        _closeBoardBtn = _page.Locator("css=[aria-label=\"Close board\"]");
        _closeBtn = _page.Locator("css=[title=\"Close\"]");
    }

    public async Task WaitForNav()
    {
        await _nav.WaitForAsync();
    }

    public async Task CloseCurrentBoard()
    {
        await _currentBoard.HoverAsync();
        await _boardActionsMenuBtn.ClickAsync();
        await _closeBoardBtn.ClickAsync();
        await _closeBtn.ClickAsync();
    }
}