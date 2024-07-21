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
                Console.WriteLine("Press Enter to return to the main menu.");
                Console.ReadLine();
            }
            else
            {
                InitializeNewGame();
            }
        }

        private void InitializeNewGame()
        {
            player = new Player();
            InitializeLocations();
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
                        UpdateBestScore(scoreManager.GetTurns());
                        EndGame();
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
                Console.WriteLine($"1. Continue🐂");
                Console.WriteLine($"2. Rest😴");
                Console.WriteLine($"3. Hunt🍖");
                Console.WriteLine($"4. Check supplies💼");
                Console.WriteLine($"5. Quit🚩");

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
                var saveData = new StringBuilder();
                saveData.AppendLine($"PlayerName:{player.Name}");
                saveData.AppendLine($"Money:{player.Inventory.GetMoney()}");
                saveData.AppendLine($"Food:{player.Inventory.GetFood()}");
                saveData.AppendLine($"Ammo:{player.Inventory.GetAmmo()}");
                saveData.AppendLine($"CurrentLocationIndex:{currentLocationIndex}");
                saveData.AppendLine($"TurnsUntilNextTown:{turnsUntilNextTown}");
                saveData.AppendLine($"Score:{scoreManager.GetTurns()}"); // Save the score
                
                // Save party count and names
                saveData.AppendLine($"PartyCount:{player.Inventory.PartyCount}");
                saveData.AppendLine($"PartyMembers:{string.Join(",", player.Inventory.PartyMembers)}");

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
                Console.WriteLine("Displaying previous game record...");

                if (!File.Exists("savegame.txt"))
                {
                    Console.WriteLine("Save file not found.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine(); // Waits for the user to press Enter
                    InitializeNewGame();
                }

                string[] lines = File.ReadAllLines("savegame.txt");

                Console.WriteLine($"Read {lines.Length} lines from save file.");

                if (lines.Length < 7) // Updated to check for 7 lines
                {
                    Console.WriteLine("Save file is corrupted or incomplete.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine(); // Waits for the user to press Enter
                    InitializeNewGame();
                }

                // Parse player data
                Console.WriteLine($"Player Name🤠: {GetValueFromLine(lines[0])}");
                Console.WriteLine($"Money💲: ${GetValueFromLine(lines[1])}");
                Console.WriteLine($"Food🍖: {GetValueFromLine(lines[2])} units");
                Console.WriteLine($"Ammo🔫: {GetValueFromLine(lines[3])} units");

                // Parse game state
                currentLocationIndex = int.Parse(GetValueFromLine(lines[4]));
                turnsUntilNextTown = int.Parse(GetValueFromLine(lines[5]));
                int score = int.Parse(GetValueFromLine(lines[6])); // Read the score

                Console.WriteLine($"Current Location Index🛖: {currentLocationIndex}");
                Console.WriteLine($"Turns Until Next Town🛖: {turnsUntilNextTown}");
                Console.WriteLine($"Score💯: {score}");

                // Output party information
                int partyCount = int.Parse(GetValueFromLine(lines[7]));
                Console.WriteLine($"Party Count👨‍👩‍👧‍👦: {partyCount}");
                Console.WriteLine($"Party Members🤠: {GetValueFromLine(lines[8])}");

                Console.WriteLine("Previous game record displayed successfully.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine(); // Waits for the user to press Enter
                InitializeNewGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying game record: {ex.Message}");
                InitializeNewGame();
            }
        }

        private void InitializeLocations()
        {
            // Initialize locations with default values
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

        public void EndGame()
        {
            SaveGame();
            Console.WriteLine($"Your score is: {scoreManager.GetTurns()} turns.");
            Console.WriteLine("Personal Best: ");
            if (File.Exists("bestscore.txt"))
            {
                string bestScoreData = File.ReadAllText("bestscore.txt");
                Console.WriteLine(bestScoreData);
                Console.WriteLine("Press Enter to Exit...");
                Console.ReadLine(); // Waits for the user to press Enter
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("No personal best score recorded yet.");
                Console.WriteLine("Press Enter to Exit...");
                Console.ReadLine(); // Waits for the user to press Enter
                Environment.Exit(0);
            }
            
        }

        private void UpdateBestScore(int currentScore)
        {
            try
            {
                if (!File.Exists("bestscore.txt"))
                {
                    File.WriteAllText("bestscore.txt", $"Personal Best Score: {currentScore} turns");
                }
                else
                {
                    string bestScoreData = File.ReadAllText("bestscore.txt");
                    int bestScore = int.Parse(bestScoreData.Split(':')[1].Trim().Split(' ')[0]);
                    if (currentScore < bestScore)
                    {
                        File.WriteAllText("bestscore.txt", $"Personal Best Score: {currentScore} turns");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating best score: {ex.Message}");
            }
        }

        private string GetValueFromLine(string line)
        {
            return line.Split(':')[1];
        }
    }
}
