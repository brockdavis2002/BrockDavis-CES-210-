using System;

namespace OregonTrailGame
{
    public class Blizzard : Disaster
    {
        public Blizzard(Player player) : base(player) { }

        protected override string GetDisasterName() => "Blizzard";

        protected override void HandleDisaster()
        {
            Console.WriteLine("A severe blizzard has struck.");
            int lostTurns = random.Next(1, 3);
            int lostFood = random.Next(5, 11);
            player.Inventory.ConsumeFood(lostFood);
            Console.WriteLine($"You lose {lostTurns} turns due to the blizzard and {lostFood} units of food.");
            // Add logic to lose turns
        }
    }
}
