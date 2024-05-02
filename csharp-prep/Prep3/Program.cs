using System;

class Program
{
    static void Main(string[] args)
    {  
       //generates a randome number form 1-11
       Random randomGenerator = new Random();
       int magicnumber = randomGenerator.Next(1, 11); 

        //sets an the original number to start the loop
       int guess =-1;
       int guesscount = 0;
        //loops untill the user guess's = the magic number
       while (guess != magicnumber)
        {
            //asks the user what ther guess is 
            Console.Write ("What is your guess?: ");
            //turns the guess into a number for use 
            guess = int.Parse(Console.ReadLine());

            //states higher if the magic nuber is less than the geuss
            if (magicnumber > guess){
                Console.WriteLine("Higher");
                //adds one to the guesscount
                guesscount++;
            }
            
            //states lower if the magic number is more than the geuss
            else if (magicnumber < guess){
                Console.WriteLine("lower");
                //adds one to the guesscount
                guesscount++;
            }

            //if the user getts it corret it will state the guesscount 
            else{
                //adds one to the guesscount for the last guess
                guesscount++;
                Console.WriteLine($"You guessed it! It took {guesscount} trys to gett it correct.");
            }
         //end of the loop
        }
    }
}