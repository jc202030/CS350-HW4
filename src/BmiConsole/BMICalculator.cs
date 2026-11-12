using System;
using System.Globalization;

public class BMICalculator
{
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
            if (person.Height < 50 || person.Height > 250)
                return "Invalid measurements";
            if (person.Weight < 20 || person.Weight > 300)
                return "Invalid measurements";
        }
        else if (person.UnitSystem == "Imperial")
        {
            if (person.Height < 20 || person.Height > 100)
                return "Invalid measurements";
            if (person.Weight < 44 || person.Weight > 660)
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
        double meters = height / 100;
        return weight / (meters * meters);
    }

    public double CalculateImperial(double height, double weight)
    {
        return 703 * weight / (height * height);
    }

    public string GetCategory(double bmi)
    {
        if (bmi < 18.5)
            return "Underweight";
        else if (bmi < 25)
            return "Healthy";
        else if (bmi < 30)
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
