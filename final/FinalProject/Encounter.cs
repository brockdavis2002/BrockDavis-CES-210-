using System;

namespace OregonTrailGame
{
    public class Encounter : Event
    {
        private Random random = new Random();

        public override void Occur(Player player)
        {
            int encounterType = random.Next(1, 7); // Adjust the range based on the number of encounters

            switch (encounterType)
            {
                case 1:
                    BanditEncounter(player);
                    break;
                case 2:
                    TraderEncounter(player);
                    break;
                case 3:
                    MerchantEncounter(player);
                    break;
                case 4:
                    HunterEncounter(player);
                    break;
                case 5:
                    WildAnimalEncounter(player);
                    break;
                case 6:
                    FunEncounter(player);
                    break;
                default:
                    Console.WriteLine("Nothing happens. You continue on your way.");
                    break;
            }
        }

        private void BanditEncounter(Player player)
        {
            var inventory = player.Inventory;
            int amountStolen = random.Next(10, 51);
            int typeOfStolen = random.Next(1, 4); // 1: Food, 2: Ammo, 3: Money

            Console.WriteLine("A bandit has appeared!");

            switch (typeOfStolen)
            {
                case 1:
                    inventory.ConsumeFood(amountStolen);
                    Console.WriteLine($"The bandit stole {amountStolen} units of food.");
                    break;
                case 2:
                    inventory.ConsumeAmmo(amountStolen);
                    Console.WriteLine($"The bandit stole {amountStolen} units of ammo.");
                    break;
                case 3:
                    inventory.SpendMoney(amountStolen);
                    Console.WriteLine($"The bandit stole ${amountStolen}.");
                    break;
            }
        }

        private void TraderEncounter(Player player)
        {
            var inventory = player.Inventory;
            Console.WriteLine("You meet a trader.");

            Console.WriteLine("You can trade food for money or ammo.");
            Console.Write("Enter amount of food to trade: ");
            int foodToTrade = int.Parse(Console.ReadLine());

            if (inventory.ConsumeFood(foodToTrade))
            {
                int moneyEarned = foodToTrade * 2; //conversion rate
                inventory.AddMoney(moneyEarned);
                Console.WriteLine($"You traded {foodToTrade} units of food for ${moneyEarned}.");
            }
            else
            {
                Console.WriteLine("You don't have enough food to trade.");
            }
        }

        private void MerchantEncounter(Player player)
        {
            var inventory = player.Inventory;
            Random random = new Random();

            // Generate random costs for food and ammo
            int costPerUnitFood = random.Next(1, 11); // Random cost between $1 and $10 per unit of food
            int costPerUnitAmmo = random.Next(1, 11); // Random cost between $1 and $10 per unit of ammo

            Console.WriteLine("You meet a merchant.");

            // Display current supplies and money
            Console.WriteLine("\nCurrent Supplies:");
            Console.WriteLine($"Food: {inventory.GetFood()} units");
            Console.WriteLine($"Ammo: {inventory.GetAmmo()} units");
            Console.WriteLine($"Money: ${inventory.GetMoney()}");

            Console.WriteLine("\nYou can buy supplies from the merchant.");
            Console.WriteLine("1. Buy Food");
            Console.WriteLine("2. Buy Ammo");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write($"Enter amount of food to buy (Cost: ${costPerUnitFood} per unit): ");
                    int foodToBuy = int.Parse(Console.ReadLine());
                    int costOfFood = foodToBuy * costPerUnitFood;
                    if (inventory.SpendMoney(costOfFood))
                    {
                        inventory.AddFood(foodToBuy);
                        Console.WriteLine($"You bought {foodToBuy} units of food at ${costPerUnitFood} per unit.");
                    }
                    else
                    {
                        Console.WriteLine("Not enough money.");
                    }
                    break;
                case 2:
                    Console.Write($"Enter amount of ammo to buy (Cost: ${costPerUnitAmmo} per unit): ");
                    int ammoToBuy = int.Parse(Console.ReadLine());
                    int costOfAmmo = ammoToBuy * costPerUnitAmmo;
                    if (inventory.SpendMoney(costOfAmmo))
                    {
                        inventory.AddAmmo(ammoToBuy);
                        Console.WriteLine($"You bought {ammoToBuy} units of ammo at ${costPerUnitAmmo} per unit.");
                    }
                    else
                    {
                        Console.WriteLine("Not enough money.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }


        private void HunterEncounter(Player player)
        {
            var inventory = player.Inventory;
            Console.WriteLine("You meet a hunter with surplus food.");

            Console.WriteLine("You can lend items to the hunter.");
            Console.Write("Enter amount of food to lend: ");
            int foodToLend = int.Parse(Console.ReadLine());

            if (inventory.ConsumeFood(foodToLend))
            {
                Console.WriteLine($"You lent {foodToLend} units of food to the hunter.");
                int foodGained = foodToLend; // you get the same amount back
                inventory.AddFood(foodGained);
                Console.WriteLine($"The hunter gave you {foodGained} units of food in return.");
            }
            else
            {
                Console.WriteLine("You don't have enough food to lend.");
            }
        }

private void WildAnimalEncounter(Player player)
{
    Console.WriteLine("You encounter a wild animal!");

    int huntOutcome = random.Next(1, 11); // Simulate hunting success with a 50% chance

    if (huntOutcome <= 6) // 60% chance of success
    {
        int foodFound = random.Next(10, 31); // Random amount of food gained
        player.Inventory.AddFood(foodFound);
        Console.WriteLine($"You successfully hunted the animal and gained {foodFound} units of food.");
    }
    else
    {
        Console.WriteLine("The hunt was unsuccessful!");

        // Check if there are any party members left
        if (player.Inventory.PartyMembers.Count > 0)
        {
            // Randomly choose one of the party members to be affected
            string affectedMember = player.Inventory.PartyMembers[new Random().Next(player.Inventory.PartyMembers.Count)];
            Console.WriteLine($"{affectedMember} was harmed during the hunt!");

            // Remove a party member and check if game is over
            player.Inventory.ReducePartyCount();
            if (player.Inventory.AreAllPartyMembersDead())
            {
                Console.WriteLine("All party members are dead. Game Over.");
            }
            else
            {
                Console.WriteLine($"Remaining party members:");
                foreach (var member in player.Inventory.PartyMembers)
                {
                    Console.WriteLine(member);
                }
            }
        }
        else
        {
            Console.WriteLine("No party members left to be harmed You now Die. Game Over.");
        }
    }
}


        private void FunEncounter(Player player)
        {
            Console.WriteLine("You stumble upon a carnival with games and fun!");

            Console.WriteLine("You can play a game for a chance to win prizes.");
            int prize = random.Next(1, 6); // 1 to 5 prize levels

            switch (prize)
            {
                case 1:
                    Console.WriteLine("You won a small prize! Gain 10 units of food.");
                    player.Inventory.AddFood(20);
                    break;
                case 2:
                    Console.WriteLine("You won a medium prize! Gain 20 units of ammo.");
                    player.Inventory.AddAmmo(20);
                    break;
                case 3:
                    Console.WriteLine("You won a big prize! Gain $50.");
                    player.Inventory.AddMoney(50);
                    break;
                case 4:
                    Console.WriteLine("You won a grand prize! Gain a free Recorces.");
                    player.Inventory.AddFood(40);
                    player.Inventory.AddAmmo(20);
                    player.Inventory.AddMoney(50);
                    break;
                case 5:
                    Console.WriteLine("You won nothing this time. Better luck next time!");
                    break;
            }
        }
    }
}
