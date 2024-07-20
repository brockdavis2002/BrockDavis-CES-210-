using System;

namespace OregonTrailGame
{
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
