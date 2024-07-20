using System;

namespace OregonTrailGame
{
    public abstract class Event
    {
        public abstract void Occur(Player player);
    }

    public static class EventManager
    {
        private static Random random = new Random();

        public static void HandleRandomEvent(Player player)
        {
            // Simulate a random event each turn
            int randomEvent = random.Next(1, 6); // Range from 1 to 5

            Event gameEvent = null;
            switch (randomEvent)
            {
                case 1:
                    gameEvent = new Encounter(); // Make sure Encounter inherits Event
                    break;
                case 2:
                    gameEvent = new Sickness(player); // Make sure Sickness inherits Event
                    break;
                case 3:
                    gameEvent = Disaster.GenerateRandomDisaster(player); // Generate and handle a random disaster
                    break;
                case 4:
                    gameEvent = new RiverCrossing("Random River"); // Make sure RiverCrossing inherits Event
                    break;
                case 5:
                    Console.WriteLine("Nothing Happens");
                    break;
            }

            if (gameEvent != null)
            {
                gameEvent.Occur(player);
            }
        }
    }
}
