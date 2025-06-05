using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Model.Core;

namespace Model.Data
{

    public class SizeDTO//size
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public KletkaDTO[,] Kletochkadto { get; set; }
        public bool Win { get; set; }
        public bool Lose { get; set; }
        public int Time { get; set; }
        public double Score {  get; set; }
        public int Level { get; set; }
        public SizeDTO() { }
        public SizeDTO(Sizes s)
        {
            Width = s.Width;
            Height = s.Height;
            Level = s.Level;
            Win = s.Win;
            Lose = s.Lose;
            Time =s.Timer;
            Kletochkadto = new KletkaDTO[s.Width, s.Height];
            Score = CalculateScore();
            for (int i = 0; i < s.Width; i++)
            {
                for (int j = 0; j < s.Height; j++)
                {
                    var kletochkadto = s.Kletochka[i, j];
                    Kletochkadto[i, j] = new KletkaDTO
                    {
                        IsBomb = kletochkadto.IsBomb,
                        IsFlagged = kletochkadto.IsFlagged,
                        IsDigit = kletochkadto.IsDigit,
                        Empty = kletochkadto.Empty,
                        Counter = kletochkadto.Counter,
                        CountFlag = kletochkadto.CountFlag
                    };
                }
            }
        }
        private double CalculateScore()
        {
            // Допустим: чем меньше время, тем выше результат
            // Формула: (Width * Height * Level) / TimeInSeconds
            if (Time <= 0) Time = 1; // защита от деления на 0
            return (Width * Height * Level) / Time;
        }
        public Sizes Deserialize()
        {
            var sizes = new Sizes(Width, Height, Level, Time);
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var kletkadto = Kletochkadto[i, j];
                    Kletka kletka;

                    if (kletkadto.IsBomb) kletka = new Bomb();
                    else if (kletkadto.IsFlagged) kletka = new Flag();
                    else if (kletkadto.IsDigit) kletka = new Digit();
                    else if (kletkadto.Empty) kletka = new Empty();
                    else return null;//что вообще за клетка

                    //устанавливаем свойства клетки
                    kletka.IsBomb = kletkadto.IsBomb;
                    kletka.IsFlagged = kletkadto.IsFlagged;
                    kletka.IsDigit = kletkadto.IsDigit;
                    kletka.Empty = kletkadto.Empty;
                    kletka.Counter = kletkadto.Counter;
                    kletka.CountFlag = kletkadto.CountFlag;

                    sizes.Kletochka[i, j] = kletka;
                }
            }
            return sizes;
            //Counter = (int)((level * Width * Height)/100); => level = Counter/Width/Height*100
        }
    }
    public class KletkaDTO
    {
        public bool IsBomb { get; set; }
        public bool IsFlagged { get; set; }
        public bool IsDigit { get; set; }
        public bool Empty { get; set; }
        public int Counter { get; set; }
        public int CountFlag { get; set; }
    }
}