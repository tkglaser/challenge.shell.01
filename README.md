# Coding Challenge for Shell

* Requirements: PriceCalculator (see PDF)
* Technology: .NET Core 3.1 Console Application with Ms Test Framework
* Tooling: .NET Core on WSL Win10, Visual Studio Code
* Author: Thomas Glaser

## Running the application

The following commands restore third party packages and run the app:

```bash
cd PriceCalculator
dotnet restore
dotnet run
```

Arguments can be passed after `dotnet run` like so:
```bash
cd PriceCalculator
dotnet run milk bread apple
```
It is also possible to build the project into a Windows executable to achieve the command line as described in the PDF.

## Testing the application

The application has a set of tests using with MSTest and Moq.

The tests can be run using the following commands.

```bash
cd PriceCalculator.Tests
dotnet restore
dotnet test
```

The tests should all succeed.
