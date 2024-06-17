using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using phantom_field.sounds;
using phantom_field.Windows.Game;

namespace phantom_field.Windows.MainMenu
{
    class MainMenuWindow : MyWindow
    {
        public MainMenuWindow()
        {
            Title = "Main Menu";
            Height = 500; Width = 400;
            
            ImageBrush temp = new ImageBrush();
            temp.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"/Images/BGMainMenu.png", UriKind.Relative));
            Background = temp;

            CreateRowDef(4);
            CreateHeader();
            CreateButton();

            this.Loaded += OnWindowLoaded;
        }

        protected override void CreateHeader()
        {
            Label title = new Label();
            title.Content = "Phantom Field";
            title.FontSize = 24;
            title.Foreground = Brushes.White;
            title.FontWeight = FontWeights.Bold;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(title, 0);
            GRID.Children.Add(title);
        }

        protected override void CreateButton()
        {
            for (int i = 1; i < 4; i++)
            {
                Button btnLevel = new Button();
                btnLevel.Content = GameWindow.getStringLevel(i - 1);
                btnLevel.Margin = new Thickness(10);
                btnLevel.FontSize = 24;
                btnLevel.Background = Brushes.Orange;
                btnLevel.BorderBrush = Brushes.Black;
                btnLevel.BorderThickness = new Thickness(2);
                btnLevel.Click += button_clicked;
                Grid.SetRow(btnLevel, i);
                GRID.Children.Add(btnLevel);
            }
        }

        void button_clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.ToString() == "Easy") GameWindow.level = 0;
            else if (button.Content.ToString() == "Medium") GameWindow.level = 1;
            else if (button.Content.ToString() == "Hard") GameWindow.level = 2;
            new GameWindow().Show();
            this.Close();
        }

        protected override void OnWindowLoaded(object sender, EventArgs e)
        {
            Audio.playBGM();
        }

        //private void CreateDock()
        //{
        //    MenuItem Open = new MenuItem();
        //    MenuItem Close = new MenuItem();
        //    MenuItem Save = new MenuItem();
        //    Open.Header = "_Open";
        //    Close.Header = "_Close";
        //    Save.Header = "_Save";

        //    Menu a = new Menu();
        //    a.Items.Add(Open);
        //    a.Items.Add(Close);
        //    a.Items.Add(Save);

        //    GRID.Children.Add(a);
        //}
    }
}
