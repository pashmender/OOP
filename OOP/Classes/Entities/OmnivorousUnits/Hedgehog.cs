using OOP.Classes.Intrfaces.OmnivoreFood;

namespace OOP.Classes.Entities.OmnivorousUnits
{
    public class Hedgehog : OmnivorousUnit<IHedgehogFood, Hedgehog>
    {
        public Hedgehog(int x, int y) : base(x, y)
        {
            
        }

        protected override AbstractUnit CreateChild()
        {
            return new Hedgehog(X,Y);
        }
    }
}