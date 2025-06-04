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
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;

namespace Lucky_Sappers
{
    public partial class Game : Form
    {
        private  Timer _uiTimer;
        private ISerializer _ser;
        private Color c = Color.LightBlue;
        Button[,] allButtons;
        private readonly Sizes _field;
        private readonly int Time;

        public Game(Sizes field, ISerializer s,int t)
        {
            _ser = s;
            _field = field;
            Time = t;
            InitializeComponent();
            InitializeButtons();
            InitializeTimer();

        }
        private void InitializeTimer()
        {
            _uiTimer = new System.Windows.Forms.Timer { Interval = 500 };
            _uiTimer.Tick += UpdateTimerDisplay;
            _uiTimer.Start();
        }
        private void UpdateTimerDisplay(object sender, EventArgs e)
        {
            // Ваш существующий Label для таймера
            if (label1 != null)
            {
                if (_field.RemainingSeconds.HasValue)
                {
                    // Режим с лимитом времени
                    int remaining = _field.RemainingSeconds.Value;
                    label1.Text = $"Осталось: {remaining / 60:00}:{remaining % 60:00}";

                    // Визуальные эффекты при малом времени
                    if (remaining <= 10)
                    {
                        label1.ForeColor = Color.Red;
                        label1.BackColor = Color.Yellow;
                        if (remaining % 2 == 0) // Мигание
                            label1.BackColor = Color.LightYellow;
                    }

                    // Проверка окончания времени
                    if (remaining <= 0)
                    {
                        _uiTimer.Stop();
                        MessageBox.Show("Время вышло!", "Игра окончена",
                                      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Close();
                    }
                }
                else
                {
                    // Режим без лимита - показываем прошедшее время
                    label1.Text = $"Прошло: {_field.ElapsedSeconds / 60:00}:{_field.ElapsedSeconds % 60:00}";
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _uiTimer.Stop();
            _field.StopTimer();
            base.OnFormClosing(e);
        }
        private void InitializeButtons()
        {
            var width = _field.Width;
            var height = _field.Height;
            var max = Math.Max(width, height);
            var cellSize = 600 / max;

            allButtons = new Button[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var button = new Button
                    {
                        Location = new Point(2 + x * cellSize, 2 + y * cellSize),
                        Size = new Size(cellSize, cellSize),
                        BackColor = c,
                        Tag = new Point(x, y)
                    };

                    button.MouseDown += FieldMouseDown;
                    allButtons[x, y] = button;
                    Controls.Add(button);
                }
            }
        }
        private void Game_Load (object sender, EventArgs e) { }
        
        private void FieldMouseDown(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            var position = (Point)button.Tag;
            int x = position.X;
            int y = position.Y;

            if (_field.Lose || _field.Win)
                return;

            if (e.Button == MouseButtons.Left)
            {
                _field.RevealCell(x, y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                _field.ToggleFlag(x, y);
            }

            UpdateButtons();

            if (_field.Lose)
            {
                MessageBox.Show("Вы проиграли!");
                Close();
            }
            else if (_field.Win)
            {
                MessageBox.Show("Поздравляем! Вы победили!", "Победа");
                Close();
            }
        }
        private void UpdateButtons()
        {
            for (int x = 0; x < _field.Width; x++)
            {
                for (int y = 0; y < _field.Height; y++)
                {
                    var button = allButtons[x, y];
                    var cell = _field.Kletochka[x, y];

                    if (cell.Openspases)
                    {
                        button.Enabled = false;
                        if (cell is Bomb)
                        {
                            button.Text = "*";
                            button.BackColor = Color.Red;
                        }
                        else
                        {
                            int bombsAround = _field.CountBombsAround(x, y);
                            button.Text = bombsAround > 0 ? bombsAround.ToString() : "";
                            button.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        button.Text = "";
                        button.BackColor = cell.IsFlagged ? Color.Red : c;
                    }
                }
            }
        }
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}