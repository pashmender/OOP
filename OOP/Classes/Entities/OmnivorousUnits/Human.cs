using System;
using System.Collections.Generic;
using System.Linq;
using OOP.Classes.Intrfaces;
using OOP.Classes.Intrfaces.OmnivoreFood;
using OOP.Classes.Intrfaces.PredatorFood;
using OOP.Classes.Units.Grass;


namespace OOP.Classes.Entities.OmnivorousUnits
{
    public class Human: OmnivorousUnit<IHumanFood, Human>, IBearFood, IWolfFood
    {

        protected internal House _building;
        

        private Grass.Grass _foundedBerries;
        private bool _gotFood;
        public Human(int x, int y) : base(x, y) {}
        protected override AbstractUnit CreateChild()
        {
            return new Human(X, Y);
        }

        public override void Live()
        {
            if (Satiety <= 0 || Dead)
            {
                Dead = true;
            }
            else if (GlobalMap.CheckTime())
            {
                CurrentBirthCooldown--;
                Sleep();
            }
            else if (Satiety <= SatTreshold)
            {
                CurrentBirthCooldown--;

                if (MyHouse != null) 
                    if(MyHouse.CheckStorage())
                            EatFromStorage();
                else
                {
                    FindFood();
                    Eat();
                }

            }
            else if (Gender == GameObjectType.Female && MyHouse != null)
            {
                if(!MyHouse.CheckStorage())
                    Gathering();
            }
            else if (FoundPartner == null)
            {
                RandomMove();
                CurrentBirthCooldown--;
                if (CurrentBirthCooldown < BirthCooldown)
                    FindPartner();
            }
            else if (FoundPartner != null && CurrentBirthCooldown < BirthCooldown)
            {
                if (MyHouse == null)
                {    
                    if(Gender == GameObjectType.Male)
                        LocateBuilding();
                    MoveTo(DateX, DateY);
                }
                else
                {
                    MoveTo(MyHouse.X,MyHouse.Y);
                }
                
                CreateFamily();
            }
            else StayHome();
        }

        private void CreateFamily()
        {
            if (MyHouse == null)
            {
                if (_building == null){
                    BuildHouse();
                    return;
                }

                MoveTo(_building.X,_building.Y);
                if (NearToBuilding(_building))
                {
                    BuildHouse();
                    ProduceNewUnit();
                }
                
            }
            else
            {
                MoveTo(MyHouse.X, MyHouse.Y);
                ProduceNewUnit();
            }
        }
        
        protected override void ProduceNewUnit()
        {
            if ((MyHouse.X != X || MyHouse.Y != Y) &&
                (X != FoundPartner.X || Y != FoundPartner.Y)) return;

            Stress = 0;
            FoundPartner.CurrentBirthCooldown = 7;
            CurrentBirthCooldown = 7;
            
            AddUnitToMap();
        }

        private void LocateBuilding()
        {
            if (_building != null || MyHouse != null) return;
            
            List<IHouse> buildings = LocateTarget<IHouse>();
            _building = buildings.OfType<House>().FirstOrDefault();
            if (_building != null)
            {
                DateX = _building.X;
                DateY = _building.Y;
                FoundPartner.DateX = DateX;
                FoundPartner.DateY = DateY;
            }
            else
            {
                BuildHouse();
            }

        }
        
        private void BuildHouse()
        {
            if (GlobalMap.Field[X, Y].Entity.OfType<IHouse>().FirstOrDefault() == null)
            {
                MyHouse = new House(X, Y);
                FoundPartner.MyHouse = MyHouse;
                GlobalMap.Field[X, Y].Entity.Add(MyHouse);
                return;
            }
            
            RandomMove();
            BuildHouse();
        }

        private bool NearToBuilding(House h)
        {
            return Math.Abs(h.X - X) + Math.Abs(h.Y - Y) < 5;
            
        }

        private void Gathering()
        {
            if (_foundedBerries == null)
            {
                List<IHumanFood> potentialFood = LocateTarget<IHumanFood>();
                _foundedBerries = potentialFood.OfType<Berries>().FirstOrDefault();
            }
            else if (!_gotFood)
            {
                MoveTo(_foundedBerries.X , _foundedBerries.Y);
                if (_foundedBerries.X != X || _foundedBerries.X != Y) return;
                _gotFood = true;
                GlobalMap.Field[X, Y].Entity.Remove(_foundedBerries);
                GlobalMap.deadEntityList.Add(_foundedBerries);
            }
            else
            {
                MoveTo(MyHouse.X, MyHouse.Y);
                if (MyHouse.X != X || MyHouse.Y != Y) return;

                if (!MyHouse.FillStorage())
                {
                 EatFromStorage();
                 MyHouse.FillStorage();
                }
                _foundedBerries = null;
                _gotFood = false;
            }
            
            
        }

        private void EatFromStorage()
        {
            MyHouse.TakeFromStorage();
            Satiety++;
        }

        private void StayHome()
        {
            Satiety -= SatPerTurn;
        }
    }
}