using OOP.Classes.Entities;
using OOP.Classes.Intrfaces.HerbivoreFood;
using OOP.Classes.Intrfaces.OmnivoreFood;
using OOP.Classes.Intrfaces.PredatorFood;

namespace OOP.Classes.Units.HerbivorousUnits
{
    public class HerbivorousUnit<TFood, TPartner> : Unit<TFood, TPartner> , IPredatorFood, IOmnivoreFood
        where TFood : IFood
        where TPartner : AbstractUnit
    {
        public HerbivorousUnit(int x, int y) : base(x, y)
        {

        }

        protected override AbstractUnit CreateChild()
        {
            return new HerbivorousUnit<TFood, TPartner>(X,Y);
        }
    }
}

