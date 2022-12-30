using Microsoft.Playwright;

public class ApiIndex
{
    private readonly IAPIRequestContext _request;
    public ApiToken apiToken;
    public BoardsApi boardsApi;
    public MembersApi membersApi;

    public ApiIndex(IAPIRequestContext request)
    {
        _request = request;
        apiToken = new ApiToken(_request);
        boardsApi = new BoardsApi(_request);
        membersApi = new MembersApi(_request);
    }
}