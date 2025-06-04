//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Model.Data
//{
//    public class HighScoreEntry
//    {
//        public string PlayerName { get; set; }
//        public double Score { get; set; }
//        public DateTime Date { get; set; }
//        public int Level { get; set; }
//        public int TimeSeconds { get; set; }
//        public string FieldSize { get; set; }
//    }

//    public static class HighScoreManager
//    {
//        private static readonly Serializer _serializer = new JsonSerializer();
//        private const string HighScoreFile = "highscores";

//        public static List<HighScoreEntry> LoadHighScores()
//        {
//            try
//            {
//                return _serializer.Deserialize<List<HighScoreEntry>>(HighScoreFile)
//                       ?? new List<HighScoreEntry>();
//            }
//            catch
//            {
//                return new List<HighScoreEntry>();
//            }
//        }

//        public static void SaveHighScore(GameState gameState, string playerName)
//        {
//            var scores = LoadHighScores();

//            scores.Add(new HighScoreEntry
//            {
//                PlayerName = playerName,
//                Score = gameState.CalculateScore(),
//                Date = DateTime.Now,
//                Level = gameState.Level,
//                TimeSeconds = gameState.ElapsedSeconds,
//                FieldSize = $"{gameState.Width}x{gameState.Height}"
//            });

//            // Сохраняем только топ-10 результатов
//            var topScores = scores
//                .OrderByDescending(s => s.Score)
//                .Take(10)
//                .ToList();

//            _serializer.Serialize(topScores, HighScoreFile);
//        }
//    }
//}