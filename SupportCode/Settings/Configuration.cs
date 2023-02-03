public static class Configuration
{
     public static string GetEnvironmentVariable(string envVar)
     {
        var str = Environment.GetEnvironmentVariable(envVar);
        if (str is null) throw new Exception("failed to retrieve Environment Variables");
        else return str;
     }
}