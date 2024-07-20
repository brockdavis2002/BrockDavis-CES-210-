using System;

namespace OregonTrailGame
{
    public class BugInfestation : Disaster
    {
        public BugInfestation(Player player) : base(player) { }

        protected override string GetDisasterName() => "Bug Infestation";

        protected override void HandleDisaster()
        {
            Console.WriteLine("Your supplies are being devoured by bugs.");
            int lostFood = random.Next(10, 21);
            player.Inventory.ConsumeFood(lostFood);
            Console.WriteLine($"You lose {lostFood} units of food due to the infestation.");
        }
    }
}
