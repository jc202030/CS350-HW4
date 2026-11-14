# CS 350 Homework 4 — BMI and Category (Option B, Feature 6)

## Requirements
- .NET 8 SDK
- Git
- For coverage: `coverlet.collector` is referenced by the xUnit test project (NuGet restore required).
- Optionally install Lizard: `python -m pip install lizard` and run `lizard src/BmiConsole/BMICalculator.cs`.

## Try it
```powershell
dotnet run --project src/BmiConsole/BmiConsole.csproj
dotnet test tests/BmiConsole.Tests/BmiConsole.Tests.csproj
# Coverage, with output files for screenshots:
dotnet test tests/BmiConsole.Tests/BmiConsole.Tests.csproj --collect:"XPlat Code Coverage" --results-directory evidence/TestResults
# Linter/format diagnostics (can run after solution restore):
dotnet format whitespace BmiHomework4.sln --verify-no-changes
# Three live mutation trials (script temporarily changes and restores source):
python tools/run_mutations.py
```

## Project design
- `Person` stores Name, Height, Weight, UnitSystem.
- `BMICalculator.Calculate(person)` **calculates AND automatically prints** Name/BMI/Category, as requested.
- Both Metric (cm/kg) and Imperial (in/lb) are required by the source handout.
- The baseline method originally had a longer series of `if` validations; subsequent commits add tests and then refactor it.
- Look at `git log --oneline --reverse` and `git show <hash>:src/BmiConsole/BMICalculator.cs` to see before/after changes.

## Evidence still needed on your computer
The build environment that generated this package did not have .NET or a NuGet connection. Do **not** mark tests, coverage, linter output, or mutations as successful until you run them yourself. See `evidence/WHAT_TO_CAPTURE.md` and the accompanying report for remaining spots.


