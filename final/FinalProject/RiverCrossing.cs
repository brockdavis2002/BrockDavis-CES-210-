using System;

namespace OregonTrailGame
{
    class RiverCrossing : Event
    {
        public override void Occur(Game game)
        {
            Console.WriteLine("You encounter a river crossing.");
        }
    }
}
