using System;
using System.Collections.Generic;

namespace OregonTrailGame
{
    [Serializable]
    public class Inventory
    {
        private int food;
        private int ammo;
        private int money;

        // Add a property to manage the number of party members
        public int PartyCount { get; private set; }
        public List<string> PartyMembers { get; private set; }

        public Inventory(int startingFood, int startingAmmo, int startingMoney)
        {
            food = startingFood;
            ammo = startingAmmo;
            money = startingMoney;
            PartyCount = 4; // Example: Initialize with 4 party members
            PartyMembers = new List<string> { "Adult1", "Adult2", "Child1", "Child2" }; // Example: Initialize party members
        }

        public bool ConsumeFood(int amount)
        {
            if (food >= amount)
            {
                food -= amount;
                return true;
            }
            else
            {
                food = 0;
                return false;
            }
        }

        public void AddFood(int amount)
        {
            food += amount;
        }

        public bool ConsumeAmmo(int amount)
        {
            if (ammo >= amount)
            {
                ammo -= amount;
                return true;
            }
            else
            {
                ammo = 0;
                return false;
            }
        }

        public void AddAmmo(int amount)
        {
            ammo += amount;
        }

        public bool SpendMoney(int amount)
        {
            if (money >= amount)
            {
                money -= amount;
                return true;
            }
            else
            {
                Console.WriteLine("Not enough money!");
                return false;
            }
        }

        public void AddMoney(int amount)
        {
            money += amount;
        }

        public void DisplayInventory()
        {
            Console.WriteLine($"Food: {food} units");
            Console.WriteLine($"Ammo: {ammo} units");
            Console.WriteLine($"Money: ${money}");
            Console.WriteLine("\nParty Members Alive:");
            foreach (var member in PartyMembers)
            {
                Console.WriteLine(member);
            }
        }
    }
}
