namespace PlaywrightCsharp.SupportCode.Pages.Trello;

public class BoardPage
{
    private readonly IPage _page;
    private readonly ILocator _mainTitle;
    private readonly ILocator _boardNameInput;
    private readonly ILocator _board;
    private readonly ILocator _leftMenu;
    private readonly ILocator _boardMenu;
    private readonly ILocator _closeBoardMessage;
    private readonly ILocator _deleteBoardBtn;
    private readonly ILocator _deleteBoardConfirmBtn;

    public BoardPage(IPage page)
    {
        _page = page;
        _mainTitle = _page.Locator("css=.board-header h1");
        _boardNameInput = _page.GetByTestId("board-name-input");
        _board = _page.Locator("css=#board");
        _leftMenu = _page.GetByTestId("workspace-boards-and-views-lists");
        _boardMenu = _page.GetByLabel("Show menu");
        _closeBoardMessage = _page.Locator("css=#content-wrapper p");
        _deleteBoardBtn = _page.GetByTestId("close-board-delete-board-button");
        _deleteBoardConfirmBtn = _page.GetByTestId("close-board-delete-board-confirm-button");
    }

    public ILocator GetMainTitle()
    {
        return _mainTitle;
    }

    public ILocator GetCloseBoardMessage()
    {
        return _closeBoardMessage;
    }

    public async Task WaitForPageLoaded()
    {
        await _board.WaitForAsync();
        await _leftMenu.WaitForAsync();
    }

    public async Task UpdateBoardName(string name)
    {
        await _mainTitle.ClickAsync();
        await _boardNameInput.ClearAsync();
        await _boardNameInput.FillAsync(name);
        await _page.Keyboard.PressAsync("Enter");
    }

    public async Task DeleteBoard()
    {
        await _boardMenu.ClickAsync();
        await _deleteBoardBtn.ClickAsync();
        await _deleteBoardConfirmBtn.ClickAsync();
    }
}