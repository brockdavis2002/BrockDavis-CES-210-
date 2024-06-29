namespace EternalQuest
//parent class to checklist eternal and simple recording the data and gathering it for the use 
//saving it to the save data file though program class
{
    public abstract class BaseGoal
    {
        protected string name;
        protected int points;

        public abstract void SetDetails();
        public abstract void RecordEvent();
        public abstract string GetGoalStatus();
        public abstract int GetPoints();
        public abstract string Save();
        public abstract void Load(string[] data);
    }
}
