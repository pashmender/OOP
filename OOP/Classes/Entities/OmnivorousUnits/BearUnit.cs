using OOP.Classes.Intrfaces.OmnivoreFood;

namespace OOP.Classes.Entities.OmnivorousUnits
{
    public class BearUnit : OmnivorousUnit<IBearFood,BearUnit>, IHumanFood
    {
        public BearUnit(int x, int y) : base( x, y)
        {
        }

        protected override AbstractUnit CreateChild()
        {
            return new BearUnit(X, Y);
        }

       
    }
}