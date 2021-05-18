using System.ComponentModel.Design;
using OOP.Classes.Intrfaces.HerbivoreFood;
using OOP.Classes.Intrfaces.OmnivoreFood;

namespace OOP.Classes.Units.Grass
{
    public class Trava : Entities.Grass.Grass, IDeerFood, IRabbitFood, IPigFood
    {
        public Trava(int x, int y) : base(x, y)
        {
            
        } 
    }
}