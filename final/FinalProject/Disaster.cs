using System;

namespace OregonTrailGame
{
    public abstract class Disaster : Event
    {
        protected Player player;
        protected Random random;

        public Disaster(Player player)
        {
            this.player = player;
            this.random = new Random();
        }

        public override void Occur(Player player)
        {
            Console.WriteLine($"A disaster has struck: {GetDisasterName()}");
            HandleDisaster();
        }

        protected abstract string GetDisasterName();
        protected abstract void HandleDisaster();

        public static Disaster GenerateRandomDisaster(Player player)
        {
            var random = new Random();
            int disasterType = random.Next(1, 10); // Adjust the range based on the number of disaster types

            switch (disasterType)
            {
                case 1:
                    return new Flooding(player);
                case 2:
                    return new FlashFlooding(player);
                case 3:
                    return new Blizzard(player);
                case 4:
                    return new BugInfestation(player);
                case 5:
                    return new Tornado(player);
                case 6:
                    return new Stampede(player);
                case 7:
                    return new AnimalAttack(player);
                case 8:
                    return new Fire(player);
                case 9:
                    return new NativeAmericanAttack(player);
                default:
                    throw new InvalidOperationException("Unknown disaster type");
            }
        }

        protected int GetPlayerChoice(int min, int max)
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
