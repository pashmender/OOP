using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Management.Instrumentation;
using OOP.Classes.Entities;
using OOP.Classes.Entities.Grass;
using OOP.Classes.Entities.OmnivorousUnits;
using OOP.Classes.Intrfaces;
using OOP.Classes.Units;
using OOP.Classes.Units.Grass;
using OOP.Classes.Units.HerbivorousUnits;
using OOP.Classes.Units.PredatorUnits;

namespace OOP.Classes
{
    public class Map
    {
        //Размеры карты
        public int Width { get; private set; }
        public int Height { get; private set; }

        //Карта и графика её
        public Cell[,] Field { get; set; }
        public HashSet<Cell> ChangedCell { get; set; }
        private int DayTime { get; set; }
        private bool _phaseChanged = false;

        public static Random Rand = new Random();

        //Количество юнитов на карте и травы 
        private int _numOfUnits = 2000;

        private int _startGrass = 10500;
        int _minGrassPerTurn = 500;
        int _maxGrassPerTurn = 800;
        private int _droughtTime = 0;
        private const int DrougtChance = 1000;
            
        //Список юнитов для навигации по ним и изменениям
        public List<GameObject> Entity { get; set; }
        public List<GameObject> deadEntityList = new List<GameObject>();
        public List<GameObject> CreatedUnits = new List<GameObject>();

        //private List<GameObject> OBJ { get; set;}
        //Creature = OBJ.oftype(AbstractUNit);
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Field = new Cell[Width, Height];
            DayTime = 12;
            
            Entity = new List<GameObject>();

            ChangedCell = new HashSet<Cell>();


            for (int x = 0; x < Height; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    Field[x, y] = new Cell(x, y);
                    Field[x, y].Entity = new List<GameObject>();
                    ChangedCell.Add(Field[x, y]);
                }
            }

