using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sokoban_beta
{
    public partial class Form1 : Form
    {
        private SokobanEngine gameEngine;
        private const int TileSize = 40;
        private const int SidePanelWidth = 220;
        private int currentLevel = 1;
        private int stepCount = 0;
        private Image imgWall;
        private Image imgPlayer;
        private Image imgBox;
        private Label lblStepCount;
        private Button btnPrevLevel;

        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.DoubleBuffered = true;

            gameEngine = new SokobanEngine();

            // Khởi tạo Label hiển thị số bước đi
            lblStepCount = new Label();
            lblStepCount.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStepCount.ForeColor = Color.Blue;
            lblStepCount.Size = new Size(200, 35);
            lblStepCount.Text = "Steps: 0";
            this.Controls.Add(lblStepCount);

            // KHỞI TẠO NÚT PREV LEVEL
            btnPrevLevel = new Button();
            btnPrevLevel.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            btnPrevLevel.Size = new Size(180, 50);
            btnPrevLevel.Text = "Previous Level";
            btnPrevLevel.UseVisualStyleBackColor = true;
            btnPrevLevel.Click += btnPrevLevel_Click;
            this.Controls.Add(btnPrevLevel);

            LoadImages();
            LoadLevel(currentLevel);

            this.KeyDown += Form1_KeyDown;
            this.Paint += Form1_Paint;
        }

        private void LoadLevel(int level)
        {
            gameEngine.StartNewGame("MAP.txt", level);
            this.Text = "Sokoban Game - Level " + level;

            stepCount = 0;
            lblStepCount.Text = "Steps: " + stepCount;

            int mapWidth = gameEngine.Map.Cols * TileSize;
            int mapHeight = gameEngine.Map.Rows * TileSize;
            // kích thước Form
            this.ClientSize = new Size(mapWidth + SidePanelWidth, Math.Max(mapHeight, 360));

            int panelX = mapWidth + 15;
            lblStepCount.Location = new Point(panelX, 20);         
            btnNextLevel.Location = new Point(panelX, 130); // Nút đi tiếp 
            btnPrevLevel.Location = new Point(panelX, 70);  // Nút quay lại 

            using (Graphics g = this.CreateGraphics())
            {
                g.Clear(Color.White);
            }

            this.Invalidate();
            this.ActiveControl = null;
        }
        private void btnPrevLevel_Click(object sender, EventArgs e)
        {
            // Nếu lớn hơn Level 1 thì mới cho phép lùi về
            if (currentLevel > 1)
            {
                currentLevel--; // Giảm số màn chơi đi 1
                LoadLevel(currentLevel); // Nạp lại bản đồ của màn cũ
            }
            else
            {
                MessageBox.Show("Bạn đang ở level đầu tiên (Level 1) rồi!", "Thông báo");
            }
            this.ActiveControl = null;
        }

        private void LoadImages()
        {
            try
            {
                imgWall = Image.FromFile("wall.png");
                imgPlayer = Image.FromFile("player.png");
                imgBox = Image.FromFile("box.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp ảnh: " + ex.Message);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int moveRow = 0, moveCol = 0;
            switch (e.KeyCode)
            {
                case Keys.Up: moveRow = -1; break;
                case Keys.Down: moveRow = 1; break;
                case Keys.Left: moveCol = -1; break;
                case Keys.Right: moveCol = 1; break;
                default: return;
            }

            int oldRow = gameEngine.PlayerPos.Row;
            int oldCol = gameEngine.PlayerPos.Col;

            gameEngine.Move(moveRow, moveCol);

            if (gameEngine.PlayerPos.Row != oldRow || gameEngine.PlayerPos.Col != oldCol)
            {
                stepCount++;
                lblStepCount.Text = "Steps: " + stepCount;
            }

            this.Invalidate();

            if (gameEngine.Map.CheckWin())
            {
                if (currentLevel < 20)
                {
                    MessageBox.Show($"Chúc mừng! Bạn đã hoàn thành Level {currentLevel} với {stepCount} bước đi. Hệ thống sẽ tự động chuyển sang màn tiếp theo!", "Chiến Thắng");
                    currentLevel++;
                    LoadLevel(currentLevel);
                }
                else
                {
                    MessageBox.Show("Bạn đã phá đảo toàn bộ 20 Level của game Sokoban!", "PHÁ ĐẢO GAME");
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (gameEngine.Map.Matrix == null) return;
            if (imgWall == null || imgPlayer == null || imgBox == null) return;

            Graphics g = e.Graphics;
            int mapWidth = gameEngine.Map.Cols * TileSize;

            // VẼ MAP GAME
            for (int i = 0; i < gameEngine.Map.Rows; i++)
            {
                for (int j = 0; j < gameEngine.Map.Cols; j++)
                {
                    int x = j * TileSize;
                    int y = i * TileSize;

                    switch (gameEngine.Map.Matrix[i, j])
                    {
                        case MapElements.Wall:
                            g.DrawImage(imgWall, x, y, TileSize, TileSize);
                            break;
                        case MapElements.Box:
                            g.DrawImage(imgBox, x, y, TileSize, TileSize);
                            break;
                        case MapElements.Target:
                            g.FillRectangle(Brushes.White, x, y, TileSize, TileSize);
                            g.FillEllipse(Brushes.Red, x + 15, y + 15, 10, 10);
                            break;
                        case MapElements.Player:
                            g.DrawImage(imgPlayer, x, y, TileSize, TileSize);
                            break;
                        case MapElements.BoxOnTarget:
                            g.DrawImage(imgBox, x, y, TileSize, TileSize);
                            g.DrawRectangle(Pens.Green, x, y, TileSize - 1, TileSize - 1);
                            break;
                        case MapElements.PlayerOnTarget:
                            g.FillRectangle(Brushes.White, x, y, TileSize, TileSize);
                            g.FillEllipse(Brushes.Red, x + 15, y + 15, 10, 10);
                            g.DrawImage(imgPlayer, x + 5, y + 5, TileSize - 10, TileSize - 10);
                            break;
                        default:
                            g.FillRectangle(Brushes.White, x, y, TileSize, TileSize);
                            break;
                    }
                }
            }
            using (Pen linePen = new Pen(Color.DarkGray, 2))
            {
                g.DrawLine(linePen, mapWidth + 5, 0, mapWidth + 5, this.ClientSize.Height);
            }
        }

        private void btnNextLevel_Click(object sender, EventArgs e)
        {
            if (currentLevel < 20)
            {
                currentLevel++;
                LoadLevel(currentLevel);
            }
            else
            {
                MessageBox.Show("Bạn đang ở level cuối cùng (Level 20) rồi!", "Thông báo");
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
    }
}