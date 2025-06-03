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
using Model.Data;

namespace Lucky_Sappers
{
    public partial class Game : Form
    {
        private Color c;
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
            var max = Math.Max(width, height);
            var distation = 500/max; // Фиксированный размер ячейки + отступ
            allButtons = new ButtonExtendent[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    ButtonExtendent button = new ButtonExtendent();
                    button.Location = new Point(2 +x * distation, 2+ y * distation);
                    
                    button.Size = new Size(600/max,600/max);

                    // Используем данные из _field.Kletochka
                    button.IsBomb = _field.Kletochka[x, y] is Bomb;
                    button.Emprty = _field.Kletochka[x, y] is Empty;

                    button.BackColor = Color.LightGreen;
                    allButtons[x, y] = button;
                    Controls.Add(button);
                    button.MouseDown += FieldMouseDown;
                }
            }
        }
        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            var ser = new JsonSerializer();
            ser.Serialize(_field,"save");
        }
        
        private void FieldMouseDown(object sender, MouseEventArgs e)
        {
            ButtonExtendent button = (ButtonExtendent)sender;
            if (e.Button == MouseButtons.Left)
            {
                FieldClick(sender, e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                PlantedtheFlag(sender, e);
            }

        }
        private void CheckWinCondition()
        {
            if (AreAllMinesFlagged() && AreAllSafeCellsRevealed())
            {
                WinGame();
            }
        }

        private bool AreAllMinesFlagged()
        {
            for (int x = 0; x < _field.Width; x++)
            {
                for (int y = 0; y < _field.Height; y++)
                {
                    // Если это мина и не помечена флагом - условие не выполнено
                    if (_field.Kletochka[x, y] is Bomb && allButtons[x, y].BackColor != Color.Red)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool AreAllSafeCellsRevealed()
        {
            for (int x = 0; x < _field.Width; x++)
            {
                for (int y = 0; y < _field.Height; y++)
                {
                    // Если это не мина и клетка не открыта - условие не выполнено
                    if (!(_field.Kletochka[x, y] is Bomb) && allButtons[x, y].Enabled)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void WinGame()
        {
            // Показываем все мины с флагами
            for (int x = 0; x < _field.Width; x++)
            {
                for (int y = 0; y < _field.Height; y++)
                {
                    if (_field.Kletochka[x, y] is Bomb)
                    {
                        allButtons[x, y].Text = "🚩";
                        allButtons[x, y].BackColor = Color.Green;
                    }
                }
            }

            MessageBox.Show("Поздравляем! Вы победили!", "Победа");
            Close();
        }

        void PlantedtheFlag(object sender, EventArgs e)
        {
            var button = (ButtonExtendent)sender;
            if (button.BackColor == Color.Red)
            {
                button.BackColor = c;
            }
            else
            {
                button.BackColor = Color.Red;
            }
            CheckWinCondition();

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
                CheckWinCondition();
            }
            else
            {
                EmptyFieldClick(button);
                CheckWinCondition();
            }

        }
        void Explode(ButtonExtendent button)
        {
            for (int x = 0; x < _field.Width; x++)
            {
                for (int y = 0; y < _field.Height; y++)
                {
                    if (_field.Kletochka[x, y] is Bomb)
                    {
                        allButtons[x, y].Text = "*";
                        allButtons[x, y].BackColor = Color.Red;
                    }
                }
            }
            MessageBox.Show("Вы проиграли!");
            Close();
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
                        if (button.Text == "0") button.Text = "";
                        button.Enabled = false;
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
                        if (_field.Kletochka[x, y] is Bomb)
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
