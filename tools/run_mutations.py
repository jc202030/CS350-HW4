"""Three REAL mutation trials. Requires local dotnet SDK, xUnit restore and Python.
The source file is restored in a finally block after each mutation.
"""
from pathlib import Path
import shutil
import subprocess
import sys

root = Path(__file__).resolve().parents[1]
source = root / "src" / "BmiConsole" / "BMICalculator.cs"
output_dir = root / "evidence" / "mutations"
output_dir.mkdir(parents=True, exist_ok=True)

if shutil.which("dotnet") is None:
    sys.exit(".NET SDK unavailable: run this script on a computer with dotnet installed")

original = source.read_text(encoding="utf-8")
mutations = [
    ("M1_comparison", "if (bmi < HealthyCutoff)", "if (bmi <= HealthyCutoff)"),
    ("M2_constant", "private const double ImperialFactor = 703;", "private const double ImperialFactor = 700;"),
    ("M3_condition", 'if (person.UnitSystem == "Metric")\n            bmi = CalculateMetric', 'if (person.UnitSystem != "Metric")\n            bmi = CalculateMetric'),
]
for name, before, after in mutations:
    if original.count(before) != 1:
        raise RuntimeError(f"Expected one occurrence of {before!r}, saw {original.count(before)}")
    try:
        source.write_text(original.replace(before, after, 1), encoding="utf-8")
        cmd = ["dotnet", "test", "tests/BmiConsole.Tests/BmiConsole.Tests.csproj", "--no-restore", "--nologo"]
        proc = subprocess.run(cmd, cwd=root, capture_output=True, text=True)
        log = f"Mutation: {name}\nChange: {before!r} => {after!r}\nExit code: {proc.returncode}\n\n{proc.stdout}\n{proc.stderr}"
        (output_dir / f"{name}.txt").write_text(log, encoding="utf-8")
        print(name, "tests failed (killed)" if proc.returncode else "tests still passed (survived)")
    finally:
        source.write_text(original, encoding="utf-8")
print("Original source restored. Review evidence/mutations/*.txt")
