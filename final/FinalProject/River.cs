using System;

namespace OregonTrailGame
{
    public class River : Location
    {
        public River(string name) : base(name)
        {
        }

        public override void Visit(Player player)
        {
            Console.WriteLine($"You have reached the river: {Name}");
            Interact(player);
        }

        public void Interact(Player player)
        {
            Console.WriteLine($"You approach {Name}.");
            Console.WriteLine("You need to decide how to cross the river.");

            // Example: Implement river crossing logic
            CrossRiver(player);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        private void CrossRiver(Player player)
        {
            int crossingDifficulty = new Random().Next(1, 11); // Simulate crossing difficulty

            Console.WriteLine($"The river crossing difficulty is {crossingDifficulty}.");

            if (crossingDifficulty <= 5) // Example: 50% chance of success
            {
                Console.WriteLine("You successfully cross the river.");
            }
            else
            {
                Console.WriteLine("You fail to cross the river!");

                // Example: Implement consequences of failing to cross
                int foodLost = new Random().Next(10, 31); // Random amount of food lost
                int ammoLost = new Random().Next(5, 16); // Random amount of ammo lost

                player.Inventory.ConsumeFood(foodLost);
                player.Inventory.ConsumeAmmo(ammoLost);

                Console.WriteLine($"You lost {foodLost} units of food and {ammoLost} units of ammo.");
            }
        }
    }
}
