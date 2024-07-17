using System;

namespace OregonTrailGame
{
    class Encounter : Event
    {
        public override void Occur(Game game)
        {
            Console.WriteLine("You encounter something.");
            // Implement encounter logic here
        }
    }
}
