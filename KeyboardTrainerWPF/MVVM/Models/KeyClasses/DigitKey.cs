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
    internal class DigitKey : KeyButton
    {
        public DigitKey(string lower, string upper, int row, int column, Key key)
            : base(lower, upper, row, column, 2, key) { }

        public override void UpdateValue(bool capsIsPressed, bool shiftIsPressed) =>
            Content.Text = shiftIsPressed ? UpperValue : LowerValue;
    }
}