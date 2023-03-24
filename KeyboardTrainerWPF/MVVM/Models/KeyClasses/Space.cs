using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainerWPF.MVVM.Models.KeyClasses
{
    internal class Space : KeyButton
    {
        public Space(int row, int col, int colSpan, Key key)
            : base("Space", "Space", row, col, colSpan, key) { }
    }
}