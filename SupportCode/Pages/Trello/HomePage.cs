using Microsoft.Playwright;
using static PlaywrightCsharp.SupportCode.Settings.Configuration;

namespace PlaywrightCsharp.SupportCode.Pages.Trello;

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
        _newBoardBtn = page.GetByTestId("create-board-tile");
        _newBoardNameInput = page.GetByTestId("create-board-title-input");
        _selectVisibilityDropdown = page.Locator("css=[id$=\"create-board-select-visibility\"] > div > div > div:nth-child(1)");
        _visibilityPrivateBtn = page.Locator("css=#react-select-4-option-0 li");
        _visibilityWorkspaceBtn = page.Locator("css=#react-select-4-option-1 li");
        _visibilityPublicBtn = page.Locator("css=#react-select-4-option-2 li");
        _createNewBoardSubmitBtn = page.GetByTestId("create-board-submit-button");
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
            ? await _boardTileTitle.AllInnerTextsAsync()
            : new List<string>();
    }

    public ILocator GetBackGroundColourBtn(string colour)
    {
        return _page.Locator($"css=button[title=\"{colour}\"]");
    }

    public async Task CreateNewBoard(string name, string backgroundColour) {
        await _newBoardBtn.ClickAsync();
        await GetBackGroundColourBtn(backgroundColour).ClickAsync();
        await _newBoardNameInput.FillAsync(name);
        await _createNewBoardSubmitBtn.WaitForAsync(new() { State = WaitForSelectorState.Attached });
        await _createNewBoardSubmitBtn.ClickAsync();
        var newName = name.Replace("_", string.Empty).ToLower();
        await _page.WaitForURLAsync($"**/{newName}");
    }
}