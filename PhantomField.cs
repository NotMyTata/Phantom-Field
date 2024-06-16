using phantom_field.Windows;
using System.Windows;

namespace phantom_field
{
    class PhantomField
    {
        [STAThread]
        static void Main()
        {
            new GameWindow().ShowDialog();
        }
    }
}