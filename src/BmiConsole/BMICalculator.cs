using System;
using System.Globalization;

public class BMICalculator
{
    private const double MinMetricHeight = 50;
    private const double MaxMetricHeight = 250;
    private const double MinMetricWeight = 20;
    private const double MaxMetricWeight = 300;
    private const double MinImperialHeight = 20;
    private const double MaxImperialHeight = 100;
    private const double MinImperialWeight = 44;
    private const double MaxImperialWeight = 660;
    private const double CentimetersPerMeter = 100;
    private const double ImperialFactor = 703;
    private const double UnderweightCutoff = 18.5;
    private const double HealthyCutoff = 25;
    private const double OverweightCutoff = 30;
    public void Calculate(Person person)
    {
        if (person == null)
            throw new ArgumentNullException(nameof(person));

        string error = GetInputError(person);
        if (error != null)
        {
            Console.WriteLine(error);
            return;
        }

        double bmi;
        if (person.UnitSystem == "Metric")
            bmi = CalculateMetric(person.Height, person.Weight);
        else
            bmi = CalculateImperial(person.Height, person.Weight);

        bmi = Math.Round(bmi, 1, MidpointRounding.AwayFromZero);
        string category = GetCategory(bmi);
        DisplayResult(person, bmi, category);
    }

    public string GetInputError(Person person)
    {
        if (person.UnitSystem == "Metric")
        {
            if (person.Height < MinMetricHeight || person.Height > MaxMetricHeight)
                return "Invalid measurements";
            if (person.Weight < MinMetricWeight || person.Weight > MaxMetricWeight)
                return "Invalid measurements";
        }
        else if (person.UnitSystem == "Imperial")
        {
            if (person.Height < MinImperialHeight || person.Height > MaxImperialHeight)
                return "Invalid measurements";
            if (person.Weight < MinImperialWeight || person.Weight > MaxImperialWeight)
                return "Invalid measurements";
        }
        else
        {
            return "Invalid unit system";
        }
        return null;
    }

    public double CalculateMetric(double height, double weight)
    {
        double meters = height / CentimetersPerMeter;
        return weight / (meters * meters);
    }

    public double CalculateImperial(double height, double weight)
    {
        return ImperialFactor * weight / (height * height);
    }

    public string GetCategory(double bmi)
    {
        if (bmi < UnderweightCutoff)
            return "Underweight";
        else if (bmi < HealthyCutoff)
            return "Healthy";
        else if (bmi < OverweightCutoff)
            return "Overweight";
        else
            return "Obese";
    }

    public void DisplayResult(Person person, double bmi, string category)
    {
        Console.WriteLine("Name: " + person.Name);
        Console.WriteLine("BMI: " + bmi.ToString("F1", CultureInfo.InvariantCulture));
        Console.WriteLine("Category: " + category);
    }
}
