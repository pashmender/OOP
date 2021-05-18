using System.Collections.Generic;

namespace OOP.Classes
{

    public class Cell
    {
        
        public List<GameObject> Entity { get; set; }

       

        public int X { get; private set; }
        public int Y { get; private set; }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
