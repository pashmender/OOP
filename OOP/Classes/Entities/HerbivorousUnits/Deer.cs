using OOP.Classes.Entities;
using OOP.Classes.Intrfaces.HerbivoreFood;
using OOP.Classes.Intrfaces.OmnivoreFood;
using OOP.Classes.Intrfaces.PredatorFood;

namespace OOP.Classes.Units.HerbivorousUnits
{
    public class Deer : HerbivorousUnit<IDeerFood,Deer>, IBearFood, IWolfFood
    {
        public Deer(int x, int y) : base(x, y){}
        protected override AbstractUnit CreateChild()
        {
            return new Deer(X, Y);
        }
    }
}