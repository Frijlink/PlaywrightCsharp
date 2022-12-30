public class HeaderConstructor
{
    private Dictionary<string, string> headers = new Dictionary<string, string>();

    public void AddHeaders(string value, string key)
    {
        headers.Add(value, key);
    }

    public Dictionary<string, string> GetHeaders()
    {
        return headers;
    }
}