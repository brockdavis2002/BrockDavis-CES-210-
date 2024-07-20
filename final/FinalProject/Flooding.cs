using System;
using System.Text;
namespace OregonTrailGame
{
    [Serializable]
    public class Flooding : Disaster
    {
        public Flooding(Player player) : base(player) { }

        protected override string GetDisasterName() => "Flooding";

        protected override void HandleDisaster()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"Your wagon is submerged in water.🌊");
            int repairTurns = random.Next(1, 4);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"You need to spend {repairTurns}😥 turns repairing the wagon.");
            // Add logic to reduce the number of turns
        }
    }
}
