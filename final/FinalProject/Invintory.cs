using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OregonTrailGame
{
    [Serializable]
    public class Inventory
    {
        private int food;
        private int ammo;
        private int money;
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
                Console.WriteLine("Not enough money!😭");
                return false;
            }
        }

        public void AddMoney(int amount)
        {
            money += amount;
        }

        public void ReducePartyCount()
        {
            if (PartyCount > 0)
            {
                PartyCount--;
                PartyMembers.RemoveAt(new Random().Next(PartyMembers.Count)); // Remove a random member
            }
        }

        public bool AreAllPartyMembersDead()
        {
            return PartyCount <= 0;
        }

        public int GetFood() => food;
        public int GetAmmo() => ammo;
        public int GetMoney() => money;

        public void DisplayInventory()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"Food: {food} units🍖");
            Console.WriteLine($"Ammo: {ammo} units🔫");
            Console.WriteLine($"Money: ${money}.🤑");
            Console.WriteLine("\nParty Members Alive👨‍👩‍👧‍👦:");
            foreach (var member in PartyMembers)
            {
                Console.WriteLine(member);
            }
        }

        public void SaveToFile(string filename)
        {
            SaveLoadToFile(filename, isSaving: true);
        }

        public void LoadFromFile(string filename)
        {
            SaveLoadToFile(filename, isSaving: false);
        }

        private void SaveLoadToFile(string filename, bool isSaving)
        {
            try
            {
                if (isSaving)
                {
                    using (StreamWriter writer = new StreamWriter(filename))
                    {
                        writer.WriteLine($"Food:{food}");
                        writer.WriteLine($"Ammo:{ammo}");
                        writer.WriteLine($"Money:{money}");
                        writer.WriteLine($"PartyCount:{PartyCount}");
                        writer.WriteLine($"PartyMembers:{string.Join(",", PartyMembers)}");
                    }
                    Console.WriteLine("Inventory data saved successfully.");
                }
                else
                {
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var parts = line.Split(':');
                            if (parts.Length == 2)
                            {
                                string key = parts[0].Trim();
                                string value = parts[1].Trim();

                                switch (key)
                                {
                                    case "Food":
                                        food = int.Parse(value);
                                        break;
                                    case "Ammo":
                                        ammo = int.Parse(value);
                                        break;
                                    case "Money":
                                        money = int.Parse(value);
                                        break;
                                    case "PartyCount":
                                        PartyCount = int.Parse(value);
                                        break;
                                    case "PartyMembers":
                                        PartyMembers = new List<string>(value.Split(','));
                                        break;
                                }
                            }
                        }
                    }
                    Console.WriteLine("Inventory data loaded successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {(isSaving ? "saving" : "loading")} inventory data: {ex.Message}");
            }
        }
    }
}
