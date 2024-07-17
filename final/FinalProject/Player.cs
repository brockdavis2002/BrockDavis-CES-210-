using System;
using System.Collections.Generic;

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

        public Player(string playerName = "DefaultPlayerName")//at start of gam sets all values to defalt
        {
            Name = playerName;
            Health = 100;
            Inventory = new Inventory(startingFood: 200, startingAmmo: 100, startingMoney: 150); 
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

        public void Rest()
        {
            Console.WriteLine("You rest and regain some health.");
            Health += 10;
            Console.WriteLine($"Health is now {Health}");
        }

        public void CheckSupplies()
        {
            Console.Clear();
            Console.WriteLine("Current Supplies:");
            Inventory.DisplayInventory();
            Console.WriteLine("\nFamily Members:");
            foreach (var member in FamilyMembers)
            {
                Console.WriteLine(member);
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        public void HandleEvent(Event gameEvent)
        {
            gameEvent.Occur(null); // Pass game reference or needed data
            // Implement event handling logic here
        }

        public void AddMoney(int amount)
        {
            Money += amount;
        }

        public void SpendMoney(int amount)
        {
            if (Money >= amount)
            {
                Money -= amount;
                Console.WriteLine($"Spent ${amount}. Money left: ${Money}");
            }
            else
            {
                Console.WriteLine("Not enough money!");
            }
        }
    }
}
