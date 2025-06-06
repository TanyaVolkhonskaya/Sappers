using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
    public partial class Sizes
    {
        public bool Check()
        {
            return AreAllMinesFlagged() && AreAllSafeCellsRevealed();
        }
        public bool AreAllMinesFlagged()//проверка всех мин с флагами
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Kletochka[x, y] is Bomb && !Kletochka[x, y].IsFlagged)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool AreAllSafeCellsRevealed()// проверка открытие всех безопасных клеток
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (!(Kletochka[x, y] is Bomb) && !Kletochka[x, y].Openspases)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void ToggleFlag(int x, int y)//флаг - не флаг
        {
            if (Kletochka[x, y].Openspases) return;

            if (Kletochka[x, y].IsFlagged)
            {
                Kletochka[x, y].IsFlagged = false;
            }
            else
            {
                Kletochka[x, y].IsFlagged = true;
            }
        }
        public void RevealAllBombs()//открытие всех мин на поле
        {
            //if (!firstClick)
            //{
            //    StartTimer();
            //}
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (Kletochka[x, y] is Bomb)
                    {
                        Kletochka[x, y].Openspases = true;
                    }
                }
            }
        }
        public void CheckWinCondition()//меняет значение Win
        {
            Win = true;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    // Если есть не-бомба, которая не открыта и не помечена флагом
                    if (!(Kletochka[x, y] is Bomb) && !Kletochka[x, y].Openspases)
                    {
                        Win = false;
                        return;
                    }
                }
            }
        }
    }
}
