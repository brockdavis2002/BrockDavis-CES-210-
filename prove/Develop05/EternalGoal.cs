using System;

namespace EternalQuest
//class for eternal goles child funckon of Base goal
{
    public class EternalGoal : BaseGoal
    {
        private int completionCount;
        //sets the number of times compelated
        private DateTime lastCompleted;
        //sets date of last compleation

        public EternalGoal()
        {
            //Checks the number of times compleated and sets a score to that
            completionCount = 0;
            lastCompleted = DateTime.MinValue;
        }

        public override void SetDetails()
        {
            //code to create a new Eternal Goal
            Console.Write("Enter goal name: ");
            name = Console.ReadLine();
            Console.WriteLine("Repeatibule recomended to set low Point score 1-5");
            Console.Write("Enter points per event: ");
            points = int.Parse(Console.ReadLine());
        }

        public override void RecordEvent()
        {
            //Adds one on compleation to coompleation count
            completionCount++;
            //overrides date of compleation
            lastCompleted = DateTime.Now;
        }

        public override string GetGoalStatus()
        {
            //records the # of times compeated for user to view
            string status = $"{name} (Eternal) - Completed {completionCount} times";
            if (completionCount > 0)
            {
                //records the date and time of compleation for display
                status += $", Last Completed on {lastCompleted.ToShortDateString()}";
            }
            return status;
        }

        public override int GetPoints()
        {
            //returns # of points on each time compelated
            return points;
        }

        public override string Save()
        {
            //formats for save in save data folder
            return $"EternalGoal|{name}|{points}|{completionCount}|{lastCompleted}";
        }

        public override void Load(string[] data)
        {
            //formats for retreaving form save data folder
            name = data[1];
            points = int.Parse(data[2]);
            completionCount = int.Parse(data[3]);
            lastCompleted = DateTime.Parse(data[4]);
        }
    }
}
