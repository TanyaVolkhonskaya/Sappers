using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Sizes // свойства поля
    {
        public int Width { get; }
        public int Height { get; }
        public Kletka[,] Kletochka { get; }
        private int Counter { get; }
        public Sizes(int width, int height, double level)
        {
            Width = width;
            Height = height;
            Kletochka = new Kletka[width, height];
            Counter = (int)((level * Width * Height)/100);
            GenerateNull();
            BombPlace(level);
            CountNumber();
        }
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
            var rng = new Random();
            var bombiki = 0;
            while (bombiki < Counter)
            {
                int x = rng.Next(0, Width);
                int y = rng.Next(0, Height);

                if (Kletochka[x, y] is Empty ) // Проверяем, что клетка пустая
                {
                    if (CheckBomb(x, y))Kletochka[x, y] = new Bomb();
                    bombiki++;
                }
            }
        }
        private bool CheckBomb(int bombX, int bombY)
        {
            int emptyCellsAround = 0;

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
                        emptyCellsAround++;

                        // Если уже нашли 3 подходящие клетки - можно выходить
                        if (emptyCellsAround >= 3)
                            return true;
                    }
                }
            }

            return emptyCellsAround >= 3;
        }
        private void CountNumber()// число на клетке
        {
            for (int x1 = 0; x1 < Width; x1++)
            {
                for (int y1 = 0; y1 < Height; y1++)
                {
                    if (Kletochka[x1, y1].IsBomb)
                    {
                        for (int x = x1 - 1; x <= x1 + 1; x++)
                        {
                            for (int y = y1 - 1; y <= y1 + 1; y++)
                            {
                                if (x >= 0 && x < Width && y >= 0 && y < Height)
                                {
                                    if (!(Kletochka[x, y] is Bomb))
                                    {
                                        Kletochka[x, y] = new Digit();// подсчёт бомб вокруг
                                    }
                                    Kletochka[x, y].Counter++;
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
