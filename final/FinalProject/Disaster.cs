using System;

namespace OregonTrailGame
{
    class Disaster : Event
    {
        public override void Occur(Game game)
        {
            Console.WriteLine("A disaster occurs!");
            // Implement disaster logic here
        }
    }
}
