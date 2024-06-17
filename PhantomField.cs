using phantom_field.Windows.MainMenu;
using System.Windows;

namespace phantom_field
{
    class PhantomField
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new MainMenuWindow());
        }
    }
}