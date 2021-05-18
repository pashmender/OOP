using OOP.Classes.Intrfaces.HerbivoreFood;
using OOP.Classes.Intrfaces.OmnivoreFood;

namespace OOP.Classes.Entities.Grass
{
    public class Grass : GameObject, IHerbivoreFood, IOmnivoreFood
    {
        public Grass(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
