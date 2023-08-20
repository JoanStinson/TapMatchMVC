namespace JGM.Game
{
    public class Coordinate
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public bool visited { get; set; }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinate other)
            {
                return x == other.x && y == other.y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }
    }
}
