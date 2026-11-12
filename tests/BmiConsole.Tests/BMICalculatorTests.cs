using System;
using System.Globalization;
using System.IO;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

public class BMICalculatorTests
{
    private static string Run(Person person)
    {
        TextWriter original = Console.Out;
        using var output = new StringWriter(CultureInfo.InvariantCulture);
        try
        {
            Console.SetOut(output);
            new BMICalculator().Calculate(person);
            return output.ToString().Replace("\r\n", "\n").Trim();
        }
        finally
        {
            Console.SetOut(original);
        }
    }

    [Theory]
    [InlineData(180, 90, "27.8", "Overweight")]
    [InlineData(170, 50, "17.3", "Underweight")]
    [InlineData(170, 70, "24.2", "Healthy")]
    [InlineData(170, 100, "34.6", "Obese")]
    [InlineData(200, 99.92, "25.0", "Overweight")]
    public void MetricExamplesAndRoundedCategory(double height, double weight, string bmi, string category)
    {
        string result = Run(new Person("Alex", height, weight, "Metric"));
        Assert.Contains("Name: Alex", result);
        Assert.Contains("BMI: " + bmi, result);
        Assert.Contains("Category: " + category, result);
    }

    [Fact]
    public void ImperialExample()
    {
        string result = Run(new Person("Pat", 70, 200, "Imperial"));
        Assert.Contains("BMI: 28.7", result);
        Assert.Contains("Category: Overweight", result);
    }

    [Theory]
    [InlineData("Metric", 49.9, 90)]
    [InlineData("Metric", 250.1, 90)]
    [InlineData("Metric", 180, 19.9)]
    [InlineData("Metric", 180, 300.1)]
    [InlineData("Imperial", 19.9, 180)]
    [InlineData("Imperial", 100.1, 180)]
    [InlineData("Imperial", 70, 43.9)]
    [InlineData("Imperial", 70, 660.1)]
    public void InvalidMeasurementRejected(string units, double height, double weight)
    {
        Assert.Equal("Invalid measurements", Run(new Person("A", height, weight, units)));
    }

    [Theory]
    [InlineData("Metric", 50, 20)]
    [InlineData("Metric", 250, 300)]
    [InlineData("Imperial", 20, 44)]
    [InlineData("Imperial", 100, 660)]
    public void ExactRangeEndpointsAreAccepted(string units, double height, double weight)
    {
        Assert.Contains("BMI:", Run(new Person("A", height, weight, units)));
    }

    [Theory]
    [InlineData("Unknown")]
    [InlineData("metric")]
    [InlineData("")]
    [InlineData(null)]
    public void OtherUnitSystemsRejected(string units)
    {
        Assert.Equal("Invalid unit system", Run(new Person("A", 170, 70, units)));
    }

    [Theory]
    [InlineData("Metric", 49.9, 90, false)]
    [InlineData("Metric", 50.0, 90, true)]
    [InlineData("Metric", 50.1, 90, true)]
    [InlineData("Metric", 249.9, 90, true)]
    [InlineData("Metric", 250.0, 90, true)]
    [InlineData("Metric", 250.1, 90, false)]
    [InlineData("Metric", 180, 19.9, false)]
    [InlineData("Metric", 180, 20.0, true)]
    [InlineData("Metric", 180, 20.1, true)]
    [InlineData("Metric", 180, 299.9, true)]
    [InlineData("Metric", 180, 300.0, true)]
    [InlineData("Metric", 180, 300.1, false)]
    [InlineData("Imperial", 19.9, 180, false)]
    [InlineData("Imperial", 20.0, 180, true)]
    [InlineData("Imperial", 20.1, 180, true)]
    [InlineData("Imperial", 99.9, 180, true)]
    [InlineData("Imperial", 100.0, 180, true)]
    [InlineData("Imperial", 100.1, 180, false)]
    [InlineData("Imperial", 70, 43.9, false)]
    [InlineData("Imperial", 70, 44.0, true)]
    [InlineData("Imperial", 70, 44.1, true)]
    [InlineData("Imperial", 70, 659.9, true)]
    [InlineData("Imperial", 70, 660.0, true)]
    [InlineData("Imperial", 70, 660.1, false)]
    public void EveryBoundaryBelowAtAbove(string units, double height, double weight, bool valid)
    {
        string result = Run(new Person("Boundary", height, weight, units));
        if (valid)
            Assert.Contains("BMI:", result);
        else
            Assert.Equal("Invalid measurements", result);
    }

    [Theory]
    [InlineData(18.4, "Underweight")]
    [InlineData(18.5, "Healthy")]
    [InlineData(18.6, "Healthy")]
    [InlineData(24.9, "Healthy")]
    [InlineData(25.0, "Overweight")]
    [InlineData(25.1, "Overweight")]
    [InlineData(29.9, "Overweight")]
    [InlineData(30.0, "Obese")]
    [InlineData(30.1, "Obese")]
    public void CategoryBoundaries(double bmi, string category)
    {
        Assert.Equal(category, new BMICalculator().GetCategory(bmi));
    }

    [Fact]
    public void NullPersonThrowsHelpfulArgumentException()
    {
        Assert.Throws<ArgumentNullException>(() => new BMICalculator().Calculate(null));
    }

    [Fact]
    public void EachHelperFormulaWorks()
    {
        var calculator = new BMICalculator();
        Assert.Equal(25.0, calculator.CalculateMetric(200, 100), 7);
        Assert.Equal(703.0 * 200 / (70 * 70), calculator.CalculateImperial(70, 200), 7);
    }
}
