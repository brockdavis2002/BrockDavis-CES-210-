using System;

namespace OregonTrailGame
{
    public class Sickness : Event
    {
        private Player player;
        private Random random;
        private int requiredRestTurns;

        public Sickness(Player player)
        {
            this.player = player;
            this.random = new Random();
        }

        public override void Occur(Player player)
        {
            Console.WriteLine("A family member has fallen sick!");

            // Randomly select a family member to be sick
            int sickMemberIndex = random.Next(player.FamilyMembers.Count);
            string sickMember = player.FamilyMembers[sickMemberIndex];
            Console.WriteLine($"{sickMember} is sick!");

            // Determine random number of turns required to rest
            requiredRestTurns = random.Next(1, 6);
            Console.WriteLine($"You need to rest for {requiredRestTurns} turns to treat the sickness.");

            Console.WriteLine("1. Rest");
            Console.WriteLine("2. Ignore");

            int choice = GetPlayerChoice(1, 2);

            if (choice == 1)
            {
                Rest(sickMember, sickMemberIndex);
            }
            else
            {
                HandleDeath(sickMember, sickMemberIndex);
            }
        }

        private void Rest(string sickMember, int sickMemberIndex)
        {
            Console.WriteLine("You chose to rest. Each turn will consume some food.");

            int turnsRested = 0;
            while (turnsRested < requiredRestTurns)
            {
                if (!player.Inventory.ConsumeFood(5)) // Consuming 5 units of food per resting turn
                {
                    Console.WriteLine("Not enough food to continue resting!");
                    break;
                }

                Console.WriteLine($"Resting... {requiredRestTurns - turnsRested} turns remaining.");
                turnsRested++;

                // Simulate passing of turns
                System.Threading.Thread.Sleep(1000); // Simulate one turn pause

                // Check if required rest turns have been completed
                if (turnsRested >= requiredRestTurns)
                {
                    Console.WriteLine($"{sickMember} has recovered from sickness.");
                    return;
                }
            }

            // If not enough turns rested, the member dies
            HandleDeath(sickMember, sickMemberIndex);
        }

        private void HandleDeath(string sickMember, int sickMemberIndex)
        {
            Console.WriteLine($"{sickMember} has died due to lack of proper rest.");
            player.Inventory.ReducePartyCount(); // Reduce party count as a member has died
            player.FamilyMembers.RemoveAt(sickMemberIndex); // Remove the deceased member from the list

            if (player.Inventory.PartyCount == 0)
            {
                Console.WriteLine("All party members are dead. Game Over.");
                Environment.Exit(0); // End the game if all party members are dead
            }

            // Add to score (turns)
            Console.WriteLine("You lose a turn due to the sickness.");
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
