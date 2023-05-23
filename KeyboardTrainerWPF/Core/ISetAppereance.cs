using System.Windows.Media;

namespace KeyboardTrainerWPF.Core
{
    internal interface ISetAppereance   
    {
        public Brush TextColor { get; set; }
        public Brush SecondColor { get; set; }
        public Brush BackgroundColor { get; set; }
        public void SetAppereance(bool isDarkTheme);
    }
}
