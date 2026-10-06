# MatchingEngineTests

Automation assessment for the QA Engineer role.

Selenium WebDriver with C# and NUnit. Tests run in Chrome.

The test opens matchingengine.com, expands Solutions in the header and checks the solutions list. Then it clicks Distribution processing, scrolls to "All-in-one solution for scale" and checks the content of that section.

## Run

Need .NET 8 SDK and Chrome.

```
dotnet test
```

To run slower:

```
set SLOW_MO=1500
dotnet test
```
