using System;

namespace OregonTrailGame
{
    public class Flooding : Disaster
    {
        public Flooding(Player player) : base(player) { }

        protected override string GetDisasterName() => "Flooding";

        protected override void HandleDisaster()
        {
            Console.WriteLine("Your wagon is submerged in water.");
            int repairTurns = random.Next(1, 4);
            Console.WriteLine($"You need to spend {repairTurns} turns repairing the wagon.");
            // Add logic to reduce the number of turns
        }
    }
}
