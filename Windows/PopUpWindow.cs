using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phantom_field.Windows
{
    class PopUpWindow : MyWindow
    {
        public PopUpWindow()
        {
            Height = 300; Width = Height;
            CreateColRowDef(2);
        }
    }

    class VictoryWindow : PopUpWindow
    {
        public VictoryWindow()
        {
            Title = "Congratulations!";
        }
    }

    class DefeatWindow : PopUpWindow
    {
        public DefeatWindow()
        {
            Title = "Nice try!";
        }
    }
}
