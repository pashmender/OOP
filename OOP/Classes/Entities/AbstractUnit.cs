using System;

namespace OOP.Classes.Entities
{

    public abstract class AbstractUnit : GameObject
    {
       
        public static Map GlobalMap { get; set; }
        protected internal House MyHouse;


        //Interaction with other Units
        protected AbstractUnit FoundPartner { get; set; }
        protected const int BirthCooldown = 0;
        protected internal int CurrentBirthCooldown;
        protected AbstractUnit Target { get; set; }

        //Current Unit characteristics
        public int Satiety {  get; protected set; }
        protected const int MinSat = 40;
        protected const int MaxSat = 80;
        protected const int SatPerTurn = 3;
        protected const int SatTreshold = 50;
        public int Stress { get; protected set; }
        protected const int CriticalStress = 100;
        protected const int StressPerTurn = 3;
        //Gender == true => Male, else => Female
        public GameObjectType Gender { get; private set; }
        
        private int _numOfGenders = 2;

        public bool Dead { get; protected set; }

        protected bool _foodWasLocated;
        protected bool FoundGrass;
        protected bool FoundMeat;
        protected int FindFoodX = -1;
        protected int FindFoodY = -1;

        protected internal int DateX = -1;
        protected internal int DateY = -1;

        protected AbstractUnit(int x, int y)
        {
            
            X = x;
            Y = y;
       
            Satiety = Map.Rand.Next(MinSat, MaxSat);
            Stress = 0;
            Dead = false;
            
            
            if (Map.Rand.Next(_numOfGenders) == 0)
                Gender = GameObjectType.Male;
            else
                Gender = GameObjectType.Female;
       
            FoundPartner = null;
        }

        protected void RandomMove()
        {
            Satiety -= SatPerTurn;
            Stress += StressPerTurn;

            int[] wayX = { -1, 0, 1, 0 };
            int[] wayY = { 0, 1, 0, -1 };

            var value = Map.Rand.Next(wayX.Length);

            if (GlobalMap.CheckBorder(X + wayX[value], Y + wayY[value]))
            {

                X += wayX[value];
                Y += wayY[value];

                GlobalMap.Field[X - wayX[value], Y - wayY[value]].Entity.Remove(this);
                GlobalMap.Field[X, Y].Entity.Add(this);

                GlobalMap.ChangedCell.Add(GlobalMap.Field[X - wayX[value], Y - wayY[value]]);
                GlobalMap.ChangedCell.Add(GlobalMap.Field[X, Y]);
            }
        }

        public virtual void Live()
        {
        }


        protected abstract void FindFood();
        

        protected virtual void Eat()
        {
        }

        protected abstract void FindPartner();
        
        protected bool CheckPotentialPartner(AbstractUnit potentialPartner)
        {
            if (CanBeTogether(potentialPartner))
            {
                SetPotentialPartner(potentialPartner);  
                return true;
            }
            
            return false;
        }

        private void SetPotentialPartner(AbstractUnit potentialPartner)
        {
            int x = potentialPartner.X;
            int y = potentialPartner.Y;
            DateX = (X + x) / 2;
            DateY = (Y + y) / 2;
            FoundPartner = potentialPartner;

            potentialPartner.DateX = DateX;
            potentialPartner.DateY = DateY;
            potentialPartner.FoundPartner = this;
        }
        
        protected bool CanBeTogether(AbstractUnit newOne)
        {
            if ((newOne.FoundPartner == null && newOne.Gender != Gender
                && newOne.GetType() == this.GetType()))
            {
                return WillBeAlive(newOne.X, newOne.Y);
            }
            return false;
        }

        private bool WillBeAlive(int newX, int newY)
        {
            int valueX = Math.Abs(X - newX) / 2;
            int valueY = Math.Abs(Y - newY) / 2;

            if ((valueX + valueY) * SatPerTurn >= Satiety - SatTreshold)
                return false;
            else
                return true;
        }

        protected virtual void ProduceNewUnit()
        {
            if (DateX != X || DateY != Y) return;
            FoundPartner.CurrentBirthCooldown = 5;
            FoundPartner.Stress = 0;
            FoundPartner.FoundPartner = null;

            FoundPartner = null;
            CurrentBirthCooldown = 5;
            Stress = 0;
            
            AddUnitToMap();
        }

        protected abstract AbstractUnit CreateChild();
    
        protected void AddUnitToMap()
        {
            AbstractUnit newUnit = CreateChild();
            GlobalMap.Field[newUnit.X, newUnit.Y].Entity.Add(newUnit);
            GlobalMap.CreatedUnits.Add(newUnit);
            newUnit.CurrentBirthCooldown = 10;
        }
        
        protected void MoveTo(int needX, int needY)
        {
            Satiety -= SatPerTurn;
            Stress += StressPerTurn;

            GlobalMap.ChangedCell.Add(GlobalMap.Field[X, Y]);
            GlobalMap.Field[X, Y].Entity.Remove(this);
            
            if (X > needX)
            {
                X -= 1;
            }
            else if (Y > needY)
            {
                Y -= 1;
            }
            else if (X < needX)
            {
                X += 1;
            }
            else if (Y < needY)
            {
                Y += 1;
            }

            GlobalMap.Field[X, Y].Entity.Add(this);
            GlobalMap.ChangedCell.Add(GlobalMap.Field[X, Y]);
        }

        protected virtual void LocateTarget()
        {
        }

        protected void KillUnit()
        {
            Satiety = 100;
            Stress = 0;
            
            Target.Dead = true;
            Target = null;
            FoundMeat = false;
            _foodWasLocated = false;
        }

        protected void SetTarget(AbstractUnit newTarget)
        {
            if (newTarget == null)
                Target = null;
            if (Target == null)
            {
                Target = newTarget; 
            }
            if (WhereShouldGo(newTarget))
            {
                Target = newTarget;
            }
        }
        
        protected bool WhereShouldGo(AbstractUnit newTarget)
        {
            int wayToNewTarget = Math.Abs(X - newTarget.X) + Math.Abs(Y - newTarget.Y);
            int wayToTarget = Math.Abs(X - Target.X) + Math.Abs(Y - Target.Y);

            return wayToNewTarget < wayToTarget;
        }
        
        protected void Sleep()
        {
            Satiety -= SatPerTurn / 2;
            Stress -= StressPerTurn / 2;
            if (Stress < 0)
                Stress = 0;
            else if (Stress < 100)
                Target = null;
        }
        
    }
}
