namespace PlaywrightCsharp.SupportCode.Settings;

public static class Configuration
{
     public static string GetEnvironmentVariable(string envVarKey)
     {
        var envar = Environment.GetEnvironmentVariable(envVarKey);
        if (envar is null) throw new Exception($"failed to retrieve Environment Variables: {envVarKey}");
        else return envar;
     }
}