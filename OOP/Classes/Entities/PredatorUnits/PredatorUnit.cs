using OOP.Classes.Entities;
using OOP.Classes.Intrfaces.HerbivoreFood;

namespace OOP.Classes.Units.PredatorUnits
{
    public class PredatorUnit<TFood, TPartner> : Unit<TFood, TPartner> 
    where TFood : IFood
    where TPartner : AbstractUnit 
    {
        protected override AbstractUnit CreateChild()
        {
            return new PredatorUnit<TFood, TPartner>(X, Y);
        }
        
        public PredatorUnit(int x, int y) : base(x, y)
        {
        }
        
    }

}
