# Evidence checklist - run on YOUR computer

Your assignment requires real local screenshots, not generated or simulated screenshots. The report intentionally marks anything not run in the build environment as **PENDING LOCAL RUN**.

1. Open the repository in Visual Studio or VS Code with .NET 8 SDK. Run `dotnet --info` and `dotnet restore BmiHomework4.sln`.
2. Run `dotnet run --project src/BmiConsole/BmiConsole.csproj`. Screenshot console output with folder path visible.
3. Run `git log --oneline --reverse` and `git blame src/BmiConsole/BMICalculator.cs`; screenshot git history showing a genuine repository; do not claim generated authorship as your own.
4. Run `python -m pip install lizard` then `lizard src/BmiConsole/BMICalculator.cs`, and screenshot the measured complexity. Use `git show <BASELINE_HASH>:src/BmiConsole/BMICalculator.cs > baseline.cs` to measure the original method; the final file has been refactored.
5. Capture VS Code Find Usages / Call Hierarchy for BMICalculator.
6. Run `dotnet test BmiHomework4.sln` and screenshot pass/fail output (do not claim pass before you run).
7. Run `dotnet test BmiHomework4.sln --collect:"XPlat Code Coverage" --results-directory evidence/TestResults` and open `evidence/TestResults/<guid>/coverage.cobertura.xml` for actual line and branch coverage.
8. Run `python tools/run_mutations.py` and read actual test logs in `evidence/mutations/`. Screenshot failures and record mutation outcomes.
9. Run `dotnet format whitespace BmiHomework4.sln --verify-no-changes` and `dotnet build BmiHomework4.sln -warnaserror` for formatting/compiler warnings. Screenshot actual output (if no warnings, note clean result). Use Visual Studio's analyzer if a true linter report is required.
10. To show the historical RED/GREEN commits, checkout appropriate commits one at a time and run the test suite at each: `git log --oneline --reverse`; `git checkout <RED_HASH>`; `dotnet test`; `git checkout <GREEN_HASH>`; `dotnet test`; `git switch master` (or `main`). Screenshot failures/success on your computer.
11. Place actual screenshots and evidence into a report. The draft PDF has notes where verification is still missing.

The provided git commits are real generated changes, **not proof of student authorship or testing**. Only assert actions and results you personally verified. Your user message stated AI is now allowed; the written assignment still states prohibited, so confirm the updated course policy before submitting AI-assisted work.
