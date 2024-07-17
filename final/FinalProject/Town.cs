using System;

namespace OregonTrailGame
{
    public class Town : Location
    {
        private const int MoneyAtTown = 25; // Amount of money earned at each town visit

        public Town(string name) : base(name)
        {
        }

        public override void Visit(Player player)
        {
            Console.WriteLine($"You have arrived at {Name}.");
            Console.WriteLine("Welcome to town!");

            Interact(player); // Call the interact method for player interaction
        }

        public void Interact(Player player)
        {
            Console.WriteLine($"Welcome to {Name}!");
            Console.WriteLine("You can buy supplies here.");

            // Example: Implement buying supplies logic
            BuySupplies(player);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        private void BuySupplies(Player player)
        {
            Console.WriteLine("1. Buy Food");
            Console.WriteLine("2. Buy Ammo");
            Console.WriteLine("3. Leave Town");

            int choice = GetPlayerChoice(1, 3);

            switch (choice)
            {
                case 1:
                    BuyFood(player);
                    break;
                case 2:
                    BuyAmmo(player);
                    break;
                case 3:
                    Console.WriteLine("Leaving town...");
                    break;
            }
        }

        private void BuyFood(Player player)
        {
            Console.Write("Enter amount of food to buy 4$: ");
            int amountToBuy = int.Parse(Console.ReadLine());

            // Example: Implement buying food logic
            player.Inventory.AddFood(amountToBuy);
            player.Inventory.SpendMoney(amountToBuy * 4); // Food price is 2 dollars per unit
            Console.WriteLine($"You bought {amountToBuy} units of food.");
        }

        private void BuyAmmo(Player player)
        {
            Console.Write("Enter amount of ammo to buy 5$: ");
            int amountToBuy = int.Parse(Console.ReadLine());

            // Example: Implement buying ammo logic
            player.Inventory.AddAmmo(amountToBuy);
            player.Inventory.SpendMoney(amountToBuy * 5); // Ammo price is 5 dollars per unit
            Console.WriteLine($"You bought {amountToBuy} units of ammo.");
        }

        private int GetPlayerChoice(int min, int max)
        {
            int choice = 0;
            bool validChoice = false;

            while (!validChoice)
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice >= min && choice <= max)
                    {
                        validChoice = true;
                    }
                    else
                    {
                        Console.WriteLine($"Please enter a number between {min} and {max}.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            return choice;
        }
    }
}
