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
It is also possible to build the project into an executable to achieve the command line as described in the PDF. After a `dotnet build` or `dotnet run` do
```bash
cd PriceCalculator/bin/Debug/netcoreapp3.1
./PriceCalculator milk bread apple
```

## Configuration
The application expects a file called `appsettings.json` to be in the current directory. This file contains a list of products and offers. It can be found in this repo at `PriceCalculator/appsettings.json`. If the configuration file is not found, an exception is thrown informing the user that the file is missing. This is intentional because there is not much value in running without the config and hardcoding a default config is dangerous because it can mask configuration errors.

## Testing the application

The application has a set of tests using with MSTest and Moq.

The tests can be run using the following commands.

```bash
cd PriceCalculator.Tests
dotnet restore
dotnet test
```

The tests should all succeed.
