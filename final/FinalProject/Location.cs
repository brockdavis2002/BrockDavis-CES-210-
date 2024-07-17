using System;

namespace OregonTrailGame
{
    public abstract class Location
    {
        public string Name { get; private set; }

        public Location(string name)
        {
            Name = name;
        }

        public abstract void Visit(Player player);
    }
}
