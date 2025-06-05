using System;
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

        public bool IsTimerRunning => _gameTimer.IsRunning;
        public int ElapsedSeconds => (int)(_gameTimer.Elapsed.TotalSeconds);

        
        
        public void StartTimer()
        {
            _gameTimer.Start();
        }
        public void StopTimer()
        {
            _gameTimer.Stop();
        }

        public void ReserTimer()
        {
            _gameTimer.Reset();
        }
    }
}