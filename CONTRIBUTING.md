# Contributing to StaadPro.Interop

Thank you for your interest in contributing to **StaadPro.Interop**! We welcome bug fixes, documentation improvements, unit tests, and feature enhancements.

## Development Setup

1. **Prerequisites**:
   - .NET SDK (8.0+ or .NET Framework 4.8.1 target support)
   - Visual Studio 2022 / JetBrains Rider / VS Code
   - Bentley STAAD.Pro (required only for live COM integration tests, not for unit tests)

2. **Clone and Build**:
   ```powershell
   git clone https://github.com/structuraldeveloper/StaadPro.Interop.git
   cd StaadPro.Interop
   dotnet build StaadPro.Interop.sln -c Release
   ```

3. **Run Unit Tests**:
   ```powershell
   dotnet test tests/StaadPro.Interop.Tests/StaadPro.Interop.Tests.csproj -c Release
   ```

## Contribution Workflow

1. Fork the repository and create your feature branch: `git checkout -b feature/my-new-feature`
2. Commit your changes with concise, descriptive commit messages.
3. Ensure all offline unit tests pass.
4. Open a Pull Request referencing any related issues.
