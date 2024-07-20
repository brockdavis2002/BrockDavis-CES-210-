using System;

namespace OregonTrailGame
{
    public class Tornado : Disaster
    {
        public Tornado(Player player) : base(player) { }

        protected override string GetDisasterName() => "Tornado";

        protected override void HandleDisaster()
        {
            Console.WriteLine("A tornado has overturned your wagon.");
            int lostFood = random.Next(5, 16);
            int lostAmmo = random.Next(3, 11);
            player.Inventory.ConsumeFood(lostFood);
            player.Inventory.ConsumeAmmo(lostAmmo);
            Console.WriteLine($"You lose {lostFood} units of food and {lostAmmo} units of ammo. Your wagon is damaged.");
            // Add logic for wagon damage
        }
    }
}
