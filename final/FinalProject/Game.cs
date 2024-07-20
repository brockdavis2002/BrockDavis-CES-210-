using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OregonTrailGame
{
    [Serializable]
    public class Game
    {
        private Player player;
        private List<Location> locations;
        private int currentLocationIndex;
        private Random random;
        private int turnsUntilNextTown;
        private ScoreManager scoreManager;
        private GameProgress gameProgress;

        public Game(bool loadGame)
        {
            if (loadGame && File.Exists("savegame.txt"))
            {
                LoadGame();
            }
            else
            {
                InitializeNewGame();
            }
        }

        private void InitializeNewGame()
        {
            player = new Player();
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
            gameProgress = new GameProgress(locations.Count);

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
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"🤠🐂Welcome to The Oregon Trail!🐂🤠");

            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            player.SetName(playerName);

            for (int i = 1; i <= 4; i++)
            {
                Console.Write($"🙂Enter the name of family member {i}: ");
                string familyMemberName = Console.ReadLine();
                player.AddFamilyMember(familyMemberName);
            }

            player.Inventory.AddFood(90);
            player.Inventory.AddAmmo(50);
        }

        private void RunGameLoop()
        {
            bool gameOver = false;

            while (!gameOver)
            {
                scoreManager.IncrementTurns();

                int foodConsumed = random.Next(1, 5) * player.Inventory.PartyCount;
                if (!player.Inventory.ConsumeFood(foodConsumed))
                {
                    Console.WriteLine($"You ran out of food! Game Over.☠️");
                    gameOver = true;
                    break;
                }

                turnsUntilNextTown--;

                if (turnsUntilNextTown <= 0)
                {
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
                        currentLocation.Visit(player);

                        if (currentLocation is Town town)
                        {
                            player.Inventory.AddMoney(25);
                            Console.WriteLine($"You have reached {town.Name}. You received $25.💲");
                        }

                        turnsUntilNextTown = random.Next(5, 21);
                        gameProgress.UpdateProgress(scoreManager.GetTurns());
                    }
                }

                Console.Clear();
                gameProgress.DisplayProgress();
                Console.WriteLine($"Moves until next town: {turnsUntilNextTown}🛖");
                Console.WriteLine($"Towns left to reach: {locations.Count - currentLocationIndex - 1}");
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($"1. Continue🐂");
                Console.WriteLine($"2. Rest😴");
                Console.WriteLine($"3. Hunt🍖");
                Console.WriteLine($"4. Check supplies💼");
                Console.WriteLine($"5. Save and Quit🚩");

                int choice = GetPlayerChoice(1, 5);
                Console.WriteLine("");
                Console.WriteLine("");
                string outcomeMessage = "";

                switch (choice)
                {
                    case 1:
                        outcomeMessage = "You choose to continue.";
                        break;
                    case 2:
                        Console.Write($"Enter number of turns to rest:💤 ");
                        if (int.TryParse(Console.ReadLine(), out int restTurns) && restTurns > 0)
                        {
                            player.Rest(restTurns);
                            outcomeMessage = "You rested and regained some health.";
                            scoreManager.IncrementTurns();
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
                        scoreManager.IncrementTurns();
                        break;
                    case 4:
                        player.CheckSupplies();
                        outcomeMessage = "You checked your supplies.";
                        break;
                    case 5:
                        SaveGame();
                        Console.WriteLine($"Game saved. Goodbye!👋");
                        Environment.Exit(0); // Terminate the program
                        break;
                }

                if (choice != 5)
                {
                    EventManager.HandleRandomEvent(player);
                }

                Console.WriteLine(outcomeMessage);
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }

            EndGame();
        }

        private void SaveGame()
        {
            try
            {
                // Prepare save data
                var saveData = new StringBuilder();
                saveData.AppendLine($"PlayerName:{player.Name}");
                saveData.AppendLine($"Money:{player.Inventory.GetMoney()}");
                saveData.AppendLine($"Food:{player.Inventory.GetFood()}");
                saveData.AppendLine($"Ammo:{player.Inventory.GetAmmo()}");
                saveData.AppendLine($"CurrentLocationIndex:{currentLocationIndex}");
                saveData.AppendLine($"TurnsUntilNextTown:{turnsUntilNextTown}");

                // Save locations
                saveData.AppendLine($"LocationsCount:{locations.Count}");
                foreach (var location in locations)
                {
                    saveData.AppendLine(location.ToString()); // Ensure Location has a ToString method
                }

                // Save ScoreManager and GameProgress data
                saveData.AppendLine($"ScoreManagerTurns:{scoreManager.GetTurns()}");
                saveData.AppendLine($"GameProgress:{gameProgress.ToString()}"); // Ensure GameProgress has a ToString method

                File.WriteAllText("savegame.txt", saveData.ToString());
                Console.WriteLine("Game saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        private void LoadGame()
        {
            try
            {
                string[] lines = File.ReadAllLines("savegame.txt");

                // Parse player data
                player = new Player();
                player.SetName(GetValueFromLine(lines[0]));
                player.Inventory.AddMoney(int.Parse(GetValueFromLine(lines[1])));
                player.Inventory.AddFood(int.Parse(GetValueFromLine(lines[2])));
                player.Inventory.AddAmmo(int.Parse(GetValueFromLine(lines[3])));

                // Load locations
                currentLocationIndex = int.Parse(GetValueFromLine(lines[4]));
                turnsUntilNextTown = int.Parse(GetValueFromLine(lines[5]));

                int locationsCount = int.Parse(GetValueFromLine(lines[6]));
                locations = new List<Location>();

                for (int i = 0; i < locationsCount; i++)
                {
                    string locationData = lines[7 + i];
                    // Parse and create Location objects based on stored data
                    locations.Add(ParseLocation(locationData)); // Ensure you have a method to parse location
                }

                // Load ScoreManager and GameProgress
                scoreManager = new ScoreManager();
                scoreManager.SetTurns(int.Parse(GetValueFromLine(lines[7 + locationsCount])));

                gameProgress = new GameProgress(locationsCount);
                // Parse and set game progress if needed

                
                Console.WriteLine("Game loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
                InitializeNewGame();
            }
        }
        //used for lading location
        private Location ParseLocation(string data)
        {
            // Example parsing logic
            if (data.StartsWith("Town:"))
            {
                return new Town(data.Substring(5)); // Creates a Town object with the name
            }
            else if (data.StartsWith("River:"))
            {
                return new River(data.Substring(6)); // Creates a River object with the name
            }
            // Add more location types if needed
            throw new InvalidOperationException("Unknown location type.");
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

        private string GetValueFromLine(string line)
        {
            return line.Split(':')[1];
        }

    }
}
