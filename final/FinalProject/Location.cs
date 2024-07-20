using System;

namespace OregonTrailGame
{
    [Serializable]
    public abstract class Location
    {
        public string Name { get; private set; }

        protected Location(string name)
        {
            Name = name;
        }

        public abstract void Visit(Player player);
    }
}
