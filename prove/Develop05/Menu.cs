using System;

namespace EternalQuest
{
    public static class Menu
    {
        public static void DisplayMainMenu(GoalManager goalManager)
        {
            while (true)
            {
                //list all posibule options
                Console.Clear();
                Console.WriteLine("Eternal Quest");
                Console.WriteLine("1. View Goals");
                Console.WriteLine("2. Create Goal");
                Console.WriteLine("3. Record Event/ Finish Goal");
                Console.WriteLine("4. View Score");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                //reads user input to run a case 
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                    //code to run Dispaly Goles in Goal Manager Class
                        goalManager.DisplayGoals();
                        break;
                    case "2":
                    //Code to run Create gole in Goal Maniger Class
                        goalManager.CreateGoal();
                        break;
                    case "3":
                    //Code to Compleat a goal in Goal maniger class
                        goalManager.RecordEvent();
                        break;
                    case "4":
                    //Code to Dispaly score in Goal maniger class
                        goalManager.DisplayScore();
                        break;
                    case "5":
                    //code to prevent a carsh incase user selects invalid options
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
