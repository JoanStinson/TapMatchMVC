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
    }
}