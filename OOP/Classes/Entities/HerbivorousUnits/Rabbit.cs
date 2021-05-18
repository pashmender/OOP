using OOP.Classes.Entities;
using OOP.Classes.Intrfaces.OmnivoreFood;
using OOP.Classes.Intrfaces.PredatorFood;
using OOP.Classes.Intrfaces.HerbivoreFood;

namespace OOP.Classes.Units.HerbivorousUnits
{
    public class Rabbit : HerbivorousUnit<IRabbitFood, Rabbit>, IBearFood, IWolfFood, IFoxFood
    {
        public Rabbit(int x, int y) : base(x, y)
        {
        }

        protected override AbstractUnit CreateChild()
        {
            return new Rabbit(X, Y);
        }
    }
}