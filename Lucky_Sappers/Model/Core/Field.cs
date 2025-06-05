using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Sizes // свойства поля
    {
        public int Width { get; private set; }//ширина
        public int Height { get; private set; }//длина
        public Kletka[,] Kletochka { get; private set; }//поле со свойствами
        public int Counter { get; private set; }//счётчик мин
        public int Level { get; private set; }//уровень
        public bool Lose { get; private set; }//проигрыш
        public bool Win {  get; private set; }//победа
        private bool firstClick;
        public int Timer {  get; set; }//время

        public Sizes(int width,int height, int level, int Time)
        {
            Width = width;
            Height = height;
            Level = level;
            Kletochka = new Kletka[width, height];
            Timer = Time;
            Counter = (int)((level * Width * Height) / 100);
            GenerateNull();
            BombPlace(level);

        }
        //public Sizes(int width, int height, int level, int Time, Kletka[,]kle)
        //{
        //    Width = width;
        //    Height = height;
        //    Level = level;
        //    Kletochka = new Kletka[width, height];
        //    Timer = Time;
        //    foreach(var k in kle)
        //    {
        //        for (int i = 0; i < Kletochka.GetLength(0); i++)
        //        {
        //            for (int j = 0; j < Kletochka.GetLength(1); j++)
        //            {
        //                Kletochka[i, j]= kle[i, j];
        //            }
        //        }
        //    }

        //}
        private void GenerateNull()// поле со всеми null
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Kletochka[x, y] = new Empty();
                }
            }
        }
        private void BombPlace(double level)// расстановка бомб 
        {
            var random = new Random();
            var bombiki = 0;
            while (bombiki < Counter)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);

                if (Kletochka[x, y] is Empty) // Проверяем, что клетка пустая
                {
                    if (CheckBomb(x, y)) Kletochka[x, y] = new Bomb();
                    bombiki++;
                }
            }
        }
        private bool CheckBomb(int bombX, int bombY)//проверяем соседние бомбы
        {
            int emptyKletkaAround = 0;

            // Проверяем все 8 соседних клеток
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue; // Пропускаем саму бомбу

                    int nx = bombX + dx;
                    int ny = bombY + dy;

                    // Если клетка в пределах поля и пустая (может стать числовой)
                    if (nx >= 0 && nx < Width && ny >= 0 && ny < Height &&
                        Kletochka[nx, ny] is Empty)
                    {
                        emptyKletkaAround++;

                        // Если уже нашли 3 подходящие клетки - можно выходить
                        if (emptyKletkaAround >= 3)
                            return true;
                    }
                }
            }

            return emptyKletkaAround >= 3;
        }
        public void RevealCell(int x, int y)//для открытия соседних пустых полей
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height || Kletochka[x, y].Openspases)
                return;

            Kletochka[x, y].Openspases = true;

            if (Kletochka[x, y] is Bomb)
            {
                Lose = true;
                RevealAllBombs();
                return;
            }

            if (Kletochka[x, y] is Empty && CountBombsAround(x, y) == 0)
            {
                // Открываем соседние клетки
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0) continue;
                        RevealCell(x + dx, y + dy);
                    }
                }
            }

            CheckWinCondition();
        }
        public int CountBombsAround(int bx, int by)
        {
            int count = 0;
            for (int x = bx - 1; x <= bx + 1; x++)
            {
                for (int y = by - 1; y <= by + 1; y++)
                {
                    if (x >= 0 && x < Width && y >= 0 && y < Height && Kletochka[x, y] is Bomb)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public static bool operator true(Sizes field) => !field.Lose && !field.Win;
        public static bool operator false(Sizes field) => field.Lose || field.Win;
    }
}