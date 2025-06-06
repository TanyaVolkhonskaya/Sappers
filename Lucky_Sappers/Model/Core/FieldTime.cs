﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Sizes
    {
        private Stopwatch _gameTimer = new Stopwatch();

        public int ElapsedSeconds => (int)(_gameTimer.Elapsed.TotalSeconds);//прошедшее время
        public int RemainingSeconds => Math.Max(0, Timer - ElapsedSeconds);//оставшееся время


        public void StartTimer()
        {
            _gameTimer.Start();
        }
        public void StopTimer()
        {
            _gameTimer.Stop();
        }

        //public void ReserTimer()
        //{
        //    _gameTimer.Reset();
        //}
        //public bool IsTimeExpired() //проверка окончания таймера
        //{ return RemainingSeconds <= 0; }
    }
}