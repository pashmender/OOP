using OOP.Classes.Intrfaces.OmnivoreFood;
using OOP.Classes.Intrfaces.PredatorFood;

namespace OOP.Classes.Entities.OmnivorousUnits
{
    public class Pig : OmnivorousUnit <IPigFood,Pig>, IWolfFood, IBearFood, IHumanFood
    {

        public Pig(int x, int y) : base(x, y)
        { }

        protected override AbstractUnit CreateChild()
        {
            return new Pig(X, Y);
        }
    }
}