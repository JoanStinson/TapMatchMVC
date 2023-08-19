namespace JGM.Game
{
    public class Coordinate
    {
        public int x => m_x;
        public int y => m_y;
        public bool isVisited { get; set; }

        private readonly int m_x = -1;
        private readonly int m_y = -1;

        public Coordinate(int x, int y)
        {
            m_x = x;
            m_y = y;
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
