using OOP.Classes.Entities;
using OOP.Classes.Intrfaces.PredatorFood;

namespace OOP.Classes.Units.PredatorUnits
{
    public class Fox : PredatorUnit<IFoxFood,Fox>
    {
        public Fox(int x, int y) : base(x, y)
        {
        }
        protected override AbstractUnit CreateChild()
        {
            return new Fox(X, Y);
        }
    }
}