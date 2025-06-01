using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky_Sappers
{
    public abstract class Kletka : ISvoystva
    {
        public bool IsBomb { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsDigit { get; set; }
        public int Counter { get; set; }
    }
    public class Bomb : Kletka
    {
        public Bomb()
        {
            IsBomb = true;
        }
    }
    public class Flag : Kletka { }
    public class Digit : Kletka { }
    public class Empty : Kletka { }
    public class Sizes // свойства поля
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
            Counter = (int)(level * Width * Height);
            GenerateNull();
            BombPlace();
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
        private void BombPlace()// расстановка бомб 
        {
            var rng = new Random();
            var bombiki = 0;
            while (bombiki < Counter)
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = 0; y < Height; y++)
                    {
                        if (rng.Next(0, 101) < 20)// 20 процентов, потом заменить
                        {
                            Kletochka[x, y] = new Bomb();
                            bombiki++;
                        }
                    }
                }

            }
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
                                        Kletochka[x, y] = new Digit();
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
