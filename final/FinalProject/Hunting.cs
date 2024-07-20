using System;
using System.Text;

namespace OregonTrailGame
{
    [Serializable]
    class Hunting
    {
        private Random random;

        public Hunting()
        {
            random = new Random();
        }

        public bool Hunt(Inventory inventory)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine($"You hunt for food.🦬");

            int huntOutcome = random.Next(1, 11); // Random number between 1 and 10

            if (inventory.ConsumeAmmo(1))
            {
                if (huntOutcome <= 7) // 70% chance of success
                {
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    int foodFound = random.Next(10, 31); // Random amount between 10 and 30
                    inventory.AddFood(foodFound);
                    Console.WriteLine($"You found {foodFound} units of food.🍖");
                    return true;
                }
                else
                {
                    Console.WriteLine($"You couldn't find any food this time.🍴");
                    return false;
                }
            }
            else
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine($"You are out of ammo!😭");
                return false;
            }
        }
    }
}
