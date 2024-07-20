using System;
using System.Text;
using System;

namespace OregonTrailGame
{
    [Serializable]
    public class GameProgress
    {
        private int totalTowns;
        private int currentProgress;
        private int currentTurn;
        private const int TotalSpaces = 20; // Total spaces in the progress bar

        public GameProgress(int totalTowns)
        {
            this.totalTowns = totalTowns;
            this.currentProgress = 0;
            this.currentTurn = 0;
        }

        public void UpdateProgress(int currentTurn)
        {
            this.currentTurn = currentTurn;
            int maxTurns = totalTowns * 10; // Adjust multiplier for progress scale
            currentProgress = (int)((double)currentTurn / maxTurns * TotalSpaces);
        }

        public void DisplayProgress()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("\xfeff"); // bom = byte order mark
            Console.Write("Progress: ");
            for (int i = 0; i < TotalSpaces; i++)
            {
                if (i < currentProgress)
                {
                    Console.Write("▓");
                }
                else
                {
                    Console.Write("░");
                }
            }
            Console.WriteLine($"Towns Traveled🐂");
        }
    }
}
