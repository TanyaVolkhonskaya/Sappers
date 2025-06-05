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
        private int? _timeLimitSeconds;

        public bool IsTimerRunning => _gameTimer.IsRunning;
        public int ElapsedSeconds => (int)(_gameTimer.Elapsed.TotalSeconds);

        public int? RemainingSeconds
        {
            get
            {
                if (!_timeLimitSeconds.HasValue) return null;
                return Math.Max(0, _timeLimitSeconds.Value - ElapsedSeconds);
            }
        }

        
        public void StartTimer()
        {
            _gameTimer.Start();
        }
        public void StopTimer()
        {
            _gameTimer.Stop();
        }

        public bool CheckTimeExpired()
        {
            return _timeLimitSeconds.HasValue && ElapsedSeconds >= _timeLimitSeconds.Value;
        }
    }
}