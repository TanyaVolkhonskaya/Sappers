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

namespace Lucky_Sappers
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
        

        private Sizes LoadField(Sizes data)
        {
            var field = new Sizes(data.Width, data.Height, 0);
            for (int x = 0; x < data.Width; x++)
            {
                for (int y = 0; y < data.Height; y++)
                {
                    var cellData = data.Kletochka[x, y];
                    if (cellData.IsBomb)
                    {
                        field.Kletochka[x, y] = new Bomb();
                    }
                    else if (cellData.Counter > 0)
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
            string[] levels = { "10*10", "10*5", "20*20", "15*15", "5*5" };

        }

        private void Start_Game(object sender, EventArgs e)
        {
            var filed = new Sizes(5, 5, 0.2);
            var game = new Game(filed);
            
            game.Show();

        }

        private void Continue_Game(object sender, EventArgs e)
        {
            if (File.Exists("save.json"))
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
    }
}