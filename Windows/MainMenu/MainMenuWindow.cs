using System;
using System.Windows;
using System.Windows.Controls;
using phantom_field.Windows.Game;

namespace phantom_field.Windows.MainMenu
{
    class MainMenuWindow : MyWindow
    {
        public MainMenuWindow()
        {
            Title = "Main Menu";
            Height = 500; Width = 400;

            CreateRowDef(4);
            CreateHeader();
            CreateButton();
        }

        protected override void CreateHeader()
        {
            Label title = new Label();
            title.Content = "Phantom Field";
            title.FontSize = 24;
            title.HorizontalAlignment = HorizontalAlignment.Center;
            title.VerticalAlignment = VerticalAlignment.Center;
            GRID.Children.Add(title);
        }

        protected override void CreateButton()
        {
            for (int i = 1; i < 4; i++)
            {
                Button btnLevel = new Button();
                btnLevel.Content = GameWindow.getStringLevel(i - 1);
                btnLevel.Margin = new Thickness(10);
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
    }
}
