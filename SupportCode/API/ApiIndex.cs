using Microsoft.Playwright;

public class ApiIndex
{
    private readonly IAPIRequestContext _request;
    public ApiToken apiToken;
    public MembersApi membersApi;

    public ApiIndex(IAPIRequestContext request)
    {
        _request = request;
        apiToken = new ApiToken(_request);
        membersApi = new MembersApi(_request);
    }
}