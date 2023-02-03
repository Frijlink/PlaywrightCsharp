using Microsoft.Playwright;
using static Configuration;

public class HomePage
{
    private readonly IPage _page;
    private readonly ILocator _sectionHeader;
    private readonly ILocator _newBoardBtn;
    private readonly ILocator _newBoardNameInput;
    private readonly ILocator _selectVisibilityDropdown;
    private readonly ILocator _visibilityPrivateBtn;
    private readonly ILocator _visibilityWorkspaceBtn;
    private readonly ILocator _visibilityPublicBtn;
    private readonly ILocator _createNewBoardSubmitBtn;
    private readonly ILocator _boardTileTitle;

    public HomePage(IPage page)
    {
        _page = page;
        _sectionHeader = page.Locator("css=h3.boards-page-section-header-name");
        _newBoardBtn = page.Locator("css=[data-testid=\"create-board-tile\"]");
        _newBoardNameInput = page.Locator("css=[data-testid=\"create-board-title-input\"]");
        _selectVisibilityDropdown = page.Locator("css=[id$=\"create-board-select-visibility\"] > div > div > div:nth-child(1)");
        _visibilityPrivateBtn = page.Locator("css=#react-select-4-option-0 li");
        _visibilityWorkspaceBtn = page.Locator("css=#react-select-4-option-1 li");
        _visibilityPublicBtn = page.Locator("css=#react-select-4-option-2 li");
        _createNewBoardSubmitBtn = page.Locator("css=[data-testid=\"create-board-submit-button\"]");
        _boardTileTitle = page.Locator("css=.board-tile-details-name");
    }

    public async Task GoTo()
    {
        await _page.GotoAsync(GetEnvironmentVariable("TRELLO_BASE_URL"));
    }

    public ILocator GetSectionHeader() {
        return _sectionHeader;
    }

    public async Task<IReadOnlyList<string>> GetAllBoardNames()
    {
        return (await _boardTileTitle.IsVisibleAsync())
            ? await (_boardTileTitle).AllInnerTextsAsync()
            : new List<string>();
    }

    public ILocator GetBackGroundColourBtn(string colour)
    {
        return _page.Locator($"css=button[title=\"{colour}\"]");
    }

    public async Task CreateNewBoard(string name, string backgroundColour) {
        await _page.RunAndWaitForResponseAsync(async () =>
        {
            await _newBoardBtn.ClickAsync();
            // TODO: this url should not be hardcoded
        }, "https://api-gateway.trello.com/gateway/api/gasv3/api/v1/batch");
        await GetBackGroundColourBtn(backgroundColour).ClickAsync();
        await _newBoardNameInput.TypeAsync(name, new() { Delay = 50 });
        await _createNewBoardSubmitBtn.WaitForAsync(new() { State = WaitForSelectorState.Attached });
        await _createNewBoardSubmitBtn.ClickAsync();
        var newName = name.Replace("_", string.Empty).ToLower();
        await _page.WaitForURLAsync($"**/{newName}");
    }
}