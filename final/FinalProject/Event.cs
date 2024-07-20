using System;

namespace OregonTrailGame
{
    [Serializable]
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
            int randomEvent = random.Next(1, 10); // Range from 1 to 5

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
                case 6:
                    //blank so that no event happens
                    break;
                case 7:
                    gameEvent = Disaster.GenerateRandomDisaster(player); // Generate and handle a random disaster
                    break;
                case 8:
                    gameEvent = new Encounter(); // Make sure Encounter inherits Event
                    break;
                case 9:
                    //blank so that no event happens
                    break;       
            }

            if (gameEvent != null)
            {
                gameEvent.Occur(player);
            }
        }
    }
}
