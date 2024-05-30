# MSB

MSB is a .NET 8 application that provides a platform for customers to handle pick-up, return, and storage of their boxes or various items. Customers can track their items online and request a return whenever they need.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET 8
- Entity Framework Core 8

### Installing

1. Clone the repo
2. Open the solution in Visual Studio
3. Update the database connection string in `appsettings.json`
4. Run the command `Update-Database` in the Package Manager Console
5. Build and run the project

## Contribution

Here are the steps to contribute to the project and creating a pull request:
1. Make sure you are on the correct branch before you start making changes with: `git branch -a`
2. Or create a new branch for your feature with: `git checkout -b "Feature/YourFeatureName"`
3. Before committing your changes, make sure your code is properly formatted using: `dotnet format`
4. Add your changes to the staging area using: `git add .`
5. Commit your changes with an explanation of what has been changed using: `git commit -m "Explanation of what you've done"`
6. Push your changes to the remote repository using: `git push`
7. If it is your first push to the branch you have to use this command instead: `git push --set-upstream origin Feature/YourFeatureName`
8. Open a pull request on GitHub to merge your changes into the main branch. 
9. Ask a colleague to approve your code changes in the pull request
10. Merge and delete branch

Always make sure to update tests as appropriate and follow the coding standards and conventions in this project.

## Continuous Integration

This project uses GitHub Actions for continuous integration, which helps to ensure that the project is always in a working state. The workflow is defined in the `.github/workflows/pull_request_checks.yml` file.

The workflow runs on every pull request to the `development` and `production` branches. It performs the following steps:

1. Checks out the code.
2. Sets up a .NET environment with the specified .NET version.
3. Formats the code and verifies that no changes are made (ensuring that all code is properly formatted).
4. Restores all .NET dependencies.
5. Builds the solution.
6. Runs all tests in the solution.

This workflow ensures that all code is properly formatted, all dependencies are correctly installed, the solution builds successfully, and all tests pass before any code is merged into the `development` or `production` branches.

## Running the tests

The tests for this project are located in the `Tests` directory. You can run them using the test runner in Visual Studio.

## Deployment

The application can be deployed using any server that supports .NET, such as IIS or Kestrel. The `launchSettings.json` file contains the configuration for the local development server.

## Built With

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org/) - For object-object mapping
- [MediatR](https://github.com/jbogard/MediatR) - For implementing the Mediator pattern and CQRS
- [FluentValidation](https://fluentvalidation.net/) - For building strongly-typed validation rules
- [Microsoft.Extensions.Logging](https://docs.microsoft.com/en-us/dotnet/core/extensions/logging) - For logging
- [xUnit](https://xunit.net/) - For unit testing

## Authors

* **Tomas Lehtelä**
* **Robert Svart**
* **Fredrik Ekroth**
* **David Larsson**
* **Aramsin Oraha**
* **Abdirisak Aden**

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## Acknowledgments

* Hat tip to Mathias and Bidi for their contributions and inspiration
