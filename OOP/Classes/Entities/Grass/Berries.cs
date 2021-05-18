using OOP.Classes.Intrfaces.HerbivoreFood;
using OOP.Classes.Intrfaces.OmnivoreFood;

namespace OOP.Classes.Units.Grass
{
    public class Berries : Entities.Grass.Grass, IBearFood, IHedgehogFood, IHumanFood, IDeerFood
    {
        public Berries(int x, int y) : base(x, y)
        {
        }
    }
}