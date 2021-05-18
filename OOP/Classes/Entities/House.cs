using OOP.Classes.Intrfaces;

namespace OOP.Classes.Entities
{
    public class House : GameObject, IHouse
    {
        public const int MaxStorage = 10;
        public int CurrStorage { get; private set; }

        public House(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool FillStorage()
        {
            if(CurrStorage < MaxStorage)
            {
                CurrStorage++;
            }

            return CurrStorage < MaxStorage;
        }

        public void TakeFromStorage()
        {
            CurrStorage--;
        }

        public bool CheckStorage()
        {
            return CurrStorage > 0;
        }
    }
}