using System;

class Program
{
    static void Main(string[] args)
    {
        //creats a new list for user numbers
        List<int> numbers = new List<int>();

        //sets the users inital number
        int usernumber = -1;

        //start of a loop repeats untill the user inputs 0 to end
        while (usernumber !=0){

            //asks the user for a number input
            Console.Write("Enter a number (0 to end): ");
            //turns user response into a usibule responce
            string userResponse = Console.ReadLine();
            //puts the user responce into the user number list
            usernumber = int.Parse(userResponse);

            //makes it so it dosent add 0 to the responce list
            if (usernumber !=0){
                //adds number to the list
                numbers.Add(usernumber);
            }

         //end of loop
        }

        //coculates the sum

        //sets the original sum to 0
        int sum = 0;

        //gathers together the list for the sum
        foreach (int number in numbers){
            //puts together the sum
            sum+= number;
        }
        //prints out the sum of numbers
        Console.WriteLine($"The sum of your numbers is: {sum}");

        //coculating the average of the numbers putting it into a float first so that we can use it
        //float turns it into a usibuloe numer
        //.count counts the number of numbers in the numbers list
        float average = ((float)sum)/numbers.Count;
        //outputs the average
        Console.WriteLine($"The average is: {average}");

        //finds the max number 
        //sets the original number to 0
        int max = numbers[0];
        
        //gathers together the list for max
        foreach (int number in numbers)
        {
            //checks all the numbers to find the max
            if (number > max)
            {
                // if this number is greater than the max it is the new max
                max = number;
            }
        }
        //prints out the max number for use
        Console.WriteLine($"The max is: {max}");
        


        
    }
}