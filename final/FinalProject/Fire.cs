using System;
using System.Text;
namespace OregonTrailGame
{
    public class Fire : Disaster
    {
        public Fire(Player player) : base(player) { }

        protected override string GetDisasterName() => "Fire";

        protected override void HandleDisaster()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"A fire has broken out.🔥");
            int lostFood = random.Next(10, 21);
            int lostAmmo = random.Next(5, 11);
            player.Inventory.ConsumeFood(lostFood);
            player.Inventory.ConsumeAmmo(lostAmmo);
            Console.WriteLine($"You lose {lostFood}🍖 units of food and {lostAmmo}🔫 units of ammo.");
        }
    }
}
