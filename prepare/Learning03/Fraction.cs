using System;

public class Fraction
{
    // Private fields to store the numerator (topnumber) and the denominator (bottomnumber) of the fraction
    private int topnumber;
    private int bottomnumber;

    // Constructor that takes a whole number and creates a fraction with denominator 1
    public Fraction(int wholeNumber){
        topnumber = wholeNumber;
        bottomnumber = 1;
    }

    // Constructor that takes a numerator (top) and a denominator (bottom)
    public Fraction(int top, int bottom){
        topnumber = top;
        bottomnumber = bottom;
    }

    // Default constructor, sets the fraction to 1/1
    public Fraction(){
        topnumber = 1;
        bottomnumber = 1;
    }
    // Method to return the fraction as a string in the format "numerator/denominator"
    public string GetFractionString(){
        // The fraction string is recomputed each time this method is called
        string text = $"{topnumber}/{bottomnumber}";
        return text;
    }
    
    // Method to return the decimal value of the fraction
    public double GetDecimalValue(){
        // The decimal value is recomputed each time this method is called
        return (double)topnumber / (double)bottomnumber;
    }
}
