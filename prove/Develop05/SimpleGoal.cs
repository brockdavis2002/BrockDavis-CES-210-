using System;

namespace EternalQuest
{
    public class SimpleGoal : BaseGoal
    {
    //Child class to Basegoal class runs the code for simplegoals
        private bool isCompleted;
        //ckecks if compleated privat so you have to compleat it for this to return any points

        public override void SetDetails()
        {
            //sets information overriding privit/protected data
            Console.Write("Enter goal name: ");
            name = Console.ReadLine();
            Console.Write("Enter points for completion: ");
            points = int.Parse(Console.ReadLine());
            isCompleted = false;
        }

        public override void RecordEvent()
        {
            //checks off to ture when compleated
            isCompleted = true;
        }

        public override string GetGoalStatus()
        {
            //return back[x] if compleated and[ ] if not
            return isCompleted ? $"[X] {name}" : $"[ ] {name}";
        }

        public override int GetPoints()
        {
            //returns back points if compleated
            return isCompleted ? points : 0;
        }

        public override string Save()
        {
            //saves into format in save data file runing in main
            return $"SimpleGoal|{name}|{points}|{isCompleted}";
        }

        public override void Load(string[] data)
        {
            //for loading in data from save file
            name = data[1];
            points = int.Parse(data[2]);
            isCompleted = bool.Parse(data[3]);
        }
    }
}
