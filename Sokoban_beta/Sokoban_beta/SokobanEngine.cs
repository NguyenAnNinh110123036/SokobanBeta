using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban_beta
{
    internal class SokobanEngine
    {
        public GameMap Map { get; private set; }
        public Position PlayerPos { get; private set; }

        public SokobanEngine()
        {
            Map = new GameMap();
            PlayerPos = new Position(0, 0);
        }

        public void StartNewGame(string mapPath, int levelNumber)
        {
            if (Map.LoadFromFile(mapPath, levelNumber))
            {
                for (int i = 0; i < Map.Rows; i++)
                {
                    for (int j = 0; j < Map.Cols; j++)
                    {
                        if (Map.Matrix[i, j] == MapElements.Player || Map.Matrix[i, j] == MapElements.PlayerOnTarget)
                        {
                            PlayerPos.Row = i;
                            PlayerPos.Col = j;
                            return;
                        }
                    }
                }
            }
        }

        public void Move(int moveRow, int moveCol)
        {
            int nextRow = PlayerPos.Row + moveRow;
            int nextCol = PlayerPos.Col + moveCol;

            int nextNextRow = nextRow + moveRow;
            int nextNextCol = nextCol + moveCol;

            if (nextRow < 0 || nextRow >= Map.Rows || nextCol < 0 || nextCol >= Map.Cols) return;

            if (Map.Matrix[nextRow, nextCol] == MapElements.Empty || Map.Matrix[nextRow, nextCol] == MapElements.Target)
            {
                ExecutePlayerMove(nextRow, nextCol);
            }
            else if (Map.Matrix[nextRow, nextCol] == MapElements.Box || Map.Matrix[nextRow, nextCol] == MapElements.BoxOnTarget)
            {
                if (nextNextRow >= 0 && nextNextRow < Map.Rows && nextNextCol >= 0 && nextNextCol < Map.Cols)
                {
                    if (Map.Matrix[nextNextRow, nextNextCol] == MapElements.Empty || Map.Matrix[nextNextRow, nextNextCol] == MapElements.Target)
                    {
                        ExecuteBoxPush(nextRow, nextCol, nextNextRow, nextNextCol);
                        ExecutePlayerMove(nextRow, nextCol);
                    }
                }
            }
        }

        private void ExecutePlayerMove(int nextRow, int nextCol)
        {
            Map.Matrix[PlayerPos.Row, PlayerPos.Col] = (Map.Matrix[PlayerPos.Row, PlayerPos.Col] == MapElements.PlayerOnTarget) ? MapElements.Target : MapElements.Empty;
            Map.Matrix[nextRow, nextCol] = (Map.Matrix[nextRow, nextCol] == MapElements.Target) ? MapElements.PlayerOnTarget : MapElements.Player;
            PlayerPos.Row = nextRow;
            PlayerPos.Col = nextCol;
        }

        private void ExecuteBoxPush(int boxRow, int boxCol, int targetRow, int targetCol)
        {
            Map.Matrix[targetRow, targetCol] = (Map.Matrix[targetRow, targetCol] == MapElements.Target) ? MapElements.BoxOnTarget : MapElements.Box;
            Map.Matrix[boxRow, boxCol] = (Map.Matrix[boxRow, boxCol] == MapElements.BoxOnTarget) ? MapElements.Target : MapElements.Empty;
        }
    }
}
