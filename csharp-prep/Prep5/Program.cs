using System;

class Program
{
    //map for runing the main function
    //main->WelcomeMassage->InputForUserName->InputForUserNumber->
    //SquareNumber(usernumber)->DisplayResults(username, squarednumber)

    //runs the main function 
    static void Main(string[] args)
    {
        //runs a welcome massage prompt
        WelcomeMassage();

        //defignes the user name function
        string userName = InputForUserName();

        //user number function
        int userNumber = InputForUserNumber();

        //function for squaring the function 
        int squaredNumber = SquareNumber(userNumber);

        //displaying the results 
        DisplayResult(userName, squaredNumber);
    }

//function for the Welcome Massage
    static void WelcomeMassage()
    {
        //prints a wecome message to the user 
        Console.WriteLine("Welcome to my program!");
    }

//function for inputing the users name
    static string InputForUserName()
    {
        //asks the user for there name
        Console.Write("Please enter your name: ");
        //turns there name into an input for there name
        string name = Console.ReadLine();

        return name;
    }

//function for the users favoriot number
    static int InputForUserNumber()
    {
        //asks the user for there favorite number
        Console.Write("Enter your favorite number: ");
        //takes the useres input and turn it into there favorit number input
        int number = int.Parse(Console.ReadLine());

        return number;
    }

//function for squaring the number pulling the number
    static int SquareNumber(int number)
    {
        //calculats the square number
        int square = number * number;
        //returns the number as the square
        return square;
    }

//functionfor displaying the results purlling the name and square for use
    static void DisplayResult(string name, int square)
    {
        //outputs the results
        Console.WriteLine($"{name}, the square for your number is {square}");
    }
}