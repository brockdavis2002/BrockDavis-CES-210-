using System;

namespace OregonTrailGame
{
    class Sickness : Event
    {
        private string personAffected;

        // Constructor
        public Sickness(string person)
        {
            personAffected = person;
        }

        public override void Occur(Game game)
        {
            Console.WriteLine($"{personAffected} falls ill.");
            // Implement sickness logic here
        }
    }
}
