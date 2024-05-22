using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a default fraction object (assuming the default constructor sets a default value)
        Fraction f1 = new Fraction();
        // Print the fraction in string format (e.g., "0/1" or "1/1")
        Console.WriteLine(f1.GetFractionString());
        // Print the decimal value of the fraction
        Console.WriteLine(f1.GetDecimalValue());

        // Create a fraction with the numerator 5 and an implicit denominator (assuming 1)
        Fraction f2 = new Fraction(5);
        // Print the fraction in string format (e.g., "5/1")
        Console.WriteLine(f2.GetFractionString());
        // Print the decimal value of the fraction (e.g., 5.0)
        Console.WriteLine(f2.GetDecimalValue());

        // Create a fraction with the numerator 3 and denominator 4
        Fraction f3 = new Fraction(3, 4);
        // Print the fraction in string format (e.g., "3/4")
        Console.WriteLine(f3.GetFractionString());
        // Print the decimal value of the fraction (e.g., 0.75)
        Console.WriteLine(f3.GetDecimalValue());

        // Create a fraction with the numerator 1 and denominator 3
        Fraction f4 = new Fraction(1, 3);
        // Print the fraction in string format (e.g., "1/3")
        Console.WriteLine(f4.GetFractionString());
        // Print the decimal value of the fraction (e.g., approximately 0.3333)
        Console.WriteLine(f4.GetDecimalValue());
    }
}
