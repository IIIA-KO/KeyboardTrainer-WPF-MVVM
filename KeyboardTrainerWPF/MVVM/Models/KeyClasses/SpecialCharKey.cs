using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainerWPF.MVVM.Models.KeyClasses
{
    internal class SpecialCharKey : KeyButton
    {
        public SpecialCharKey(string? lower, string? upper, int row, int col, int colSpan, Key key)
            : base(lower, upper, row, col, colSpan, key) { }

        public override void UpdateValue(bool capsIsPressed, bool shiftIsPressed) =>
            Content.Text = shiftIsPressed ? UpperValue : LowerValue;
    }
}