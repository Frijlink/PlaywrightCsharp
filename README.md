# Playwright in C# #

## Tools used ##

* [Playwright for dotnet](https://playwright.dev/dotnet/docs/api/class-playwright)
* [NUnit](https://playwright.dev/dotnet/docs/test-runners#nunit)

## Prerequisites ##

You need Visual Studio or Visual Studio Code with the C# extension and [PowerShell](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.3)

## How do I get set up? ##

Build the project in your IDE or in the terminal using `dotnet build PlaywrightCsharp.csproj`.

Install Playwright with `pwsh bin/Debug/net8.0/playwright.ps1 install`.

Create a `.env`-file with the following values

```
RETRIES=0
TRELLO_BASE_URL="http://trello.com"
TRELLO_USERNAME="***"
TRELLO_PASSWORD="***"
TRELLO_API_URL="https://api.trello.com"
TRELLO_API_KEY="***"
TRELLO_API_TOKEN="***"
```

## How to run the tests ##

* Run all the tests with `dotnet test --settings:.runsettings`
* Run a single test with `dotnet test --settings:.runsettings --filter "MyClassName"`
* Run a single category with `dotnet test --settings:.runsettings --filter TestCategory=MyCategory`

## TODO: ##

* get rid of unnecassary casting?
* Screenshot on save
* HTML report (or trx)