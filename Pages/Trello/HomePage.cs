using PlaywrightCsharp.Playwright.Tests;

namespace PlaywrightCsharp.Pages.Trello;

public class HomePage(IPage page)
{
    private readonly ILocator _sectionHeader = page.Locator("css=h3.boards-page-section-header-name");
    private readonly ILocator _newBoardBtn = page.GetByTestId("create-board-tile");
    private readonly ILocator _newBoardNameInput = page.GetByTestId("create-board-title-input");
    private readonly ILocator _createNewBoardSubmitBtn = page.GetByTestId("create-board-submit-button");
    private readonly ILocator _boardTileTitle = page.Locator("css=.board-tile-details-name");

    public async Task GoTo() => await page.GotoAsync(BasePage.TRELLO_URL);

    public ILocator GetSectionHeader() {
        return _sectionHeader;
    }

    public async Task<IReadOnlyList<string>> GetAllBoardNames()
    {
        return (await _boardTileTitle.IsVisibleAsync())
            ? await _boardTileTitle.AllInnerTextsAsync()
            : [];
    }

    public ILocator GetBackGroundColourBtn(string colour) => page.Locator($"css=button[title=\"{colour}\"]");

    public async Task CreateNewBoard(string name, string backgroundColour) {
        await _newBoardBtn.ClickAsync();
        await GetBackGroundColourBtn(backgroundColour).ClickAsync();
        await _newBoardNameInput.FillAsync(name);
        await _createNewBoardSubmitBtn.WaitForAsync(new() { State = WaitForSelectorState.Attached });
        await _createNewBoardSubmitBtn.ClickAsync();
        var newName = name.Replace("_", string.Empty).ToLower();
        await page.WaitForURLAsync($"**/{newName}");
    }
}