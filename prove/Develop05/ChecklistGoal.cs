using System;

namespace EternalQuest
{
    //code for checklist gole child funcion for Base Goal
    public class ChecklistGoal : BaseGoal
    {
        private int requiredCount;
        //sets the number of times requred to finish
        private int currentCount;
        //sets the current number of compleated times
        private int bonusPoints;
        //sets the bones points on compleation

        public override void SetDetails()
        {
            //code to create a new checklist goal
            Console.Write("Enter goal name: ");
            name = Console.ReadLine();
            Console.Write("Enter points per event: ");
            points = int.Parse(Console.ReadLine());
            Console.Write("Enter required count for completion: ");
            requiredCount = int.Parse(Console.ReadLine());
            Console.Write("Enter bonus points for completion: ");
            bonusPoints = int.Parse(Console.ReadLine());
            currentCount = 0;
        }

        public override void RecordEvent()
        {
            //adds on to the number of times compleated
            currentCount++;
        }

        public override string GetGoalStatus()
        {
            //returns [x] if all times are compleated returns[ ] if not met
            return currentCount >= requiredCount ? $"[X] {name} (Completed)" : $"[ ] {name} (Completed {currentCount}/{requiredCount} times)";
        }

        public override int GetPoints()
        {
            //code to return regurner points if under the number of times compleated +bonis if over or =
            return currentCount >= requiredCount ? points + bonusPoints : points;
        }

        public override string Save()
        {
            //formats for save to save data
            return $"ChecklistGoal|{name}|{points}|{requiredCount}|{currentCount}|{bonusPoints}";
        }

        public override void Load(string[] data)
        //formats for loading save data
        {
            name = data[1];
            points = int.Parse(data[2]);
            requiredCount = int.Parse(data[3]);
            currentCount = int.Parse(data[4]);
            bonusPoints = int.Parse(data[5]);
        }
    }
}
