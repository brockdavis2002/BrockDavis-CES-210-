using System;
using System.Text;
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
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"You have reached the river: {river.Name}💦");
            river.Visit(player);
        }
    }
}
