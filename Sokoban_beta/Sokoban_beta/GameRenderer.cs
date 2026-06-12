using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sokoban_beta
{
    internal class GameRenderer
    {
        private readonly SokobanEngine engine;
        private readonly ImageManager images;
        private readonly int tileSize;
        private Direction playerDirection = Direction.Down;
        private int currentFrame = 0;
        private int frameDirection = 1;
        public Bitmap MapCache { get; private set; }

        public GameRenderer(
            SokobanEngine engine,
            ImageManager images,
            int tileSize)
        {
            this.engine = engine;
            this.images = images;
            this.tileSize = tileSize;
        }
        public void SetPlayerDirection(Direction dir)
        {
            playerDirection = dir;
        }
        public void NextFrame()
        {
            currentFrame += frameDirection;
            if (currentFrame >= 2) 
                frameDirection = -1;
            if (currentFrame <= 0) 
                frameDirection = 1;
        }
        public void ResetFrame()
        {
            currentFrame = 0;
            frameDirection = 1;
        }
        private Image GetPlayerImage()
        {
            switch (playerDirection)
            {
                case Direction.Up:
                    return images.WalkUp[currentFrame];

                case Direction.Down:
                    return images.WalkDown[currentFrame];

                case Direction.Left:
                    return images.WalkLeft[currentFrame];

                case Direction.Right:
                    return images.WalkRight[currentFrame];
            }

            return images.WalkDown[0];
        }
        public void BuildMapCache()
        {
            MapCache?.Dispose();

            int width = engine.Map.Cols * tileSize;
            int height = engine.Map.Rows * tileSize;

            MapCache = new Bitmap(width, height);

            using (Graphics g =
                   Graphics.FromImage(MapCache))
            {
                g.Clear(Color.White);

                using (Brush floorBrush =
                    new SolidBrush(Color.FromArgb(235, 220, 200)))
                {
                    for (int i = 0; i < engine.Map.Rows; i++)
                    {
                        int firstWall = -1;
                        int lastWall = -1;

                        for (int j = 0; j < engine.Map.Cols; j++)
                        {
                            if (engine.Map.Matrix[i, j] == MapElements.Wall)
                            {
                                if (firstWall == -1)
                                    firstWall = j;

                                lastWall = j;
                            }
                        }

                        for (int j = 0; j < engine.Map.Cols; j++)
                        {
                            int x = j * tileSize;
                            int y = i * tileSize;

                            int tile =
                                engine.Map.Matrix[i, j];

                            bool insideMap =
                                firstWall != -1 &&
                                j >= firstWall &&
                                j <= lastWall;

                            if (insideMap)
                            {
                                g.FillRectangle(
                                    floorBrush,
                                    x,
                                    y,
                                    tileSize,
                                    tileSize);
                            }

                            if (tile == MapElements.Wall)
                            {
                                g.DrawImageUnscaled(
                                    images.Wall,
                                    x,
                                    y);
                            }

                            if (tile == MapElements.Target)
                            {
                                g.DrawImageUnscaled(
                                    images.Target,
                                    x,
                                    y);
                            }
                        }
                    }
                }
            }
        }

        public void Draw(Graphics g, float renderX, float renderY)
        {
            if (MapCache == null)
                return;

            g.DrawImageUnscaled(MapCache, 0, 0);

            for (int i = 0; i < engine.Map.Rows; i++)
            {
                for (int j = 0; j < engine.Map.Cols; j++)
                {
                    int tile =
                        engine.Map.Matrix[i, j];

                    int x = j * tileSize;
                    int y = i * tileSize;

                    switch (tile)
                    {
                        case MapElements.Box:

                            g.DrawImageUnscaled(
                                images.Box,
                                x,
                                y);
                            break;

                        case MapElements.BoxOnTarget:

                            g.DrawImageUnscaled(
                                images.Box,
                                x,
                                y);

                            g.DrawRectangle(
                                Pens.Green,
                                x,
                                y,
                                tileSize - 1,
                                tileSize - 1);
                            break;                   
                    }
                }
            }
            using (Brush shadow = new SolidBrush(
            Color.FromArgb(70, Color.Black)))
            {
                g.FillEllipse(
                    shadow,
                    renderX + 10,
                    renderY + tileSize - 12,
                    tileSize - 20,
                    8);
            }
            g.DrawImage(
                GetPlayerImage(),
                renderX,
                renderY,
                tileSize,
                tileSize);
        }
    }
}
