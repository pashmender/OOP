using OOP.Classes.Entities;
using OOP.Classes.Intrfaces.PredatorFood;

namespace OOP.Classes.Units.PredatorUnits
{
    public class Wolf : PredatorUnit<IWolfFood, Wolf>
    {
        public Wolf(int x, int y) : base (x, y) {}

        protected override AbstractUnit CreateChild()
        {
            return new Wolf(X, Y);
        }
    }
}