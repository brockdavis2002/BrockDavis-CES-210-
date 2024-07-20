using System;
using System.Text;
namespace OregonTrailGame
{
    [Serializable]
    public class AnimalAttack : Disaster
    {
        public AnimalAttack(Player player) : base(player) { }

        protected override string GetDisasterName() => "Animal Attack";

        protected override void HandleDisaster()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"Your camp is attacked by wild animals.🐖");
            int lostFood = random.Next(5, 16);
            int lostAmmo = random.Next(3, 11);
            player.Inventory.ConsumeFood(lostFood);
            player.Inventory.ConsumeAmmo(lostAmmo);
            Console.WriteLine($"You lose {lostFood}🍗 units of food and {lostAmmo}🔫 units of ammo.");
        }
    }
}
