using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    public class GoalManager
    //parent class to Base goal that all the goals ar children to the indivishiwal gole types
    {
        private List<BaseGoal> goals;
        //runs to basegoal to run all types of goles
        private int score;
        //score data

        public GoalManager()
        {
            //sets score to 0 if no goles are compleated
            goals = new List<BaseGoal>();
            score = 0;
        }


        public void CreateGoal()
        //code to create new goles
        {
            //code to list types of goles
            Console.WriteLine("Select goal type:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Select 1-3: ");
            string choice = Console.ReadLine();

            BaseGoal newGoal = null;

            switch (choice)
            {
                //runs to create simple goals
                case "1":
                    newGoal = new SimpleGoal();
                    break;
                //runs to create eternal goles
                case "2":
                    newGoal = new EternalGoal();
                    break;
                //runs to create checklist goles
                case "3":
                    newGoal = new ChecklistGoal();
                    break;
                //code to prevent crash if inproper # is inputed
                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }


            newGoal.SetDetails();
            goals.Add(newGoal);
            //code to add gole to save data
        }

        public void RecordEvent()
        {
            //code to record when a gole is compleated
            while (true)
            {
                DisplayGoals();
                Console.Write("Enter goal # you want or type 'return' to go back: ");
                string input = Console.ReadLine();

                if (input.ToLower() == "return")
                {
                    return;
                }

                if (int.TryParse(input, out int index))
                {
                    index--;

                    if (index >= 0 && index < goals.Count)
                    {
                        goals[index].RecordEvent();
                        score += goals[index].GetPoints();
                        Console.WriteLine("Event recorded successfully.");
                        return;
                    }
                    else
                    {
                        //code to prevent crashing 
                        Console.WriteLine("Invalid goal number. Please try again.");
                    }
                }
                else
                {
                    //code to prevent crashing
                    Console.WriteLine("Invalid input. Please enter a valid number or 'return'.");
                }
            }
        }

        public void DisplayGoals()
        {
            Console.Clear();
            if (goals.Count == 0)
            {
                //runs if no goles have been created 
                Console.WriteLine("No goals have been created yet.");
            }
            else
            {
                for (int i = 0; i < goals.Count; i++)
                {
                    //prints goles and gole status 
                    Console.WriteLine($"{i + 1}. {goals[i].GetGoalStatus()}");
                }
            }
            //runs as a spacer to run next code
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        public void DisplayScore()
        {
            //gets current score from save data to display
            Console.WriteLine($"Your current score is: {score}");
            
            //for creativity i created code to show where you are standing 
            if (score == 0) {
                Console.WriteLine ("Your at 0 Serisly Zero thats litrery nothing Come back after doing some goles");
            }
            else if (score <= 5) {
                Console.WriteLine ($"Your Score is At {score} better than 0 you Are doing better Dont stop now elce you are Stuck Like Nephis Brothers");
            }
            else if (score <= 10) {
                Console.WriteLine ($"Your Score is At {score} you are inproving Greatly over time Keep it up ");
            }
            else if (score <= 25) {
                Console.WriteLine ($"Your Score is At {score} better than last time You are Doing Great");
            }
            else if (score <=50) {
                Console.WriteLine ($"Your Score is at {score} You have made it a long ways Congrats about half way to 100 =)");
            }
            else if (score == 69) {
                Console.WriteLine ($"Currently your score is 69 (Nice) You are seeing this as a hiden message congrats");
            }
            else if (score <= 99) {
                Console.WriteLine ($"wow your almost to 100");
            }
            else if (score == 100) {
                Console.WriteLine ($"Congrats You made it to 100 i never would have thought you made it this far");
                Console.WriteLine ("For you are Truly of Nephi of old");
            }
            else{
                Console.WriteLine("Nice Job Nothing elce to say Here keep up the Hard work Never Give up");
            }
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        public void SaveData(string filename)
        {
            //gathers save data and wrights new goles to save data
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(score);
                foreach (var goal in goals)
                {
                    writer.WriteLine(goal.Save());
                }
            }
        }

        public void LoadData(string filename)
        {
            //loads the save data for use 
            if (File.Exists(filename))
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    //reads the whole save data sending it to its proper child class for use
                    score = int.Parse(reader.ReadLine());
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //devides up data with the | symbole 
                        string[] data = line.Split('|');
                        BaseGoal goal = null;
                        switch (data[0])
                        {
                            //defignes the type of gole
                            case "SimpleGoal":
                                goal = new SimpleGoal();
                                break;
                            case "EternalGoal":
                                goal = new EternalGoal();
                                break;
                            case "ChecklistGoal":
                                goal = new ChecklistGoal();
                                break;
                        }
                        //checks if sucsessfully loaded if not create new save data file
                        goal?.Load(data);
                        goals.Add(goal);
                    }
                }
            }
        }
    }
}
