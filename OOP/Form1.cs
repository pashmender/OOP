using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OOP.Classes;
using OOP.Classes.Entities;
using OOP.Classes.Entities.Grass;
using OOP.Classes.Entities.OmnivorousUnits;
using OOP.Classes.Units;
using OOP.Classes.Units.Grass;
using OOP.Classes.Units.HerbivorousUnits;
using OOP.Classes.Units.PredatorUnits;

namespace OOP
{
    public partial class Form1 : Form
    {
        private Graphics _graphic;
        private int _width = 1000, _height = 1000;
        private int _res;
        private Map _map;

        public Form1()
        {
            InitializeComponent();
                //InitializeComponent();

            _res = (int)ResolutionNumeric.Value;

            WorldView.Image = new Bitmap(_width * _res, _height * _res);
            _graphic = Graphics.FromImage(WorldView.Image);

            _map = new Map(_width, _height);
        }

        private void StartGame()
        {
            MapColor();
            WorldView.Refresh();
            timer1.Start();
        }

        private void MapColor()
        {
            foreach (var cell in _map.ChangedCell)
            {
                _graphic.FillRectangle(setColor(cell), cell.X * _res, cell.Y * _res, _res, _res);
            }

            _map.ChangedCell.Clear();
        }

        private Brush setColor(Cell cell)
        {
            if (cell.Entity.OfType<House>().FirstOrDefault() != null)
                return Brushes.BurlyWood;
            if (cell.Entity.OfType<Human>().FirstOrDefault() != null)
                return Brushes.Chocolate;
            if (cell.Entity.OfType<Deer>().FirstOrDefault() != null ||
                cell.Entity.OfType<Rabbit>().FirstOrDefault() != null ||
                cell.Entity.OfType<Rat>().FirstOrDefault() != null)
                return Brushes.Crimson;
            
            if (cell.Entity.OfType<BearUnit>().FirstOrDefault() != null ||
                cell.Entity.OfType<Hedgehog>().FirstOrDefault() != null ||
                cell.Entity.OfType<Pig>().FirstOrDefault() != null)
                return Brushes.DarkSlateBlue;
            
            if (cell.Entity.OfType<Wolf>().FirstOrDefault() != null ||
                cell.Entity.OfType<Fox>().FirstOrDefault() != null ||
                cell.Entity.OfType<Wolverine>().FirstOrDefault() != null)
                return Brushes.Black;

            if (cell.Entity.OfType<Grass>().FirstOrDefault() != null &&
                _map.CheckTime())

                return Brushes.DarkGreen;

            if (cell.Entity.OfType<Grass>().FirstOrDefault() != null &&
                    !_map.CheckTime())
                return Brushes.ForestGreen;
            
            return _map.CheckTime() ? Brushes.MediumSeaGreen : Brushes.LightGreen;
        }
        
        
        private void startButton_Click(object sender, EventArgs e)
        {
            StartGame();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _map.NextSituation();
            MapColor();
            WorldView.Refresh();
            labelCount.Text = _map.Entity.OfType<AbstractUnit>().Count().ToString();
        }

        private void WorldView_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / _res;
            int y = e.Y / _res;

            if (e.Button == MouseButtons.Left)
            {
                if (_map.Field[x, y].Entity.OfType<AbstractUnit>().FirstOrDefault() != null)
                {
                    AbstractUnit val = _map.Field[x, y].Entity.OfType<AbstractUnit>().FirstOrDefault();
                    LabelUnit.Text = "Unit gender: " + val.Gender + "\n" +
                        "Unit satiety: " + val.Satiety.ToString() + "\n" +
                        "Unit Stress: " + val.Stress.ToString() + "\n" +
                        "Unit coordinates: " + val.X.ToString() + " " + val.Y.ToString();
                }
                else
                {
                    LabelUnit.Text = "Unit Info will be here";
                }
            }
        }

        private void ResolutionNumeric_ValueChanged(object sender, EventArgs e)
        {
            _res = (int)ResolutionNumeric.Value;

            WorldView.Image = new Bitmap(_width * _res, _height * _res);
            _graphic = Graphics.FromImage(WorldView.Image);

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _map.ChangedCell.Add(_map.Field[x, y]);
                }
            }
            MapColor();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        
    }
}
