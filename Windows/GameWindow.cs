using System;
using System.Windows;
using System.Windows.Controls;
using phantom_field.sounds;

namespace phantom_field.Windows
{
    class GameWindow : MyWindow
    {
        Tile[,] tileList;

        public GameWindow()
        {
            setSize(14);
            CreateColRowDef(Tile.size);

            tileList = new Tile[Tile.size, Tile.size];
            CreateBombs();
            CreateTiles();
            MarkAdjacentBomb();
            AddChildren();

            Title = "Phantom Field";
            Content = GRID;
            Height = 600; Width = Height;
            Audio.playStart();
        }

        // Set Difficulty
        private void setSize(int n) { Tile.size = n; }
        private void setTotalBomb(int n)
        {
            Tile.totalBomb = n; Tile.totalFlag = n;
            Tile.tilesLeft = Tile.size * Tile.size - Tile.totalBomb;
        }


        // Bombs
        private void CreateBombs()
        {
            Random rnd = new Random();
            setTotalBomb(rnd.Next(1, 6));
            int temp = Tile.totalBomb;
            while (temp-- > 0)
            {
                int PosX = rnd.Next(0, Tile.size - 1);
                int PosY = rnd.Next(0, Tile.size - 1);

                Tile newTile = new Tile();
                newTile.PosX = PosX;
                newTile.PosY = PosY;
                newTile.isBomb = true;
                newTile.Click += tile_clicked;
                newTile.MouseRightButtonUp += tile_flagged;
                tileList[PosY, PosX] = newTile;
            }
        }

        // Tiles
        class Tile : Button
        {
            public static int size, totalBomb, totalFlag, tilesLeft;
            public int PosX, PosY, Number;
            public bool isBomb, isFlag, isOpened;

            public Tile()
            {
                isBomb = false;
                isFlag = false;
                isOpened = false;
            }
        }

        private void CreateTiles()
        {
            for (int i = 0; i < Tile.size; i++)
            {
                for (int j = 0; j < Tile.size; j++)
                {
                    if (tileList[i, j] == null)
                    {
                        Tile newTile = new Tile();
                        newTile.PosX = j;
                        newTile.PosY = i;
                        newTile.Click += tile_clicked;
                        newTile.MouseRightButtonUp += tile_flagged;
                        tileList[i, j] = newTile;
                    }
                }
            }
        }

        private bool OutBound(int X, int Y)
        {
            if (X < 0 || Y < 0) return true;
            if (X >= Tile.size || Y >= Tile.size) return true;
            return false;
        }

        private void MarkAdjacentBomb()
        {
            foreach (Tile tile in tileList)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (OutBound(tile.PosX + j, tile.PosY + i)) continue;
                        else if (tileList[tile.PosY + i, tile.PosX + j].isBomb)
                        {
                            tile.Number++;
                        }
                    }
                }
            }
        }

        private void OpenTile(Tile tile)
        {
            tile.isOpened = true;
            if (tile.Number != 0)
            {
                CreateLabel(tile);
            }
            GRID.Children.Remove(tile);
            Tile.tilesLeft--;
        }

        private void CreateLabel(Tile tile)
        {
            Label newLabel = new Label();
            newLabel.Content = tile.Number.ToString();
            newLabel.FontSize = 16;
            newLabel.HorizontalAlignment = HorizontalAlignment.Center;
            newLabel.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(newLabel, tile.PosX);
            Grid.SetRow(newLabel, tile.PosY);
            GRID.Children.Add(newLabel);
        }

        private void OpenAdjacentTile(int X, int Y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (OutBound(X + j, Y + i)) continue;
                    if (tileList[Y + i, X + j].isOpened) continue;
                    else if (!tileList[Y + i, X + j].isBomb)
                    {
                        OpenTile(tileList[Y + i, X + j]);
                        if (tileList[Y + i, X + j].Number == 0) OpenAdjacentTile(X + j, Y + i);
                    }
                }
            }
        }

        // Tile Event
        private void tile_clicked(object sender, EventArgs e)
        {
            Tile tile = (Tile)sender;
            if (tile.isFlag == false)
            {
                if (tile.isBomb)
                {
                    Audio.playLose();
                    new DefeatWindow().ShowDialog();
                }
                else
                {
                    Audio.playClick();
                    OpenTile(tile);
                    if (tile.Number == 0) OpenAdjacentTile(tile.PosX, tile.PosY);
                    if (Tile.tilesLeft == 0)
                    {
                        Audio.playWin();
                        new VictoryWindow().ShowDialog();
                    }
                }
            }
        }

        private void tile_flagged(object sender, EventArgs e)
        {
            Tile tile = (Tile)sender;
            if (tile.isFlag && !tile.isOpened)
            {
                tile.isFlag = false;
                tile.Content = "";
                Tile.totalFlag++;
            }
            else if (tile.isFlag == false && Tile.totalFlag > 0 && !tile.isOpened)
            {
                tile.isFlag = true;
                tile.Content = "F";
                Tile.totalFlag--;
            }
        }

        // Add Tiles as Children of Grid
        private void AddChildren()
        {
            foreach (Tile tile in tileList)
            {
                Grid.SetColumn(tile, tile.PosX);
                Grid.SetRow(tile, tile.PosY);
                GRID.Children.Add(tile);
            }
        }
    }
}