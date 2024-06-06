using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace phantom_field
{
    class PhantomField : Window
    {
        Grid grid;
        int _level, _ncol, _nrow, _flag;
        Label flag, countdown, level;
        DispatcherTimer _timer;
        TimeSpan timeLeft;
        ComboBox levels;

        public PhantomField()
        {
            grid = new Grid();
            grid.ShowGridLines = false;
            Content = grid; Title = "Phantom Field";
            Height = 600; Width = 600;

            _level = 1;
            setTileSize(_level);
            timeLeft = TimeSpan.FromMinutes(5);
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += DispatchTimer_Tick;

            createColDef(grid, _ncol);
            createRowDef(grid, _nrow);

            Border heading = new Border();
            heading.Background = Brushes.Orange;
            //heading.BorderBrush = Brushes.Brown;
            //heading.BorderThickness = new Thickness(2);
            Grid.SetColumnSpan(heading, _ncol);
            grid.Children.Add(heading);

            flag = new Label();
            countdown = new Label();
            level = new Label();
            flag.Content = _flag.ToString();
            countdown.Content = timeLeft.ToString();
            level.Content = getLevel(_level);
            Grid.SetColumn(flag, 0);
            Grid.SetColumn(countdown, 2);
            Grid.SetColumnSpan(countdown, 2);
            Grid.SetColumn(level, 5);
            setFontSize(new UIElement[] { flag, countdown, level }, 24);
            setHVAlignment(new UIElement[] { flag, countdown, level }, HorizontalAlignment.Center, VerticalAlignment.Center);
            grid.Children.Add(flag);
            grid.Children.Add(countdown);
            grid.Children.Add(level);

            Border bFlag = new Border();
            Border bTimer = new Border();
            Border bLevel = new Border();

            Border content = new Border();
            content.Background = Brushes.Chocolate;
            content.BorderBrush = Brushes.Brown;
            content.BorderThickness = new Thickness(2);
            Grid.SetColumnSpan(content, _ncol);
            Grid.SetRow(content, Grid.GetColumn(heading) + 1);
            Grid.SetRowSpan(content, _nrow-1);
            grid.Children.Add(content);

            createTile(grid, _nrow);
        }

        public void setFontSize(UIElement[] elements, int size)
        {
            foreach (var element in elements)
            {
                if (element is Control)
                {
                    ((Control)element).FontSize = size;
                }
            }
        }

        public void setHVAlignment(UIElement[] elements, HorizontalAlignment HA, VerticalAlignment VA)
        {
            foreach (var element in elements)
            {
                if (element is Control)
                {
                    ((Control)element).HorizontalContentAlignment = HA;
                    ((Control)element).VerticalContentAlignment = VA;
                }
                
            }
        }

        public void DispatchTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft == TimeSpan.Zero)
            {
                _timer.Stop();
                
            }
            else
            {
                timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
            }
            countdown.Content = timeLeft.ToString();
        }

        public string getLevel(int level)
        {
            switch (level)
            {
                case 1: return "Easy";
                case 2: return "Medium";
                case 3: return "Hard";
                default:
                    throw new ArgumentOutOfRangeException("level is not supported");
            }
        }

        public void setTileSize(int level)
        {
            switch (level)
            {
                case 1: _ncol = 6; _nrow = 6; break;
                case 2: _ncol = 14; _nrow = 14; break;
                case 3: _ncol = 20; _nrow = 20; break;
                default:
                    throw new ArgumentOutOfRangeException("level is not supported");
            }
        }

        public void createTile(Grid grid, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Button tile = new Button();
                    tile.Background = Brushes.Orange;
                    tile.BorderBrush = Brushes.Brown;
                    tile.BorderThickness = new Thickness(2);
                    tile.Margin = new Thickness(2);
                    tile.Click += new RoutedEventHandler(tile_clicked);
                    Grid.SetColumn(tile, j);
                    Grid.SetRow(tile, i+1);
                    grid.Children.Add(tile);
                }
            }
        }

        public void tile_clicked(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                _timer.Start();
                Button button = (Button)sender;
                grid.Children.Remove(button);
            }
        }

        public void createColDef(Grid grid, int n)
        {
            while(n-- > 0)
            {
                ColumnDefinition cd = new ColumnDefinition();
                grid.ColumnDefinitions.Add(cd);
            }
        }

        public void createRowDef(Grid grid, int n)
        {
            while (n-- > 0)
            {
                RowDefinition rd = new RowDefinition();
                grid.RowDefinitions.Add(rd);
            }
        }
        
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new PhantomField());
        }
    }
}
