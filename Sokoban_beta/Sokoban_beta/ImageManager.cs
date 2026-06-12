using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sokoban_beta
{
    internal class ImageManager : IDisposable
    {
        private readonly int tileSize;
        public Image Wall { get; private set; }
        public Image Box { get; private set; }
        public Image Target { get; private set; }
        public Image[] WalkUp { get; private set; }
        public Image[] WalkDown { get; private set; }
        public Image[] WalkLeft { get; private set; }
        public Image[] WalkRight { get; private set; }
        public ImageManager(int tileSize)
        {
            this.tileSize = tileSize;
            LoadImages();
        }

        private Image LoadTile(string fileName)
        {
            using (Image original = Image.FromFile(fileName))
            {
                return new Bitmap(original, tileSize, tileSize);
            }
        }

        private void LoadImages()
        {
            Wall = LoadTile("Wall-removebg-preview.png");
            Box = LoadTile("crate-removebg-preview.png");
            Target = LoadTile("crate_target-removebg-preview.png");
            WalkDown = new Image[]
{
        LoadTile("player_down_0.png"),
        LoadTile("player_down_1.png"),
        LoadTile("player_down_2.png")
};

            WalkUp = new Image[]
            {
        LoadTile("player_up_0.png"),
        LoadTile("player_up_1.png"),
        LoadTile("player_up_2.png")
            };

            WalkLeft = new Image[]
            {
        LoadTile("player_left_0.png"),
        LoadTile("player_left_1.png"),
        LoadTile("player_left_2.png")
            };

            WalkRight = new Image[]
            {
        LoadTile("player_right_0.png"),
        LoadTile("player_right_1.png"),
        LoadTile("player_right_2.png")
            };
        }

        public void Dispose()
        {
            Wall?.Dispose();
            Box?.Dispose();
            Target?.Dispose();
            if (WalkUp != null)
                foreach (var img in WalkUp)
                    img?.Dispose();

            if (WalkDown != null)
                foreach (var img in WalkDown)
                    img?.Dispose();

            if (WalkLeft != null)
                foreach (var img in WalkLeft)
                    img?.Dispose();

            if (WalkRight != null)
                foreach (var img in WalkRight)
                    img?.Dispose();
        }
    }
}
