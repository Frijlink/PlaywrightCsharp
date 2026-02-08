namespace PlaywrightCsharp.Api;

public class HeaderConstructor
{
    private readonly Dictionary<string, string> headers = [];

    public void AddHeaders(string key, string value)
    {
        headers.TryAdd(key, value);
    }

    public Dictionary<string, string> GetHeaders()
    {
        return headers;
    }
}