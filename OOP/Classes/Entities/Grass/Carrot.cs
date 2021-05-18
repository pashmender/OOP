using OOP.Classes.Intrfaces.HerbivoreFood;
using OOP.Classes.Intrfaces.OmnivoreFood;

namespace OOP.Classes.Units.Grass
{
    public class Carrot : Entities.Grass.Grass, IPigFood, IRabbitFood, IRatFood, IHumanFood
    {
        public Carrot(int x, int y) : base(x, y)
        {
        }
    }
}