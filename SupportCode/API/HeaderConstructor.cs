public class HeaderConstructor
{
    private readonly Dictionary<string, string> headers = new();

    public void AddHeaders(string key, string value)
    {
        headers.TryAdd(key, value);
    }

    public Dictionary<string, string> GetHeaders()
    {
        return headers;
    }
}