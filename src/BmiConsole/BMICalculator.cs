using System;
using System.Globalization;

public class BMICalculator
{
    public void Calculate(Person person)
    {
        double bmi;
        if (person.UnitSystem == "Metric")
        {
            if (person.Height < 50 || person.Height > 250)
            {
                Console.WriteLine("Invalid measurements");
                return;
            }
            if (person.Weight < 20 || person.Weight > 300)
            {
                Console.WriteLine("Invalid measurements");
                return;
            }
            bmi = CalculateMetric(person.Height, person.Weight);
        }
        else if (person.UnitSystem == "Imperial")
        {
            if (person.Height < 20 || person.Height > 100)
            {
                Console.WriteLine("Invalid measurements");
                return;
            }
            if (person.Weight < 44 || person.Weight > 660)
            {
                Console.WriteLine("Invalid measurements");
                return;
            }
            bmi = CalculateImperial(person.Height, person.Weight);
        }
        else
        {
            Console.WriteLine("Invalid unit system");
            return;
        }

        bmi = Math.Round(bmi, 1, MidpointRounding.AwayFromZero);
        string category = GetCategory(bmi);
        DisplayResult(person, bmi, category);
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
