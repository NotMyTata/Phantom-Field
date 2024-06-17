using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using phantom_field.Windows.Game;
using phantom_field.Windows.MainMenu;

namespace phantom_field.Windows.PopUp
{
    class PopUpWindow : MyWindow
    {
        public MyWindow Parent;
        Label curTime, highScore;
        static TimeSpan[] HighScore = new TimeSpan[3];
        bool isClicked;

        public PopUpWindow(bool Won)
        {
            Title = Won ? "Congratulations!" : "Nice try!";
            Height = 300; Width = Height;
            ImageBrush temp = new ImageBrush();
            temp.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"/Images/BGPopUp.png", UriKind.Relative));
            Background = temp;

            isClicked = false;
            CreateColRowDef(2);
            CreateHeader();
            UpdateHeader(Won);
            CreateButton();
        }

        private void UpdateHeader(bool Won)
        {
            if (Won)
            {
                TimeSpan temp = TimeSpan.FromMinutes(GameWindow.countdownTime) - GameWindow.timeLeft;
                curTime.Content = temp.ToString();
                if (temp != TimeSpan.Zero && HighScore[GameWindow.level] > temp || HighScore[GameWindow.level] == TimeSpan.Zero)
                {
                    HighScore[GameWindow.level] = temp;
                }
                highScore.Content = HighScore[GameWindow.level].ToString();
            }
            else
            {
                curTime.Content = "---";
                if (HighScore[GameWindow.level] == TimeSpan.Zero)
                {
                    highScore.Content = "---";
                }
                else highScore.Content = HighScore[GameWindow.level].ToString();
            }
        }

        protected override void CreateHeader()
        {
            curTime = new Label();
            curTime.FontSize = 24;
            curTime.Foreground = Brushes.White;
            curTime.FontWeight = FontWeights.Bold;
            curTime.HorizontalAlignment = HorizontalAlignment.Left;
            curTime.VerticalAlignment = VerticalAlignment.Center;
            curTime.Margin = new Thickness(20, 0, 0, 40);
            Grid.SetColumnSpan(curTime, 2);
            Grid.SetRowSpan(curTime, 2);
            GRID.Children.Add(curTime);

            highScore = new Label();
            highScore.FontSize = 24;
            highScore.Foreground = Brushes.White;
            highScore.FontWeight = FontWeights.Bold;
            highScore.HorizontalAlignment = HorizontalAlignment.Right;
            highScore.VerticalAlignment = VerticalAlignment.Center;
            highScore.Margin = new Thickness(0, 0, 20, 40);
            Grid.SetColumnSpan(highScore, 2);
            Grid.SetRowSpan(highScore, 2);
            GRID.Children.Add(highScore);
        }

        protected override void CreateButton()
        {
            Button btnTryAgain = new Button();
            btnTryAgain.Content = "Try Again";
            btnTryAgain.FontSize = 24;
            btnTryAgain.Background = Brushes.Orange;
            btnTryAgain.BorderBrush = Brushes.Black;
            btnTryAgain.BorderThickness = new Thickness(2);
            btnTryAgain.Margin = new Thickness(10);
            btnTryAgain.Click += TryAgain_clicked;
            Grid.SetRow(btnTryAgain, 1);
            GRID.Children.Add(btnTryAgain);

            Button btnReturn = new Button();
            btnReturn.Content = "Return";
            btnReturn.FontSize = 24;
            btnReturn.Background = Brushes.Orange;
            btnReturn.BorderBrush = Brushes.Black;
            btnReturn.BorderThickness = new Thickness(2);
            btnReturn.Margin = new Thickness(10);
            btnReturn.Click += Return_clicked;
            Grid.SetColumn(btnReturn, 1);
            Grid.SetRow(btnReturn, 1);
            GRID.Children.Add(btnReturn);
        }

        void TryAgain_clicked(object sender, EventArgs e)
        {
            isClicked = true;
            new GameWindow().Show();
            Parent.Close();
            this.Close();
        }

        void Return_clicked(object sender, EventArgs e)
        {
            isClicked = true;
            new MainMenuWindow().Show();
            Parent.Close();
            this.Close();
        }

        protected override void OnWindowClosing(object sender, EventArgs e)
        {
            if (isClicked) return;
            new MainMenuWindow().Show();
            Parent.Close();
        }
    }
}