            InitializeUnits();
            InitializeGrass(_startGrass);
        }


        private void InitializeUnits()
        {
            AbstractUnit.GlobalMap = this;
            
            for (int i = 0; i < _numOfUnits;)
            {
                int newX = Rand.Next(Height);
                int newY = Rand.Next(Width);

                
                AbstractUnit newOne = new BearUnit(newX, newY);
                
                var type = Rand.Next(4);
                    
                switch (type)
                {
                    case 0: newOne = new BearUnit(newX, newY);
                        break;
                    case 1: newOne = new Hedgehog(newX, newY);
                        break;
                    case 2: newOne = new Pig(newX,newY);
                        break;
                    case 3: newOne = new Human(newX,newY);
                        break;
                }
                
                Field[newX, newY].Entity.Add(newOne);

                Entity.Add(newOne);
                ChangedCell.Add(Field[newX, newY]);

                i++;
                
            }

            for (int i = 0; i < _numOfUnits;)
            {
                int newX = Rand.Next(Height);
                int newY = Rand.Next(Width);

                if (Field[newX, newY].Entity.FirstOrDefault() == null)
                {
                    AbstractUnit newOne = new Deer(newX, newY);
                    
                    var type = Rand.Next(3);
                    
                    switch (type)
                    {
                        case 0: newOne = new Deer(newX, newY);
                            break;
                        case 1: newOne = new Rabbit(newX, newY);
                            break;
                        case 2: newOne = new Rat(newX,newY);
                            break;
                    }
                    
                    Field[newX, newY].Entity.Add(newOne);
            
                    Entity.Add(newOne);
                    ChangedCell.Add(Field[newX, newY]);
            
                    i++;
                }
            }
            
            for (int i = 0; i < _numOfUnits;)
            {
                int newX = Rand.Next(Height);
                int newY = Rand.Next(Width);
            
                if (Field[newX, newY].Entity.FirstOrDefault() == null)
                {
                    AbstractUnit newOne = new Fox(newX, newY);
                    
                    var type = Rand.Next(3);
                    
                    switch (type)
                    {
                        case 0: newOne = new Fox(newX, newY);
                            break;
                        case 1: newOne = new Wolf(newX, newY);
                            break;
                        case 2: newOne = new Wolverine(newX,newY);
                            break;
                    }
                    
                    Field[newX, newY].Entity.Add(newOne);
            
                    Entity.Add(newOne);
                    ChangedCell.Add(Field[newX, newY]);
            
                    i++;
                }
            }
        }

        private void InitializeGrass(int num)
        {
            AddSomeGrass(num);
        }

        private void AddSomeGrass(int numOfGrass)
        {
            for (int i = 0; i < numOfGrass; i++)
            {
                int curX = Rand.Next(Height);
                int curY = Rand.Next(Width);

                if (Field[curX, curY].Entity.OfType<Grass>().FirstOrDefault() == null)
                {
                    if (!LocateGrass(Field[curX, curY]))
                    {
                        Grass newGrass = new Grass(curX, curY);
                        int type = Rand.Next(3);
                        switch (type)
                        {
                            case 0: newGrass = new Carrot(curX,curY);
                                break;
                            case 1: newGrass = new Trava(curX,curY);
                                break;
                            case 2: newGrass = new Berries(curX,curY);
                                break;
                        }
                        
                        Field[curX, curY].Entity.Add(newGrass);
                        
                        Entity.Add(newGrass);                        
                        ChangedCell.Add(Field[curX, curY]);

                        i++;
                    }
                }
                else 
                {
                   i += NearSpawn(Field[curX, curY]);
                }
            }

        }

        public void NextSituation()
        {
            WorldTimer();
            
            foreach (var unit in Entity.OfType<AbstractUnit>())
            {
                unit.Live();
                if (unit.Dead)
                {
                    Field[unit.X, unit.Y].Entity.Remove(unit);
                    ChangedCell.Add(Field[unit.X, unit.Y]);
                    deadEntityList.Add(unit);
                }
            }

            foreach (var deadEntity in deadEntityList)
            {
                Entity.Remove(deadEntity);
            }
            deadEntityList.Clear();

            foreach (var unit in CreatedUnits)
            {
                Entity.Add(unit);
            }
            
            if (Rand.Next(DrougtChance) == 0)
                _droughtTime = 10;
            
            if(_droughtTime < 0)
                AddSomeGrass(Rand.Next(_minGrassPerTurn, _maxGrassPerTurn) / 2);
            else
                EventDrought();
            
        }


        private bool LocateGrass(Cell cell)
        {
            int radius = 5;

            for(int x = cell.X - radius; x < cell.X + radius; x++)
            {
                for (int y = cell.Y - radius; y < cell.Y + radius; y++)
                {
                    if(CheckBorder(x, y))
                        if(Field[x, y].Entity.OfType<Grass>().FirstOrDefault() != null)
                        {
                            NearSpawn(Field[x, y]);
                            return true;
                        }
                }
            }
            return false;
        }

        private int NearSpawn(Cell cell)
        {
            int[] wayX = { -1, 0, 1, 0 };
            int[] wayY = { 0, 1, 0, -1 };

            int x = cell.X;
            int y = cell.Y;

            int count = 0;

            for (int i = 0; i < wayX.Length; i++)
            {
                int newX = x + wayX[i];
                int newY = y + wayY[i];

                if (CheckBorder(newX, newY)) 
                {
                    if (Field[newX, newY].Entity.OfType<Grass>().FirstOrDefault() == null)
                    {
                        Grass newGrass = new Grass(newX, newY);
                        Field[newX, newY].Entity.Add(newGrass);
                        Entity.Add(newGrass);
                        ChangedCell.Add(Field[newX, newY]);
                        count++;
                    } 
                }
            }

            return count;
        }

        public bool CheckBorder(int x, int y)
        {
            return x >= 0 && x < Height && y >= 0 && y < Width;
        }

        private void WorldTimer() 
        {
            DayTime++;

            if (DayTime == 24)
                DayTime = 0;

            if (CheckTime() != _phaseChanged)
            {
                for (int x = 0; x < Height; x++)
                {
                    for (int y = 0; y < Width; y++)
                    {
                        ChangedCell.Add(Field[x, y]);
                    }
                }
                _phaseChanged = CheckTime();
            }
        }

        public bool CheckTime()
        {
            return DayTime > 20 && DayTime <= 23 ||
                   DayTime >= 0 && DayTime < 5;
        }

        private void EventDrought()
        {
            int MinDeadGrass = 100;
            int MaxDeadGrass = 300;

            int countToDelete = Rand.Next(MinDeadGrass, MaxDeadGrass);
            int temp = 0;
            List<Grass> grassList = new List<Grass>();
            
            foreach (var grass in Entity.OfType<Grass>())
            {
                grassList.Add(grass);
                temp++;
                Field[grass.X, grass.Y].Entity.Remove(grass);
                ChangedCell.Add(Field[grass.X, grass.Y]);
                if (temp == countToDelete)
                    break;
            }

            foreach (var grass in grassList )
            {
                deadEntityList.Add(grass);
            }
            _droughtTime--;
        }

    }
}

