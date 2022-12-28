# Playwright in C# #

## Tools used ##

* [Playwright for dotnet](https://playwright.dev/dotnet/docs/api/class-playwright)
* [NUnit](https://playwright.dev/dotnet/docs/test-runners#nunit)

## Prerequisites ##

You need Visual Studio or Visual Studio Code with the C# extension and [PowerShell](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.3)

## How do I get set up? ##

Follow the steps [here](https://playwright.dev/dotnet/docs/intro), follow the steps from step 2

## How to run the tests ##

* Run all the tests with `dotnet test --settings:.runsettings`
* Run a single test with `dotnet test --settings:.runsettings --filter "MyClassName"`