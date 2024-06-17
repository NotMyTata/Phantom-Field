using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace phantom_field.Windows
{
    abstract class MyWindow : Window
    {
        protected Grid GRID;

        public MyWindow()
        {
            GRID = new Grid();
            Content = GRID;
            Background = new SolidColorBrush(Color.FromRgb(244, 117, 27));
            BorderBrush = Brushes.White;
            BorderThickness = new Thickness(2);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.NoResize;
            Loaded += OnWindowLoaded;
            Closed += new EventHandler(OnWindowClosing);
        }

        protected virtual void CreateColRowDef(int n)
        {
            while (n-- > 0)
            {
                GRID.ColumnDefinitions.Add(new ColumnDefinition());
                GRID.RowDefinitions.Add(new RowDefinition());
            }
        }

        protected virtual void CreateColDef(int n)
        {
            while (n-- > 0)
            {
                GRID.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        protected virtual void CreateRowDef(int n)
        {
            while (n-- > 0)
            {
                GRID.RowDefinitions.Add(new RowDefinition());
            }
        }

        protected virtual void CreateHeader() { }
        protected virtual void CreateButton() { }
        protected virtual void CreateBorder(int X, int Y, int ColSpan, int RowSpan) { }
        protected virtual void CreateBackground(int X, int Y, int ColSpan, int RowSpan) { }
        protected virtual void OnWindowLoaded(object sender, EventArgs e) { }
        protected virtual void OnWindowClosing(object sender, EventArgs e) { }
    }
}
