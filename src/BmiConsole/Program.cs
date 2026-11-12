class Program
{
    static void Main(string[] args)
    {
        Person person = new Person("Joshua", 180, 90, "Metric");
        BMICalculator cal = new BMICalculator();
        cal.Calculate(person);
    }
}
