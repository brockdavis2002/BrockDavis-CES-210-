using System;
using System.IO;

namespace OregonTrailGame
{
    [Serializable]
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Game game;

            if (File.Exists("savegame.txt"))
            {
                Console.Clear();
                Console.WriteLine("A Previos Gameplay Was Found. View Stats? (yes/no)");
                string choice = Console.ReadLine().Trim().ToLower();

                if (choice == "yes" || choice == "y")
                {
                    game = new Game(loadGame: true);
                }
                else
                {
                    game = new Game(loadGame: false);
                }
            }
            else
            {
                game = new Game(loadGame: false);
            }

            game.Start();
        }
    }
}
