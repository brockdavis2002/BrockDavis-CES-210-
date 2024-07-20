using System;

namespace OregonTrailGame
{
    public class Town : Location
    {
        private const int MoneyAtTown = 25; // Amount of money earned at each town visit
        private const int FoodPrice = 15; // Price per unit of food
        private const int AmmoPrice = 10; // Price per unit of ammo

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

            // Add money for visiting the town
            player.Inventory.AddMoney(MoneyAtTown);

            bool continueShopping = true;

            while (continueShopping)
            {
                // Display current inventory status before making a choice
                DisplayInventoryStatus(player);

                // Example: Implement buying supplies logic
                continueShopping = BuySupplies(player);
            }

            Console.WriteLine("Leaving town...");
        }

        private void DisplayInventoryStatus(Player player)
        {
            Console.WriteLine("\nCurrent Inventory Status:");
            Console.WriteLine($"Money: ${player.Inventory.GetMoney()}");
            Console.WriteLine($"Food: {player.Inventory.GetFood()} units");
            Console.WriteLine($"Ammo: {player.Inventory.GetAmmo()} units");
        }

        private bool BuySupplies(Player player)
        {
            Console.WriteLine("\nWhat would you like to do?");
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
                    return false; // Exit the loop and leave town
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }

            return true; // Continue shopping
        }

        private void BuyFood(Player player)
        {
            Console.Write($"Enter amount of food to buy (${FoodPrice} each): ");
            if (int.TryParse(Console.ReadLine(), out int amountToBuy) && amountToBuy > 0)
            {
                int totalCost = amountToBuy * FoodPrice;

                if (player.Inventory.SpendMoney(totalCost))
                {
                    player.Inventory.AddFood(amountToBuy);
                    Console.WriteLine($"You bought {amountToBuy} units of food.");
                }
                else
                {
                    Console.WriteLine("Not enough money to buy the food.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
            }
        }

        private void BuyAmmo(Player player)
        {
            Console.Write($"Enter amount of ammo to buy (${AmmoPrice} each): ");
            if (int.TryParse(Console.ReadLine(), out int amountToBuy) && amountToBuy > 0)
            {
                int totalCost = amountToBuy * AmmoPrice;

                if (player.Inventory.SpendMoney(totalCost))
                {
                    player.Inventory.AddAmmo(amountToBuy);
                    Console.WriteLine($"You bought {amountToBuy} units of ammo.");
                }
                else
                {
                    Console.WriteLine("Not enough money to buy the ammo.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
            }
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
