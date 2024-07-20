using System;

namespace OregonTrailGame
{
    public class NativeAmericanAttack : Disaster
    {
        public NativeAmericanAttack(Player player) : base(player) { }

        protected override string GetDisasterName() => "Native American Attack";

        protected override void HandleDisaster()
        {
            Console.WriteLine("You are attacked by Native Americans.");
                        // Example consequence: party member injured or kidnapped
            if (random.NextDouble() < 0.2) // 20% chance of injury
            {
                Console.WriteLine("A party member has been injured.");
                int injuredMemberIndex = random.Next(player.Inventory.PartyMembers.Count);
                Console.WriteLine($"{player.Inventory.PartyMembers[injuredMemberIndex]} is injured.");
            }
            else
            {
                Console.WriteLine("A party member has been kidnapped!");

                // Player must decide to search or leave
                Console.WriteLine("1. Search for the kidnapped member");
                Console.WriteLine("2. Leave them behind");
                int choice = GetPlayerChoice(1, 2);

                if (choice == 1)
                {
                    int searchTurns = random.Next(1, 5);
                    Console.WriteLine($"Searching for {searchTurns} turns...");

                    if (random.NextDouble() < 0.5) // 50% chance of finding
                    {
                        Console.WriteLine("You found the kidnapped member.");
                    }
                    else
                    {
                        Console.WriteLine("You failed to find the kidnapped member.");
                        player.Inventory.ReducePartyCount();
                        player.Inventory.PartyMembers.RemoveAt(random.Next(player.Inventory.PartyMembers.Count));
                    }
                }
                else
                {
                    Console.WriteLine("You chose to leave the kidnapped member behind.");
                    player.Inventory.ReducePartyCount();
                    player.Inventory.PartyMembers.RemoveAt(random.Next(player.Inventory.PartyMembers.Count));
                }
            }
        }
    }
}

