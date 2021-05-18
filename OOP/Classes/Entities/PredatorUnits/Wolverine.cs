using OOP.Classes.Entities;
using OOP.Classes.Intrfaces.PredatorFood;

namespace OOP.Classes.Units.PredatorUnits
{
    public class Wolverine : PredatorUnit<IWolverineFood, Wolverine>
    {
        public Wolverine(int x, int y) : base(x, y)
        {
        }

        protected override AbstractUnit CreateChild()
        {
            return new Wolverine(X, Y);
        }
    }
}