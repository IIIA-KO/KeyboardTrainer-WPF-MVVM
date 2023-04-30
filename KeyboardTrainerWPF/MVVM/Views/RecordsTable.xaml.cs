using KeyboardTrainerWPF.MVVM.ViewModels;
using System.Windows.Controls;

namespace KeyboardTrainerWPF.MVVM.Views
{
    public partial class RecordsTable : UserControl
    {
        public RecordsTable()
        {
            InitializeComponent();
            DataContext = new RecordsTableViewModel();
        }
    }
}