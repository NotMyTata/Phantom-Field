﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using phantom_field.sounds;
using phantom_field.Windows.MainMenu;
using phantom_field.Windows.PopUp;

namespace phantom_field.Windows.Game
{
    class GameWindow : MyWindow
    {
        Tile[,] tileList;
        Label flag, time;
        DispatcherTimer dispatcherTimer;
        public static TimeSpan timeLeft;
        public static int level;
        public static double countdownTime;
        bool hasStarted;

        public GameWindow()
        {
            hasStarted = false;
            countdownTime = 5;
            setSize();
            tileList = new Tile[Tile.size+1, Tile.size];
            setTotalBomb(GenerateRandomBomb());
            CreateHeader();
            CreateColRowDef(Tile.size);
            CreateBombs();
            CreateTiles();
            MarkAdjacentBomb();
            ValidateBomb();
            AddChildren();

            Title = "Phantom Field, Difficulty: " + getStringLevel(level);
            Height = 700; Width = 600;
            ImageBrush temp = new ImageBrush();
            temp.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"/Images/BGGame.png", UriKind.Relative));
            Background = temp;
        }

        // Set Difficulty
        void setSize()
        {
            switch (level)
            {
                case 1: Tile.size = 14; break;
                case 2: Tile.size = 20; break;
                default: Tile.size = 6; break;
            }
        }

        void setTotalBomb(int n)
        {
            Tile.totalBomb = n;
            Tile.totalFlag = n;
            Tile.tilesLeft = Tile.size * Tile.size - Tile.totalBomb;
        }

        // Bombs
        int GenerateRandomBomb()
        {
            int temp = Tile.size*Tile.size;
            switch (level)
            {
                case 1: return new Random().Next((int)(temp*0.15), (int)(temp*0.2));
                case 2: return new Random().Next((int)(temp*0.2), (int)(temp*0.25));
                default: return new Random().Next((int)(temp*0.1), (int)(temp*0.15));
            }
        }
        
        void CreateBombs()
        {
            Random rnd = new Random();
            int temp = Tile.totalBomb;
            while (temp-- > 0)
            {
                int PosX = rnd.Next(1, Tile.size);
                int PosY = rnd.Next(1, Tile.size);

                while (tileList[PosY, PosX] != null && tileList[PosY, PosX].isBomb == true)
                {
                    PosX = rnd.Next(1, Tile.size);
                    PosY = rnd.Next(1, Tile.size);
                }

                CreateNewTiles(PosX, PosY);
                tileList[PosY, PosX].isBomb = true;
            }
        }

        void ValidateBomb()
        {
            int bomb = 0;
            foreach(Tile tile in tileList)
            {
                if (tile != null && tile.isBomb) bomb++;
            }
            if (bomb != Tile.totalBomb) this.Close();
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

        void CreateNewTiles(int X, int Y)
        {
            Tile newTile = new Tile();
            newTile.PosX = X;
            newTile.PosY = Y;
            newTile.Background = Brushes.Orange;
            newTile.Click += tile_clicked;
            newTile.MouseRightButtonUp += tile_flagged;
            tileList[Y, X] = newTile;
            CreateBorder(X, Y, 1, 1);
        }

        void CreateTiles()
        {
            for (int i = 1; i <= Tile.size; i++)
            {
                for (int j = 0; j < Tile.size; j++)
                {
                    if (tileList[i, j] == null)
                    {
                        CreateNewTiles(j, i);
                    }
                }
            }
        }

        bool OutBound(int X, int Y)
        {
            if (X < 0 || Y < 1) return true;
            if (X >= Tile.size || Y > Tile.size) return true;
            return false;
        }

        void MarkAdjacentBomb()
        {
            foreach (Tile tile in tileList)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (tile == null || OutBound(tile.PosX + j, tile.PosY + i)) continue;
                        else if (tileList[tile.PosY + i, tile.PosX + j].isBomb)
                        {
                            tile.Number++;
                        }
                    }
                }
            }
        }

        void OpenTile(Tile tile)
        {
            tile.isOpened = true;
            if (tile.Number != 0)
            {
                CreateLabel(tile);
            }
            GRID.Children.Remove(tile);
            Tile.tilesLeft--;
        }

        void OpenAdjacentTile(int X, int Y)
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
        void tile_clicked(object sender, EventArgs e)
        {
            Tile tile = (Tile)sender;
            if (tile.isFlag == false)
            {
                if (tile.isBomb)
                {
                    Audio.playLose();
                    OpenPopUpWindow(false);
                }
                else
                {
                    Audio.playClick();
                    if (!hasStarted)
                    {
                        dispatcherTimer.Start();
                        hasStarted = true;
                    }
                    OpenTile(tile);
                    if (tile.Number == 0) OpenAdjacentTile(tile.PosX, tile.PosY);
                    if (Tile.tilesLeft == 0)
                    {
                        Audio.playWin();
                        OpenPopUpWindow(true);
                    }
                }
            }
        }

        void tile_flagged(object sender, EventArgs e)
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
            flag.Content = Tile.totalFlag.ToString();
        }

        // Add Tiles as Children of Grid
        void AddChildren()
        {
            foreach (Tile tile in tileList)
            {
                if (tile == null) continue;
                Grid.SetColumn(tile, tile.PosX);
                Grid.SetRow(tile, tile.PosY);
                GRID.Children.Add(tile);
            }
        }

        void CreateLabel(Tile tile)
        {
            Label newLabel = new Label();
            newLabel.Content = tile.Number.ToString();
            newLabel.FontSize = 16;
            newLabel.Foreground = SetLabelColor(tile.Number);
            newLabel.HorizontalAlignment = HorizontalAlignment.Center;
            newLabel.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(newLabel, tile.PosX);
            Grid.SetRow(newLabel, tile.PosY);
            GRID.Children.Add(newLabel);
        }

        SolidColorBrush SetLabelColor(int number)
        {
            switch (number)
            {
                case 1: return Brushes.Blue;
                case 2: return Brushes.Green;
                case 3: return Brushes.Red;
                case 4: return Brushes.Purple;
                case 5: return Brushes.DarkOrange;
                case 6: return Brushes.Aquamarine;
                case 7: return Brushes.Black;
                case 8: return Brushes.Gray;
                default: return Brushes.White;
            }
        }

        void OpenPopUpWindow(bool Won)
        {
            dispatcherTimer.Stop();
            PopUpWindow PUW = new PopUpWindow(Won);
            PUW.Parent = this;
            PUW.ShowDialog();
        }

        static public string getStringLevel()
        {
            switch (level)
            {
                case 1: return "Medium";
                case 2: return "Hard";
                default: return "Easy";
            }
        }

        static public string getStringLevel(int n)
        {
            switch (n)
            {
                case 1: return "Medium";
                case 2: return "Hard";
                default: return "Easy";
            }
        }

        protected override void OnWindowLoaded(object sender, EventArgs e)
        {
            Audio.playStart();
        }

        private void CreateTimer()
        {
            timeLeft = TimeSpan.FromMinutes(countdownTime);
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0,0,0,1);
            dispatcherTimer.Tick += Countdown;
        }

        private void Countdown(object sender, EventArgs e)
        {
            if (timeLeft == TimeSpan.Zero) OpenPopUpWindow(false);
            else
            {
                timeLeft -= TimeSpan.FromSeconds(1);
                time.Content = timeLeft.ToString();
            }
        }

        protected override void CreateHeader()
        {
            RowDefinition rd = new RowDefinition();
            rd.MinHeight = 100;
            rd.MaxHeight = 100;
            GRID.RowDefinitions.Add(rd);

            flag = new Label();
            flag.Content = Tile.totalFlag.ToString();
            flag.FontSize = 24;
            flag.Foreground = Brushes.White;
            flag.Margin = new Thickness(40,0,0,0);
            flag.HorizontalAlignment = HorizontalAlignment.Left;
            flag.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumnSpan(flag, Tile.size);
            GRID.Children.Add(flag);

            time = new Label();
            CreateTimer();
            time.Content = timeLeft.ToString();
            time.FontSize = 24;
            time.Foreground = Brushes.White;
            time.Margin = new Thickness(10);
            time.HorizontalAlignment = HorizontalAlignment.Center;
            time.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumnSpan(time, Tile.size);
            GRID.Children.Add(time);

            Label dif = new Label();
            dif.Content = getStringLevel(level);
            dif.FontSize = 24;
            dif.Foreground = Brushes.White;
            dif.Margin = new Thickness(0,0,70,0);
            dif.HorizontalAlignment = HorizontalAlignment.Right;
            dif.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumnSpan(dif, Tile.size);
            GRID.Children.Add(dif);
        }

        protected override void CreateBorder(int X, int Y, int ColSpan, int RowSpan)
        {
            Border border = new Border();
            border.Background = Brushes.LightYellow;
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(1);
            Grid.SetColumn(border, X);
            Grid.SetRow(border, Y);
            Grid.SetColumnSpan(border, ColSpan);
            Grid.SetRowSpan(border, RowSpan);
            GRID.Children.Add(border);
        }
    }
}