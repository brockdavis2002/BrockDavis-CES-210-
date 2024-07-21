using System;
using System.Text;

namespace OregonTrailGame
{
    [Serializable]
    public class Town : Location
    {
        private const int MoneyAtTown = 25; // Amount of money earned at each town visit

        public Town(string name) : base(name)
        {
        }

        public override void Visit(Player player)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"You have arrived at {Name}.");
            Console.WriteLine("Welcome to town!🏠");

            Interact(player); // Call the interact method for player interaction
        }

        private void Interact(Player player)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"Welcome to {Name}!");
            Console.WriteLine("You can buy supplies here.🛒");

            // Add money for visiting the town
            player.Inventory.AddMoney(MoneyAtTown);

            bool continueShopping = true;

            while (continueShopping)
            {
                // Display current inventory status before making a choice
                DisplayInventoryStatus(player);
                continueShopping = BuySupplies(player);
            }

            Console.WriteLine("Leaving town...");
        }

        private void DisplayInventoryStatus(Player player)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\nCurrent Inventory Status:");
            Console.WriteLine($"Money: ${player.Inventory.GetMoney()}💲");
            Console.WriteLine($"Food: {player.Inventory.GetFood()} units🍖");
            Console.WriteLine($"Ammo: {player.Inventory.GetAmmo()} units🔫");
        }

        private bool BuySupplies(Player player)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("1. Buy Food🍖");
            Console.WriteLine("2. Buy Ammo🔫");
            Console.WriteLine("3. Leave Town🐂");

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

        private int GetRandomFoodPrice()
        {
            Random random = new Random();
            // Define the price range for food
            int minPrice = 4;
            int maxPrice = 20;
            return random.Next(minPrice, maxPrice + 1); // +1 to include maxPrice in the range
        }
        private void BuyFood(Player player)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int FoodPrice = GetRandomFoodPrice(); // Get a random price for food
            Console.Write($"Enter amount of food to buy (${FoodPrice} for 4 units):🍖 ");
            if (int.TryParse(Console.ReadLine(), out int amountToBuy) && amountToBuy > 0)
            {
                int totalCost = amountToBuy * FoodPrice;
                int totalpurchas = amountToBuy * 4;
                if (player.Inventory.SpendMoney(totalCost))
                {
                    player.Inventory.AddFood(totalpurchas);
                    Console.WriteLine($"You bought {totalpurchas} units of food.🍖");
                }
                else
                {
                    Console.WriteLine("Not enough money to buy the food.🤑");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
            }
        }

        private int GetRandomAmmoPrice()
        {
            Random random = new Random();
            // Define the price range for ammo
            int minPrice = 3;
            int maxPrice = 25;
            return random.Next(minPrice, maxPrice + 1); // +1 to include maxPrice in the range
        }
        private void BuyAmmo(Player player)
        {
            int AmmoPrice = GetRandomAmmoPrice(); // Get a random price for ammo
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write($"Enter amount of ammo to buy (${AmmoPrice} for 5 units):🔫 ");
            if (int.TryParse(Console.ReadLine(), out int amountToBuy) && amountToBuy > 0)
            {
                int totalCost = amountToBuy * AmmoPrice;
                int totalpurchas = amountToBuy * 5;
                if (player.Inventory.SpendMoney(totalCost))
                {
                    player.Inventory.AddAmmo(totalpurchas);
                    Console.WriteLine($"You bought {totalpurchas} units of ammo.🔫");
                }
                else
                {
                    Console.WriteLine("Not enough money to buy the ammo.🤑");
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
