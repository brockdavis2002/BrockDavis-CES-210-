using System;
using System.Text;
namespace OregonTrailGame
{
    [Serializable]
    public class FlashFlooding : Disaster
    {
        public FlashFlooding(Player player) : base(player) { }

        protected override string GetDisasterName() => "Flash Flooding";

        protected override void HandleDisaster()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"A sudden flash flood has damaged your supplies.🌊");
            int lostFood = random.Next(10, 21);
            int lostAmmo = random.Next(5, 11);
            player.Inventory.ConsumeFood(lostFood);
            player.Inventory.ConsumeAmmo(lostAmmo);
            Console.WriteLine($"You lost {lostFood}🍖 units of food and {lostAmmo}🔫 units of ammo.");
        }
    }
}
