using System;
using System.Collections.Generic;
using System.Text;
namespace OregonTrailGame
{
    [Serializable]
    public class Player
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Money { get; private set; }
        public Inventory Inventory { get; private set; }
        public List<string> FamilyMembers { get; private set; }
        private Random random;
        private Game game;

        public Player(string playerName = "DefaultPlayerName")
        {
            Name = playerName;
            Health = 100;
            Inventory = new Inventory(startingFood: 200, startingAmmo: 60, startingMoney: 150);
            FamilyMembers = new List<string>();
            random = new Random();
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void AddFamilyMember(string name)
        {
            FamilyMembers.Add(name);
        }

        public void Rest(int turns)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"You rest for {turns} turns and regain some health❤️‍🩹.");

            for (int i = 0; i < turns; i++)
            {
                if (!Inventory.ConsumeFood(10)) // Consuming 10 units of food per resting turn
                {
                    Console.WriteLine($"Not enough food to continue resting!🍖");
                    break;
                }
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Health += 10; // Regain health per turn
                if (Health > 100) Health = 100; // Cap health at 100%
                Console.WriteLine($"Health is now {Health}.❤️‍🩹");
                System.Threading.Thread.Sleep(500); // Simulate the passage of time
            }
        }

        public void CheckSupplies()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();
            Console.WriteLine("Current Supplies:");
            Inventory.DisplayInventory();
            Console.WriteLine($"\nFamily Members:👨‍👩‍👧‍👦");
            foreach (var member in FamilyMembers)
            {
                Console.WriteLine(member);
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadKey(true);
        }

        public void HandleEvent(Event gameEvent)
        {
            gameEvent.Occur(this); // Pass the player to the event
            // Implement event handling logic here
        }

        public void AddMoney(int amount)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Money += amount;
            Console.WriteLine($"Added ${amount}. Money now: ${Money}💲");
        }

        public void SpendMoney(int amount)
        {
            if (Money >= amount)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Money -= amount;
                Console.WriteLine($"Spent ${amount}. Money left: ${Money}💲");
            }
            else
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine($"Not enough money!😭");
            }
        }

        public void RemoveFamilyMember(string memberName)
        {
            if (FamilyMembers.Remove(memberName))
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Inventory.ReducePartyCount();
                Console.WriteLine($"{memberName} has died.🪦");
                if (Inventory.PartyCount == 0)
                {
                    Console.WriteLine($"All party members are dead. Game Over.💀");
                    game.EndGame();
                    Environment.Exit(0); // End the game if all party members are dead
                }
            }
            else
            {
                Console.WriteLine($"{memberName} not found in the family.");
            }
        }
    }
}
