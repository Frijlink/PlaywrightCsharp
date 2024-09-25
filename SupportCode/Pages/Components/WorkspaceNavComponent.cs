using Microsoft.Playwright;

namespace PlaywrightCsharp.SupportCode.Pages.Components;

public class WorkspaceNavComponent
{
    private readonly IPage _page;
    private readonly ILocator _nav;
    private readonly ILocator _currentBoard;
    private readonly ILocator _boardActionsMenuBtn;
    private readonly ILocator _boardMenu;
    private readonly ILocator _closeBoardBtn;
    private readonly ILocator _closeBtn;

    public WorkspaceNavComponent(IPage page)
    {
        _page = page;
        _nav = _page.GetByTestId("workspace-navigation-nav");
        _currentBoard = _page.GetByLabel("(currently active)");
        _boardActionsMenuBtn = _page.GetByLabel("Board actions menu");
        _boardMenu = _page.GetByLabel("Show menu");
        _closeBoardBtn = _page.GetByLabel("Close board");
        _closeBtn = _page.GetByTestId("popover-close-board-confirm");
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