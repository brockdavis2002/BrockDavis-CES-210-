using System;

namespace OregonTrailGame
{
    public class Stampede : Disaster
    {
        public Stampede(Player player) : base(player) { }

        protected override string GetDisasterName() => "Stampede";

        protected override void HandleDisaster()
        {
            Console.WriteLine("A stampede has caused chaos.");
            if (random.NextDouble() < 0.03) // 3% chance
            {
                Console.WriteLine("Multiple family members have died in the stampede.");
                int deaths = random.Next(2, 4);
                for (int i = 0; i < deaths; i++)
                {
                    if (player.Inventory.PartyMembers.Count > 0)
                    {
                        player.Inventory.PartyMembers.RemoveAt(random.Next(player.Inventory.PartyMembers.Count));
                        player.Inventory.ReducePartyCount();
                    }
                }
            }
            else
            {
                Console.WriteLine("A few supplies were lost in the chaos.");
                int lostFood = random.Next(5, 11);
                int lostAmmo = random.Next(3, 6);
                player.Inventory.ConsumeFood(lostFood);
                player.Inventory.ConsumeAmmo(lostAmmo);
                Console.WriteLine($"You lose {lostFood} units of food and {lostAmmo} units of ammo.");
            }
        }
    }
}
