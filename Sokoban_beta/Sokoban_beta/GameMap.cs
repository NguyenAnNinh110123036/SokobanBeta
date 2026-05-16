using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_beta
{
    internal class GameMap
    {
        public int[,] Matrix { get; private set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public bool LoadFromFile(string filePath, int levelNumber)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Không tìm thấy file bản đồ: " + filePath);
                return false;
            }

            string[] allLines = File.ReadAllLines(filePath);
            List<string> levelLines = new List<string>();
            bool isTargetLevel = false;
            for (int i = 0; i < allLines.Length; i++)
            {
                string line = allLines[i].Trim();

                if (line.Equals($"[Level {levelNumber}]", StringComparison.OrdinalIgnoreCase))
                {
                    isTargetLevel = true;
                    continue;
                }
                if (isTargetLevel && line.StartsWith("[Level", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                if (isTargetLevel && !string.IsNullOrEmpty(allLines[i]))
                {
                    levelLines.Add(allLines[i]);
                }
            }

            if (levelLines.Count == 0)
            {
                MessageBox.Show($"Không tìm thấy dữ liệu cho Level {levelNumber} trong file!");
                return false;
            }
            Rows = levelLines.Count;
            Cols = 0;
            for (int i = 0; i < levelLines.Count; i++)
            {
                if (levelLines[i].Length > Cols) Cols = levelLines[i].Length;
            }

            Matrix = new int[Rows, Cols];
            for (int i = 0; i < Rows; i++)
            {
                string currentLine = levelLines[i];
                for (int j = 0; j < Cols; j++)
                {
                    char tile = j < currentLine.Length ? currentLine[j] : ' ';

                    switch (tile)
                    {
                        case 'X': Matrix[i, j] = MapElements.Wall; break;
                        case '*': Matrix[i, j] = MapElements.Box; break;
                        case '.': Matrix[i, j] = MapElements.Target; break;
                        case '@': Matrix[i, j] = MapElements.Player; break;
                        default: Matrix[i, j] = MapElements.Empty; break;
                    }
                }
            }
            return true;
        }
        public bool CheckWin()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (Matrix[i, j] == 3) return false; 
                }
            }
            return true;
        }
    }
}
