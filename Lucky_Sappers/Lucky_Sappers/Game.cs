using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Model.Core;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lucky_Sappers
{
    public partial class Game : Form
    {
        ButtonExtendent[,] allButtons;
        private readonly Sizes _field;
        public Game(Sizes field)
        {
            _field = field;
            InitializeComponent();
        }
        

      private void Game_Load(object sender, EventArgs e)
        {
            var width = _field.Width;
            var height = _field.Height;
            var distation = width + 25;
            allButtons = new ButtonExtendent[width, height];
            var rng = new Random();
            for (int x = 10; (x - 10) < width * distation; x += distation)
            {
                for (int y = 30; (y - 30) < height * distation; y += distation)
                {
                    ButtonExtendent button = new ButtonExtendent();
                    button.Location = new Point(x, y);
                    button.Size = new Size(30, 30);
                    if (rng.Next(0, 101) < 40)
                    {
                        button.IsBomb = true;
                    }
                    else
                    {
                        button.Emprty = true;
                    }
                        allButtons[(x - 10) / distation, (y - 30) / distation] = button;
                    Controls.Add(button);
                    button.Click += new EventHandler(FieldClick);
                }
            }
        }
        void PlantedtheFlag(object sender, EventArgs e)
        {
            var button =(ButtonExtendent)sender;
            if (button.Emprty)
            {
                button.BackColor = Color.Red;
                button.Enabled = false;
            }
            else if (button.BackColor == Color.Red)
            {
                button.BackColor = Color.White;
                button.Enabled = true;
            }
        }
        void FieldClick(object sender, EventArgs e)
        {
            var button = (ButtonExtendent)sender;
            if (button.IsBomb)
            {
                Explode(button);
            }
            else if (button.BackColor == Color.Red)
            {

            }
            else
            {
                EmptyFieldClick(button);
            }

        }
        void Explode(ButtonExtendent button)
        {
            var width = _field.Width;
            var height = _field.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (allButtons[x, y].IsBomb)
                    {
                        allButtons[x, y].Text = "*";
                    }
                }
            }
            MessageBox.Show("Вы проиграли");
        }
        void EmptyFieldClick(ButtonExtendent button)
        {
            var width = _field.Width;
            var height = _field.Height;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (allButtons[x, y] == button)
                    {
                        button.Text = CountBombsAround(x, y).ToString();
                    }
                }
            }
        }
        private int CountBombsAround(int bx, int by)
        {
            var width = _field.Width;
            var height = _field.Height;
            int count = 0;
            for (int x = bx - 1; x <= bx + 1; x++)
            {
                for (int y = by - 1; y <= by + 1; y++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        if (allButtons[x, y].IsBomb)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }

    class ButtonExtendent : Button
    {
        public bool IsBomb;
        public bool Emprty;
    }
}
