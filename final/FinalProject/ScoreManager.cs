using System;

namespace OregonTrailGame
{
    [Serializable]
    public class ScoreManager
    {
        private int turns;

        public ScoreManager()
        {
            turns = 0;
        }

        public void IncrementTurns()
        {
            turns++;
        }

        public void SetTurns(int newTurns)
        {
            if (newTurns >= 0) // Ensure turns are not negative
            {
                turns = newTurns;
            }
            else
            {
                throw new ArgumentException("Number of turns cannot be negative.");
            }
        }

        public int GetTurns()
        {
            return turns;
        }

        public int CalculateScore()
        {
            // The score is simply the number of turns taken
            return turns;
        }
    }
}
