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
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lucky_Sappers
{
    public partial class Menu : Form
    {
        private int fieldWidth;
        private int fieldHeight;
        private int Time;
        private int[] text;
        private ISerialize serialize;
        private bool SaveOrNot=false;
        private string format="json";
        private int Procent = 30;
        private Size fields;
        private ISerialize _ser;
        private string FolderPath=Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private string FileName="save";
        private string FullPath=>Path.Combine(FolderPath, FileName+$".{format}");
        
        public Menu()
        {
            serialize = new JsonSerializer();
            InitializeComponent();
            InitializeComboBox();
            InitializeTrackBar();
            //InitializeNewComboBox();
            InitilizeComboBoer();
            changePath();
            FileName = "save";
            format ="json";
        }
                
        private void Menu_Load(object sender, EventArgs e)
        {
            
        }
        private void InitializeTrackBar()
        {
            trackBar1.Minimum = 20;
            trackBar1.Maximum = 40;
            trackBar1.Value = Procent;
            trackBar1.TickFrequency = 1;
            trackBar1.Scroll += Level;
            
            label1.Text = $"{Procent} % мин";
            label1.Location= new Point(trackBar1.Right+10,trackBar1.Top);
            this.Controls.Add(label1);
            trackBar1.Tag = label1;
        }
        private void InitilizeComboBoer()
        {
            comboBox1.Items.AddRange(new object[]
            {
                new {Text ="json", format ="json"},
                new {Text = "XML", format = "xml"}
            });
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "format";
            comboBox1.Text = "Выбери формат";
            comboBox1.SelectedIndex = 0; // Выбираем первый элемент по умолчанию
            comboBox1.SelectedIndexChanged += FormatFile;
        }
        private void FormatFile(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                dynamic select = comboBox1.SelectedItem;
                format = select.format;
                // Инициализируем сериализатор сразу при выборе формата
                switch (format.ToLower())
                {
                    case "json":
                        serialize = new JsonSerializer();
                        break;
                    case "xml":
                        serialize = new SerializerXML();
                        break;
                }
                changePath();
            }
        }
        private void InitializeComboBox()
        {
            comboBox2.Items.AddRange(new object[]
            {
            new { Text = "10x10", Width = 10, Height = 10,Time = 200 },
            new { Text = "10x5",  Width = 10, Height = 5,Time =100 },
            new { Text = "5x5",   Width = 5,  Height = 5, Time =50 },
            new { Text = "1x1", Width = 1, Height = 1 ,Time =10},
            new { Text = "20x20",  Width = 20, Height = 20,Time =400 },
            new { Text = "15x15",   Width = 15,  Height = 15,Time=250 }
             });

            comboBox2.DisplayMember = "Text";
            comboBox2.ValueMember = "Width"; // Можно использовать любое свойство
            comboBox2.Text = "Выбери размерность"; // Выбрать первый элемент по умолчанию

            // Обработчик изменения выбора
            comboBox2.SelectedIndexChanged += SizeField;
        }
        public void MMenu()
        {

            string selectedText = comboBox2.SelectedItem.ToString();

            var name_file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"top.{comboBox2.SelectedItem.ToString()}");

            if (File.Exists(name_file) == true)
            {
                if (selectedText == "json")
                {
                    var xml = new XML_TOP_S();
                    var json = new JSON_SerializerList();

                    text = xml.Deserialize();
                    json.Serialize(text);

                }
                else // XML
                {
                    var xml = new XML_TOP_S();
                    var json = new JSON_SerializerList();
                    text = json.Deserialize();
                    //xml.Serializer_top_10(text);
                }
            }

            tableLayoutPanel2.Controls.Clear();
        }

            
        public void SelectFolder(string path)
        {
            if (path == null) return;
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            FolderPath = path;
        }
        public void SelectFile(string name, string extension)
        {
            if (name == null || FolderPath == null) return;
            var name_file = Path.Combine(FolderPath, $"{name}.{extension}");
            if (File.Exists(name_file) == false)
            {
                using (File.Create(name_file)) { }
            }
            FileName = name_file;
        }
        private void SizeField(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                dynamic selectedItem = comboBox2.SelectedItem;
                fieldWidth = selectedItem.Width;
                fieldHeight = selectedItem.Height;
                Time = selectedItem.Time;
            }
        }

        private void Start_Game(object sender, EventArgs e)
        {}
        private void Level(object sender, EventArgs e)
        {
            Procent=trackBar1.Value;
            label1.Text = $"{Procent}% мин";
            
        }

        private void Continue_Game(object sender, EventArgs e)
        {
            changePath();
        }
        private void changePath()
        {

            label2.Text = FullPath;

        }
        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            format = (string)comboBox1.SelectedItem;
            label2.Text = FullPath;
            switch (format)
            {
                case "json":
                    serialize = new JsonSerializer();
                    break;
                case "xml":
                    serialize = new SerializerXML();
                    break;
            }
            changePath();
        }
        private void Start_button(object sender, EventArgs e)
        {
            if (fieldHeight == 0 || fieldWidth == 0)
            {
                MessageBox.Show("Выберите размерность");
                return;
            }
            var filed = new Sizes(fieldWidth, fieldHeight, Procent,Time);
            var f = format;
            if (format == "json")
            {

                var game = new Game(filed, new JsonSerializer(), Time, true);
                game.Show();
            }
            else if (format == "xml")
            {
                var game = new Game(filed, new SerializerXML(), Time, false);
                game.Show();
            }
            else
            {
                MessageBox.Show("Выберите типы файла");
            }


        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest_1(object sender, EventArgs e)
        {}


        private void label2_Click_1(object sender, EventArgs e)
        {}

        private void Filenamiki(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                var c = textBox1.Text[i];
                if (char.IsDigit(c) || char.IsLetter(c))
                {
                    sb.Append(c);
                }
            }
            FileName = sb.ToString();
            textBox1.Text = sb.ToString();
            if (FileName == "") FileName = "save";
            changePath();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            changePath();
        }

        private void label3_Click(object sender, EventArgs e)
        {}
        private void Continue_Button(object sender, EventArgs e)
        {

            if (serialize == null)
            {
                MessageBox.Show("Сначала выберите формат сохранения");
                return;
            }

            if (!File.Exists(FullPath))
            {
                MessageBox.Show("Файл сохранения не найден");
                return;
            }
            try
            {
                var l = serialize.Load(FullPath);
                if (l != null)
                {
                    var game = new Game(l, serialize, l.Timer, false);
                    game.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Ошибка загрузки");
            }

        }
        private void FilPath_Button(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                FolderPath = folderBrowserDialog1.SelectedPath;
                changePath();
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {}

        private void Menu_Load_1(object sender, EventArgs e)
        {}
    }
}