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
        private ISerialize serializer;
        private Color c = Color.LightBlue;
        Button[,] allButtons;
        private Sizes _field;
        static private int Time;

        public Game(Sizes field, ISerialize s,int t)
        {
            serializer = s;
            _field = field;
            Time = t;
            InitializeComponent();
            InitializeButtons();
            InitializeTimer();
            MainTimer.Start();

        }
        private void InitializeTimer()
        {
            _uiTimer = new Timer { Interval = 1000 };
            _uiTimer.Tick += UpdateTimerDisplay;
            _uiTimer.Start();
        }
        private void UpdateTimerDisplay(object sender, EventArgs e)
        {
            // Ваш существующий Label для таймера
            second--;
            if (second <= 0) { label1.Text = TimeSpan.FromSeconds(0).ToString(@"\:mm\:ss"); }
            label1.Text = TimeSpan.FromSeconds(second).ToString(@"\:mm\:ss");
            if (second == 0)
            {
                MainTimer.Stop();
                MessageBox.Show("Время вышло");


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
                            button.BackColor = Color.Black;
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


        private void button1_Click(object sender, EventArgs e)
        {

        }
        private int second = Time;

        private void timer1_Trick(object sender, EventArgs e)
        { }
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            serializer.Save(_field);
        }
    }
}