# Playwright in C# #

## Tools used ##

* [Playwright for dotnet](https://playwright.dev/dotnet/docs/api/class-playwright)
* [NUnit](https://playwright.dev/dotnet/docs/test-runners#nunit)

## Prerequisites ##

You need Visual Studio or Visual Studio Code with the C# extension and [PowerShell](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.3)

## How do I get set up? ##

Follow the steps [here](https://playwright.dev/dotnet/docs/intro), follow the steps from step 2

Install [Faket.net](https://github.com/Kuree/Faker.Net)-package with `dotnet add package Faker.Net`

Fill in your credentials in `MyConfig.cs`.

## How to run the tests ##

* Run all the tests with `dotnet test --settings:.runsettings`
* Run a single test with `dotnet test --settings:.runsettings --filter "MyClassName"`

## TODO: ##

* Parallel tests
* Screenshot on save
* HTML report (or trx)
* Get rid of MyConfig.cs (dotnet user-secrets?)
* get rid of unnecassary casting?
* Fluentassert vs Assert