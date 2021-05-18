using OOP.Classes.Entities;
using OOP.Classes.Intrfaces.HerbivoreFood;
using OOP.Classes.Intrfaces.PredatorFood;

namespace OOP.Classes.Units.HerbivorousUnits
{
    public class Rat : HerbivorousUnit<IRatFood,Rat>, IFoxFood 
    {
        public Rat(int x, int y) : base(x, y)
        {
        }

        protected override AbstractUnit CreateChild()
        {
            return new Rat(X, Y);
        }
    }
}