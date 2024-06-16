using System;
using System.Windows.Controls;
using System.Windows;

namespace phantom_field.Windows
{
    class MyWindow : Window
    {
        protected Grid GRID;

        public MyWindow()
        {
            GRID = new Grid();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ResizeMode = ResizeMode.CanMinimize;
        }
        protected void CreateColRowDef(int n)
        {
            while (n-- > 0)
            {
                GRID.ColumnDefinitions.Add(new ColumnDefinition());
                GRID.RowDefinitions.Add(new RowDefinition());
            }
        }
    }
}
