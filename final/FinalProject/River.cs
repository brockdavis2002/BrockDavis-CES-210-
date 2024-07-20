using System;
using System.Text;

namespace OregonTrailGame
{
    [Serializable]
    public class River : Location
    {
        private int crossingDifficulty;

        public River(string name) : base(name)
        {
        }

        public override void Visit(Player player)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"You have reached the river: {Name}💦");
            Interact(player);
        }

        private void Interact(Player player)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Set the crossing difficulty once when the river is first visited
            crossingDifficulty = new Random().Next(1, 11); // Simulate crossing difficulty

            Console.WriteLine($"You approach {Name}.💦");
            Console.WriteLine($"The river crossing difficulty is {crossingDifficulty}.");
            Console.WriteLine("You need to decide how to cross the river.");
            Console.WriteLine("1. Float/Swim across the river🥽");
            Console.WriteLine("2. Pay for a raft to take you across.🛶");

            int choice = GetPlayerChoice(1, 2);

            switch (choice)
            {
                case 1:
                    FloatSwimAcross(player);
                    break;
                case 2:
                    PayForRaft(player);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        private void FloatSwimAcross(Player player)
        {
            Console.WriteLine($"The river crossing difficulty is {crossingDifficulty}.");

            bool success = new Random().Next(1, 101) <= 50; // 50% chance of success

            if (success)
            {
                Console.WriteLine("You successfully float/swim across the river.");
            }
            else
            {
                Console.WriteLine("You fail to cross the river!");

                // Example: Implement consequences of failing to cross
                int foodLost = new Random().Next(10, 31); // Random amount of food lost
                int ammoLost = new Random().Next(5, 16); // Random amount of ammo lost

                player.Inventory.ConsumeFood(foodLost);
                player.Inventory.ConsumeAmmo(ammoLost);
                Console.WriteLine($"You lost {foodLost} units of food 🍖 and {ammoLost} units of ammo 🔫.");

                // Check for family member risk
                if (crossingDifficulty >= 6) // Medium or hard difficulty
                {
                    int familyMemberRisk = new Random().Next(1, 101); // Risk percentage

                    if (familyMemberRisk <= 20) // 20% chance of family member dying
                    {
                        if (player.FamilyMembers.Count > 0)
                        {
                            // Randomly choose a family member to die
                            int index = new Random().Next(player.FamilyMembers.Count);
                            string deceasedMember = player.FamilyMembers[index];
                            player.FamilyMembers.RemoveAt(index);
                            Console.WriteLine($"{deceasedMember} has died due to the failed crossing 💀.");
                        }
                        else
                        {
                            Console.WriteLine("All family members have already perished 💀.");
                        }
                    }
                }

                // Check for wagon repair costs
                if (crossingDifficulty >= 6) // Medium or hard difficulty
                {
                    int repairCost = new Random().Next(1, 51); // Random repair cost between $1 and $50

                    if (player.Inventory.GetMoney() >= repairCost)
                    {
                        player.Inventory.SpendMoney(repairCost);
                        Console.WriteLine($"You need to pay ${repairCost} to repair the wagon 💲.");
                    }
                    else
                    {
                        Console.WriteLine("You do not have enough money to repair the wagon 💲.");
                        Console.WriteLine("You have lost the game due to lack of funds for repairs 😭.");
                        Environment.Exit(0); // End the game
                    }
                }
            }
        }

        private void PayForRaft(Player player)
        {
            int raftCost = CalculateRaftCost(crossingDifficulty);
            Console.WriteLine($"The cost to hire a raft is ${raftCost} 🛶💲.");

            if (player.Inventory.GetMoney() >= raftCost)
            {
                player.Inventory.SpendMoney(raftCost);
                Console.WriteLine("You pay for the raft and cross the river safely.");
            }
            else
            {
                Console.WriteLine("You don't have enough money to pay for the raft 💲.");
                Console.WriteLine("You must try another method to cross the river 😭.");
                FloatSwimAcross(player); // Offer another attempt to cross
            }
        }

        private int CalculateRaftCost(int difficulty)
        {
            // Calculate the cost based on difficulty
            return difficulty * 10; // For example, 10 per level of difficulty
        }

        private int GetPlayerChoice(int min, int max)
        {
            int choice = 0;
            bool validChoice = false;

            while (!validChoice)
            {
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out choice))
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
