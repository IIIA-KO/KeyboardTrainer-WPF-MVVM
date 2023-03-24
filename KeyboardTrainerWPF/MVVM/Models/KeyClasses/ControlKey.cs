using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainerWPF.MVVM.Models.KeyClasses
{
    internal class ControlKey : KeyButton
    {
        public ControlKey(string value, int row, int col, int colSpan, Key key)
            : base(value, value, row, col, colSpan, key) { Content.FontSize = 20; }
    }
}