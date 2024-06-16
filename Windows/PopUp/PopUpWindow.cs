using System;
using System.Windows;
using System.Windows.Controls;
using phantom_field.Windows.Game;
using phantom_field.Windows.MainMenu;

namespace phantom_field.Windows.PopUp
{
    class PopUpWindow : MyWindow
    {
        public MyWindow Parent;

        public PopUpWindow(bool Won)
        {
            Title = Won ? "Congratulations!" : "Nice try!";
            Height = 300; Width = Height;
            CreateColRowDef(2);
            CreateHeader();
            CreateButton();
        }

        protected override void CreateHeader()
        {
            Label curTime = new Label();
            curTime.Content = "CurTime";
            curTime.FontSize = 24;
            curTime.HorizontalAlignment = HorizontalAlignment.Center;
            curTime.VerticalAlignment = VerticalAlignment.Center;
            GRID.Children.Add(curTime);

            Label highScore = new Label();
            highScore.Content = "HighTime";
            highScore.FontSize = 24;
            highScore.HorizontalAlignment = HorizontalAlignment.Center;
            highScore.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(highScore, 1);
            GRID.Children.Add(highScore);
        }

        protected override void CreateButton()
        {
            Button btnTryAgain = new Button();
            btnTryAgain.Content = "Try Again";
            btnTryAgain.Margin = new Thickness(10);
            btnTryAgain.Click += TryAgain_clicked;
            Grid.SetRow(btnTryAgain, 1);
            GRID.Children.Add(btnTryAgain);

            Button btnReturn = new Button();
            btnReturn.Content = "Return";
            btnReturn.Margin = new Thickness(10);
            btnReturn.Click += Return_clicked;
            Grid.SetColumn(btnReturn, 1);
            Grid.SetRow(btnReturn, 1);
            GRID.Children.Add(btnReturn);
        }

        void TryAgain_clicked(object sender, EventArgs e)
        {
            Parent.Close();
            new GameWindow().Show();
            this.Close();
        }

        void Return_clicked(object sender, EventArgs e)
        {
            Parent.Close();
            new MainMenuWindow().Show();
            this.Close();
        }
    }
}
