using System.Collections.Generic;
using System.Linq;

namespace OOP.Classes.Entities
{
    public interface IFood
    {
        
    }
    public abstract class Unit<TFood, TPartner> : AbstractUnit 
        where TFood : IFood 
        where TPartner: AbstractUnit
    {
        protected Unit(int x, int y) : base (x, y)
        {
            Satiety = Map.Rand.Next(MinSat, MaxSat);
            Stress = 0;
            Dead = false;

            FoundPartner = null;
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
        
                FindFood();

                Eat();
            }
            else if (FoundPartner == null)
            {
                RandomMove();
                CurrentBirthCooldown--;
                if (CurrentBirthCooldown < BirthCooldown)
                    FindPartner();
            }
            else if (FoundPartner != null)
            {
                MoveTo(DateX, DateY);
                ProduceNewUnit();
            }
        }

        protected override void FindFood()
        {
            
            if (!_foodWasLocated)
            {
                List<TFood> potentialFood = LocateFood();
                foreach (var food in potentialFood.OfType<AbstractUnit>())
                {
                    _foodWasLocated = true;
                    SetTarget(food);
                    FoundMeat = true;
                }

                foreach (var food in potentialFood.OfType<Grass.Grass>())
                {
                    _foodWasLocated = true;
                    FoundGrass = true;
                    
                    FindFoodX = food.X;
                    FindFoodY = food.Y;
                }
            }

            
            if(FoundGrass)
                MoveTo(FindFoodX, FindFoodY);
            else if(FoundMeat && Target == null)
            {
                FoundMeat = false;
                _foodWasLocated = false;
            }
            else if (FoundMeat && Target != null)
            {
                MoveTo(Target.X, Target.Y);
            }
            else    
                RandomMove();
        }

        protected List<TFood> LocateFood()
        {
           return LocateTarget<TFood>();
        }
        protected override void FindPartner()
        {
            List<TPartner> potentialPartners = LocateTarget<TPartner>();

            foreach (var partner in potentialPartners)
            {
                if (GetType() != partner.GetType()) continue;
                CheckPotentialPartner(partner);
            }
        }

        protected override void Eat()
        {
            if (!_foodWasLocated)
                return;
           
                
            if (FoundGrass && FindFoodX == X && FindFoodY == Y)
            {
                EatGrass();
            }
            else if (FoundMeat )
            {
                if (Target == null)
                {
                    FoundMeat = _foodWasLocated = false;
                    return;
                }
                if(Target.X == X && Target.Y == Y)
                    KillUnit();
            }
        }
        
        private void EatGrass()
        {
            Grass.Grass grassOnCell = GlobalMap.Field[X, Y].Entity.OfType<Grass.Grass>().FirstOrDefault();
            GlobalMap.Field[X, Y].Entity.Remove(grassOnCell);
            GlobalMap.deadEntityList.Add(grassOnCell);
            Satiety = 100;

            Stress /= 2;
            if (Stress < 100)
                Target = null;

            _foodWasLocated = false;
            FoundGrass = false;
            FindFoodX = -1; FindFoodY = -1;
        }

        protected List<TTargetToSearch> LocateTarget<TTargetToSearch>()
        {
            int[] wayX = { -1, 0, 1, 0 };
            int[] wayY = { 0, 1, 0, -1 };
            int radius = 8;
            int[] anotherWay = { -1, 1 };
            
            List<TTargetToSearch> list = new List<TTargetToSearch>();
            for (int i = 0; i < wayX.Length * radius; i++)
            {
                int k = i / wayX.Length + 1;
                int moveRight = wayY[i % 4] * k;
                int moveForward = wayX[i % 4] * k;


                if (i % 2 == 1)
                {
                    for (int j = 0; j < anotherWay.Length * radius; j++)
                    {
                        int c = j / anotherWay.Length + 1;

                        moveForward = anotherWay[j % 2] * c;

                        if (GlobalMap.CheckBorder(X + moveForward, Y + moveRight))
                        {
                            if (GlobalMap.Field[X + moveForward, Y + moveRight].Entity.OfType<TTargetToSearch>().FirstOrDefault() != null)
                                foreach (var target in GlobalMap.Field[X + moveForward, Y + moveRight].Entity.OfType<TTargetToSearch>())
                                {
                                    list.Add(target);
                                }
                                
                        }
                    }
                }
                else if (GlobalMap.CheckBorder(X + moveForward, Y + moveRight))
                {
                    if (GlobalMap.Field[X + moveForward, Y + moveRight].Entity.OfType<TTargetToSearch>().FirstOrDefault() != null)
                        foreach (var target in GlobalMap.Field[X + moveForward, Y + moveRight].Entity.OfType<TTargetToSearch>())
                        {
                            list.Add(target);
                        }
                }
            }
            
            
            return list;
        }
        
    }
}
