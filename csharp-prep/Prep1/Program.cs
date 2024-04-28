using System;

class Program
{
    static void Main(string[] args)
    {
        //Consle.write asks the user for the input in the consle if you want to 
        //start a new line you can use Console.WriteLine
        //string asignes the input to a designated tag

        //asks user for first name and gets input
        Console.Write("What is your first name?: ");
        //asigns the inputed name to the tag firstname for further use
        string firstname = Console.ReadLine();

        //asks the user for the last name and gets input
        Console.Write("What is your last name?: ");
        //asignes the inputed last name to the tag lastname for further use
        string lastname = Console.ReadLine();


        //prints out the persons name formated
        //the $ is for fomating it simmeler to using f in python
        Console.WriteLine($"Your name is {lastname}. {firstname} {lastname}");

    }
}