using System;
using System.ComponentModel;

class Program
{
    static void Main(string[] args)
    {
        //asks the user for there grade
        Console.Write("What is your grade %? ");
        //takes ther grade for usage
        string grade = Console.ReadLine();
        //for grade %
        int percent = int.Parse(grade);

        //sting for converting percentage into a letter
        string lettergrade = "";
        
        //if grade is > than or = 90 than its an A
        if (percent >= 90)
        {
            lettergrade = "A";
        }

        //if grade is > or = to 80 but less than 90 than B
        else if (percent >= 80)
        {
            lettergrade = "B";
        }
        
        //if grade is > or = to 70 but less than 80 than B
        else if (percent >= 70)
        {
            lettergrade = "C";
        }

        //if grade is > or = to 60 but less than 70 than B
        else if (percent >= 60)
        {
            lettergrade = "D";
        }

        //if you grade is any lower than 60 than F
        else
        {
            lettergrade = "F";
        }
        //prints out the inputed grade into a letter 
        Console.WriteLine($"Your Grade of {grade}% gets you a {lettergrade}");


        //prints out if you passed
        if (percent >= 70)
        {
            Console.WriteLine("Congragulations You passed! =)");
        }

        //prints out if you failed
        else
        {
            Console.WriteLine("Better luck next time. =( ");
        }
    }

  

}