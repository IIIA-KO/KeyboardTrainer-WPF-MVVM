using KeyboardTrainerModel.Interfaces;
using KeyboardTrainerWPF.MVVM.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace KeyboardTrainerWPF
{
    public partial class MainWindow : Window
    {
        private readonly IUserService users;
        private readonly IScoreService scores;
        private readonly ITextService texts;

        public MainWindow(IUserService userService, IScoreService scoreService, ITextService textService)
        {
            InitializeComponent();
            users = userService;
            scores = scoreService;
            texts = textService;
            DataContext = new MainViewModel(users, scores, texts);
        }
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private bool isMaximazed = false;
        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (isMaximazed)
                {
                    this.WindowState = WindowState.Normal;
                    //this.Width = 1280;
                    //this.Height = 720;
                    isMaximazed = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    isMaximazed = true;
                }
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
