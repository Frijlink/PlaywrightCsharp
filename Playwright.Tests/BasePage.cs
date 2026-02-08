using DotNetEnv;
using PlaywrightCsharp.Settings;

namespace PlaywrightCsharp.Playwright.Tests;

[TestFixture]
public class BasePage : PageTest
{
    public static string TOKEN = string.Empty;
    public static string KEY = string.Empty;
    public static string API_URL = string.Empty;
    public static string USERNAME = string.Empty;
    public static string PASSWORD = string.Empty;
    public static string TRELLO_URL = string.Empty;

    [OneTimeSetUp]
    public static void ReadDotEnv()
    {
        Env.TraversePath().Load();

        TOKEN = Configuration.GetEnvironmentVariable("TRELLO_API_TOKEN");
        KEY = Configuration.GetEnvironmentVariable("TRELLO_API_KEY");
        API_URL = Configuration.GetEnvironmentVariable("TRELLO_API_URL");
        USERNAME = Configuration.GetEnvironmentVariable("TRELLO_USERNAME");
        PASSWORD = Configuration.GetEnvironmentVariable("TRELLO_PASSWORD");
        TRELLO_URL = Configuration.GetEnvironmentVariable("TRELLO_BASE_URL");
    }

    public static string GenerateBoardName() => string.Join('_', Faker.Lorem.Words(3));
}