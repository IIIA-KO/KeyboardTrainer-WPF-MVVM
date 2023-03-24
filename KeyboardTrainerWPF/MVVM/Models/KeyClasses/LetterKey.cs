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
    internal class LetterKey : KeyButton
    {
        public LetterKey(string value, int row, int col, Key key)
            : base(value.ToLower(), value.ToUpper(), row, col, 2, key) { }

        public override void UpdateValue(bool capsIsPressed, bool shiftIsPressed) =>
            Content.Text = capsIsPressed != shiftIsPressed ? UpperValue : LowerValue;
    }
}