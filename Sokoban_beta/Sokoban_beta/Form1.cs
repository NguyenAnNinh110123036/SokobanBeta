using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Sokoban_beta
{
    public partial class Form1 : Form
    {
        private SokobanEngine gameEngine;
        private ImageManager imageManager;
        private GameRenderer renderer;
        private const int TileSize = 54;
        private const int SidePanelWidth = 220;
        private int currentLevel = 1;
        private int stepCount = 0;
        private Timer animationTimer;
        private float renderX;
        private float renderY;
        private float startX;
        private float startY;
        private float targetX;
        private float targetY;
        private float animationProgress;
        private bool isAnimating;
        public Form1()
        {
            InitializeComponent();
            KeyPreview = true;
            DoubleBuffered = true;
            gameEngine = new SokobanEngine();
            animationTimer = new Timer();
            animationTimer.Interval = 16;
            animationTimer.Tick += AnimationTimer_Tick;
            imageManager = new ImageManager(TileSize);
            renderer = new GameRenderer(gameEngine, imageManager, TileSize);
            LoadLevel(currentLevel);
            KeyDown += Form1_KeyDown;
            Paint += Form1_Paint;
        }

        private void LoadLevel(int level)
        {
            gameEngine.StartNewGame("MAP.txt", level);
            renderX = gameEngine.PlayerPos.Col * TileSize;
            renderY = gameEngine.PlayerPos.Row * TileSize;
            Text = $"Sokoban Game - Level {level}";
            stepCount = 0;
            lblStepCount.Text = $"Bước: {stepCount}";
            int mapWidth =  gameEngine.Map.Cols * TileSize;
            int mapHeight = gameEngine.Map.Rows * TileSize;
            ClientSize = new Size( mapWidth + SidePanelWidth, Math.Max(mapHeight, 360));
            int panelX = mapWidth + 15;
            lblStepCount.Location =new Point(panelX, 20); // Vị trí hiển thị số bước
            btnPrevLevel.Location = new Point(panelX, 55); // Nút quay lại level trước
            btnNextLevel.Location = new Point(panelX, 125); // Nút sang level tiếp theo
            btnReturnLevel.Location = new Point(panelX, 200); // Nút quay lại
            renderer.BuildMapCache();
            Invalidate();
            ActiveControl = null;
        }
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            renderer.NextFrame();
            animationProgress += 0.15f;
            if (animationProgress >= 1f)
            {
                animationProgress = 1f;
                renderX = targetX;
                renderY = targetY;
                renderer.NextFrame();
                isAnimating = false;
                animationTimer.Stop();
            }
            else
            {
                renderX = startX + (targetX - startX) * animationProgress;
                renderY = startY + (targetY - startY) * animationProgress;
            }
            Invalidate();
        }
        private void btnPrevLevel_Click(object sender, EventArgs e)
        {
            if (currentLevel > 1)
            {
                currentLevel--;
                LoadLevel(currentLevel);
            }
            else
            {
                MessageBox.Show("Bạn đang ở level đầu tiên rồi!", "Thông báo");
            }
            this.ActiveControl = null;
        }       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (isAnimating)
                return;
            int moveRow = 0;
            int moveCol = 0;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    moveRow = -1;
                    renderer.SetPlayerDirection(Direction.Up);
                    break;

                case Keys.Down:
                    moveRow = 1;
                    renderer.SetPlayerDirection(Direction.Down);
                    break;

                case Keys.Left:
                    moveCol = -1;
                    renderer.SetPlayerDirection(Direction.Left);
                    break;

                case Keys.Right:
                    moveCol = 1;
                    renderer.SetPlayerDirection(Direction.Right);
                    break;

                default:
                    return;
            }

            int oldRow = gameEngine.PlayerPos.Row;

            int oldCol = gameEngine.PlayerPos.Col;

            gameEngine.Move(moveRow, moveCol);

            if (gameEngine.PlayerPos.Row != oldRow || gameEngine.PlayerPos.Col != oldCol)
            {
                stepCount++;
                lblStepCount.Text = "Bước: " + stepCount;
                renderer.NextFrame();
            }
            startX = oldCol * TileSize;
            startY = oldRow * TileSize;
            targetX = gameEngine.PlayerPos.Col * TileSize;
            targetY = gameEngine.PlayerPos.Row * TileSize;
            animationProgress = 0;
            isAnimating = true;
            animationTimer.Start();
            Invalidate();
            CheckWin();
        }
        private void CheckWin()
        {
            if (!gameEngine.Map.CheckWin())
                return;

            if (currentLevel < 5)
            {
                MessageBox.Show(
                    $"Chúc mừng! Bạn hoàn thành Level {currentLevel} với {stepCount} bước.",
                    "Chiến Thắng");

                currentLevel++;

                LoadLevel(currentLevel);
            }
            else
            {
                MessageBox.Show(
                    "Bạn đã phá đảo toàn bộ game!",
                    "PHÁ ĐẢO");
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            renderer.Draw(e.Graphics, renderX, renderY);
            DrawSidePanelLine(e.Graphics);
        }
        private void DrawSidePanelLine(Graphics g)
        {
            int mapWidth = gameEngine.Map.Cols * TileSize;
            using (Pen pen = new Pen(Color.DarkGray, 2))
            {
                g.DrawLine( pen, mapWidth + 5, 0, mapWidth + 5, ClientSize.Height);
            }
        }
        private void btnNextLevel_Click(object sender, EventArgs e)
        {
            if (currentLevel < 5)
            {
                currentLevel++;
                LoadLevel(currentLevel);
            }
            else
            {
                MessageBox.Show("Bạn đang ở level cuối cùng rồi!", "Thông báo");
            }

            this.ActiveControl = null;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
            {
                KeyEventArgs args = new KeyEventArgs(keyData);
                Form1_KeyDown(this, args);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnReturnLevel_Click(object sender, EventArgs e)
        {
            LoadLevel(currentLevel);
            this.ActiveControl = null;
        }
        
    }
}