using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
namespace OregonTrailGame
{
    public class Game
    {
        private Player player;
        private List<Location> locations;
        private int currentLocationIndex;
        private Random random;
        private int turnsUntilNextTown;
        private ScoreManager scoreManager;
        private GameProgress gameProgress; // Add this field

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
                new River("South Platte River"),
                new Town("Green River"),
                new Town("Fort Bridger"),
                new Town("Soda Springs"),
                new Town("Fort Hall"),
                new River("Green River"),
                new River("Snake River"),
                new Town("Snake River"),
                new Town("Fort Boise"),
                new Town("Blue Mountains"),
                new Town("The Dalles"),
                new Town("Oregon City")
            };
            currentLocationIndex = 0;
            random = new Random();
            turnsUntilNextTown = random.Next(10, 21); // Random turns between 10 and 20
            scoreManager = new ScoreManager();
            gameProgress = new GameProgress(locations.Count); // Initialize GameProgress

            Start();
        }

        public void Start()
        {
            InitializeGame();
            RunGameLoop();
        }

        private void InitializeGame()
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"🤠🐂Welcome to The Oregon Trail!🐂🤠");

            // Initialize player
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            player.SetName(playerName); // Use SetName method to set the player's name

            // Add family members
            for (int i = 1; i <= 4; i++)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.Write($"🙂Enter the name of family member {i}: ");
                string familyMemberName = Console.ReadLine();
                player.AddFamilyMember(familyMemberName);
            }

            // Initialize player inventory with some starting items
            player.Inventory.AddFood(90);
            player.Inventory.AddAmmo(50);
        }

        private void RunGameLoop()
        {
            bool gameOver = false;

            while (!gameOver)
            {
                scoreManager.IncrementTurns(); // Increment the turn count

                // Consume food based on number of party members
                int foodConsumed = random.Next(1, 5) * player.Inventory.PartyCount; // Random amount per turn
                if (!player.Inventory.ConsumeFood(foodConsumed))
                {
                    Console.WriteLine($"You ran out of food! Game Over.☠️");
                    gameOver = true;
                    break;
                }

                turnsUntilNextTown--;

                if (turnsUntilNextTown <= 0)
                {
                    // Move to the next location
                    currentLocationIndex++;
                    if (currentLocationIndex >= locations.Count)
                    {
                        Console.WriteLine($"You have reached Oregon City! Congratulations, you win!🐂🤠");
                        gameOver = true;
                        break;
                    }
                    else
                    {
                        Location currentLocation = locations[currentLocationIndex];

                        // Visit the new location
                        currentLocation.Visit(player);

                        // Handle specific logic for towns
                        if (currentLocation is Town town)
                        {
                            player.Inventory.AddMoney(25); // Add $25 at each town
                            Console.WriteLine($"You have reached {town.Name}. You received $25.💲");
                        }

                        // Reset the turns until next town
                        turnsUntilNextTown = random.Next(5, 21); // Sets the number of moves between towns to between 5 and 20

                        // Update game progress after reaching a town
                        gameProgress.UpdateProgress(scoreManager.GetTurns()); // Update progress based on turn count

                    }
                }

                // Clear the console
                Console.Clear();

                // Update and display the game progress bar
                gameProgress.DisplayProgress(); // Display progress bar

                // Display the number of moves until the next town and towns left
                Console.WriteLine($"Moves until next town: {turnsUntilNextTown}🛖");
                Console.WriteLine($"Towns left to reach: {locations.Count - currentLocationIndex - 1}");

                // Display game menu
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine($"1. Continue🐂");
                Console.WriteLine($"2. Rest😴");
                Console.WriteLine($"3. Hunt🍖");
                Console.WriteLine($"4. Check supplies💼");
                Console.WriteLine($"5. Save and Quit🚩");

                int choice = GetPlayerChoice(1, 5);
                string outcomeMessage = "";

                switch (choice)
                {
                    case 1:
                        outcomeMessage = "You choose to continue.";
                        break;
                    case 2:
                        Console.OutputEncoding = System.Text.Encoding.UTF8;
                        Console.Write($"Enter number of turns to rest:💤 ");
                        if (int.TryParse(Console.ReadLine(), out int restTurns) && restTurns > 0)
                        {
                            player.Rest(restTurns);
                            outcomeMessage = "You rested and regained some health.";
                            scoreManager.IncrementTurns(); // Increment turns when resting
                        }
                        else
                        {
                            outcomeMessage = "Invalid number of turns. Please enter a positive number.";
                        }
                        break;
                    case 3:
                        Hunting hunting = new Hunting();
                        hunting.Hunt(player.Inventory);
                        outcomeMessage = "You went hunting.";
                        scoreManager.IncrementTurns(); // Increment turns when hunting
                        break;
                    case 4:
                        player.CheckSupplies();
                        outcomeMessage = "You checked your supplies.";
                        break;
                    case 5:
                        SaveGame();
                        Console.OutputEncoding = System.Text.Encoding.UTF8;
                        Console.WriteLine($"Game saved. Goodbye!👋");
                        gameOver = true;
                        continue; // Skip the rest of the loop to end the game
                }

                // Handle random event each turn, but after town logic
                if (choice != 5) // Only handle events if not saving
                {
                    EventManager.HandleRandomEvent(player);
                }

                // Display the outcome message and wait for Enter key
                Console.WriteLine(outcomeMessage);
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }

            EndGame(); // End the game and show score
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

        private void EndGame()
        {
            Console.WriteLine($"Game Over! Your score is: {scoreManager.GetTurns()} turns.");
        }
    }
}
