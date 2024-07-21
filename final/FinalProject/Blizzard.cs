using System;
using System.Text;
namespace OregonTrailGame
{
    [Serializable]
    public class Blizzard : Disaster
    {
        public Blizzard(Player player) : base(player) { }

        protected override string GetDisasterName() => "Blizzard";

        protected override void HandleDisaster()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"A severe blizzard has struck.❄️");
            int lostTurns = random.Next(1, 3);
            int lostFood = random.Next(5, 11);
            player.Inventory.ConsumeFood(lostFood);
            Console.WriteLine($"You are stuck for {lostTurns}😥 Days due to the blizzard and {lostFood}🍗 units of food.");
        }
    }
}
