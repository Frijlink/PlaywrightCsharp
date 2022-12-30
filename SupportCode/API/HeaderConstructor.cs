public class HeaderConstructor
{
    private Dictionary<string, string> headers = new Dictionary<string, string>();

    public void AddHeaders(string key, string value)
    {
        headers.TryAdd(key, value);
    }

    public Dictionary<string, string> GetHeaders()
    {
        return headers;
    }
}