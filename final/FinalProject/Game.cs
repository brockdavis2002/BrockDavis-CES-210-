using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace OregonTrailGame
{
    public class Game
    {
        private Player player;
        private List<Location> locations;
        private int currentLocationIndex;
        private Random random;
        private int turnsSinceLastTown;
        private int turnsToNextTown;

        public Game()
        {
            // Initialize game state
            player = new Player(); // Use default constructor
            locations = new List<Location>
            {
                new Town("Independence"),
                new River("Kansas River"),
                new Town("Fort Kearney"),
                new Town("Chimney Rock"),
                new Town("Fort Laramie"),
                new Town("Independence Rock"),
                new Town("South Pass"),
                new Town("Green River"),
                new Town("Fort Bridger"),
                new Town("Soda Springs"),
                new Town("Fort Hall"),
                new Town("Snake River"),
                new Town("Fort Boise"),
                new Town("Blue Mountains"),
                new Town("The Dalles"),
                new Town("Oregon City")
            };
            currentLocationIndex = 0;
            random = new Random();
            turnsSinceLastTown = 0;
            turnsToNextTown = random.Next(10, 21);

            Start();
        }

        public void Start()
        {
            InitializeGame();
            RunGameLoop();
        }

private void InitializeGame()
{
    Console.WriteLine("Welcome to The Oregon Trail!");

    // Initialize player
    Console.Write("Enter your name: ");
    string playerName = Console.ReadLine();
    player.SetName(playerName);

    // Initialize family members
    for (int i = 1; i <= 4; i++) // Assuming 4 family members
    {
        Console.Write($"Enter name for family member {i}: ");
        string familyMemberName = Console.ReadLine();
        player.AddFamilyMember(familyMemberName);
    }
}



        private void RunGameLoop()
        {
            bool gameOver = false;
            int turnsSurvived = 0;

            while (!gameOver)
            {
                //Console.Clear();
                turnsSurvived++;
                turnsSinceLastTown++;

                // Consume food based on number of party members
                int foodConsumed = random.Next(1, 5) * player.Inventory.PartyCount; // Random amount per turn
                if (!player.Inventory.ConsumeFood(foodConsumed))
                {
                    Console.WriteLine("You ran out of food! Game Over.");
                    break;
                }

                Location currentLocation = locations[currentLocationIndex];

                Console.WriteLine($"Current Location: {currentLocation.Name}");
                Console.WriteLine("1. Travel to next location");
                Console.WriteLine("2. Rest");
                Console.WriteLine("3. Hunt");
                Console.WriteLine("4. Check supplies");
                Console.WriteLine("5. Save and Quit");

                int choice = GetPlayerChoice(1, 5);

                switch (choice)
                {
                    case 1:
                        if (turnsSinceLastTown >= turnsToNextTown)
                        {
                            currentLocationIndex++;
                            if (currentLocationIndex >= locations.Count)
                            {
                                Console.WriteLine("You have reached Oregon City! Congratulations, you win!");
                                gameOver = true;
                            }
                            else
                            {
                                HandleLocationEvent(currentLocation);
                                turnsSinceLastTown = 0;
                                turnsToNextTown = random.Next(10, 21);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"You need to travel {turnsToNextTown - turnsSinceLastTown} more turns to reach the next town.");
                        }
                        break;
                    case 2:
                        player.Rest();
                        break;
                    case 3:
                        Hunting hunting = new Hunting();
                        hunting.Hunt(player.Inventory);
                        break;
                    case 4:
                        player.CheckSupplies();
                        break;
                    case 5:
                        SaveGame();
                        Console.WriteLine("Game saved. Goodbye!");
                        gameOver = true;
                        break;
                }

                // Handle random event each turn
                HandleRandomEvent();
            }
        }

        private void HandleLocationEvent(Location location)
        {
            location.Visit(player);
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }


        private void HandleRandomEvent()
        {
            // Simulate a random event each turn
            int randomEvent = random.Next(1, 5); // Example: 1-4 for different types of events

            switch (randomEvent)
            {
                case 1:
                    Event encounter = new Encounter();
                    player.HandleEvent(encounter);
                    break;
                case 2:
                    string[] partyMembers = { "Adult1", "Adult2", "Child1", "Child2" };
                    string personAffected = partyMembers[random.Next(partyMembers.Length)];
                    Event sickness = new Sickness(personAffected);
                    player.HandleEvent(sickness);
                    break;
                case 3:
                    Event disaster = new Disaster();
                    player.HandleEvent(disaster);
                    break;
                case 4:
                    Event riverCrossing = new RiverCrossing();
                    player.HandleEvent(riverCrossing);
                    break;
            }
        }

        private void SaveGame()
        {
            try
            {
                // Serialize game state to JSON
                string json = JsonSerializer.Serialize(this);
                File.WriteAllText("savegame.json", json);
                Console.WriteLine("Game saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
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
