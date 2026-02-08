namespace PlaywrightCsharp.Settings;

public static class Configuration
{
   public static string GetEnvironmentVariable(string envVarKey) =>
       Environment.GetEnvironmentVariable(envVarKey)
         ?? throw new Exception($"failed to retrieve Environment Variables: {envVarKey}");
}