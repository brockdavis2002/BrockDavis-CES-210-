using System;

namespace OregonTrailGame
{
    public class RiverCrossing : Event
    {
        private River river;

        public RiverCrossing(string name)
        {
            river = new River(name);
        }

        public override void Occur(Player player)
        {
            Console.WriteLine($"You have reached the river: {river.Name}");
            river.Visit(player);
        }
    }
}
