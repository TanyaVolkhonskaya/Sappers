using Model;
using Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using Model.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Lucky_Sappers
{
    public partial class Menu : Form
    {
        private int fieldWidth;
        private int fieldHeight;
        private int level;
        private int Procent = 30;
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            InitializeComboBox();
            InitializeTrackBar();
        }
        private void InitializeTrackBar()
        {
            trackBar2.Minimum = 20;
            trackBar2.Maximum = 100;
            trackBar2.Value = Procent;
            trackBar2.TickFrequency = 1;
            trackBar2.Scroll += Level;
            Label p = new Label();
            p.Text = $"{Procent} % мин";
            p.Location= new Point(trackBar2.Right+10,trackBar2.Top);
            this.Controls.Add(p);
            trackBar2.Tag = p;
        }
        private void InitializeComboBox()
        {
            comboBox1.Items.AddRange(new object[]
            {
            new { Text = "10x10", Width = 10, Height = 10 },
            new { Text = "10x5",  Width = 10, Height = 5 },
            new { Text = "5x5",   Width = 5,  Height = 5 },
            new { Text = "1x1", Width = 1, Height = 1 },
            new { Text = "20x20",  Width = 20, Height = 20 },
            new { Text = "15x15",   Width = 15,  Height = 15 }
             });

            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Width"; // Можно использовать любое свойство
            comboBox1.Text = "Выбери размерность"; // Выбрать первый элемент по умолчанию

            // Обработчик изменения выбора
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
        }
        private Sizes LoadField(Sizes data)
        {
            var field = new Sizes(data.Width, data.Height, level);
            for (int x = 0; x < data.Width; x++)
            {
                for (int y = 0; y < data.Height; y++)
                {
                    var cellData = data.Kletochka[x, y];
                    if (cellData.IsBomb)
                    {
                        field.Kletochka[x, y] = new Bomb();
                    }
                    else if (cellData.Counter > Procent)
                    {
                        field.Kletochka[x, y] = new Digit();
                    }
                    else field.Kletochka[x, y] = new Empty();
                    field.Kletochka[x, y].IsFlagged = cellData.IsFlagged;
                    field.Kletochka[x, y].IsDigit = cellData.IsDigit;
                    field.Kletochka[x, y].Counter = cellData.Counter;
                }
            }
            return field;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                // Получаем выбранный элемент через отражение
                var selectedItem = comboBox1.SelectedItem;
                var type = selectedItem.GetType();

                fieldWidth = (int)type.GetProperty("Width").GetValue(selectedItem);
                fieldHeight = (int)type.GetProperty("Height").GetValue(selectedItem);
            }
        }

        private void Start_Game(object sender, EventArgs e)
        {
            if (fieldHeight == 0 || fieldWidth == 0)
            {
                MessageBox.Show("Выберите размерность");
                return;
            }
            var filed = new Sizes(fieldWidth, fieldHeight, Procent);
            var game = new Game(filed);
            
            game.Show();

        }

        private void Continue_Game(object sender, EventArgs e)
        {
            string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string FilePath = Path.Combine(FolderPath, $"save.json");
            if (File.Exists(FilePath))
            {
                
                var ser = new JsonSerializer();
                var data = ser.Deserialize< Sizes>("save.json");
                var field = LoadField(data);
                var gameForm = new Game(field);
                gameForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Сохраненная игра не найдена");
            }
        }

        private void Level(object sender, EventArgs e)
        {
            Procent=trackBar2.Value;
            if (trackBar2.Tag is Label label)
            {
                label.Text = $"{Procent}% мин";
            }
        }
    }
}