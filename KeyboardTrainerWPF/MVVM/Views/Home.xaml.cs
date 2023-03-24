using KeyboardTrainerWPF.MVVM.Models.KeyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Color = System.Windows.Media.Color;


namespace KeyboardTrainerWPF.MVVM.Views
{
    public partial class Home : UserControl
    {
        const int PRINTABLE_CHARS = 47;

        Dictionary<Key, KeyButton> KeyboardButtons;
        Random rand;
        DateTime startTime;
        int correctlyTypedTextLength;
        int fails;

        public Home()
        {
            InitializeComponent();

            rand = new Random(DateTime.Now.Millisecond);

            KeyboardButtons = new Dictionary<Key, KeyButton>();
            GenerateKeyboardButtons(KeyboardButtons);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnStop.IsEnabled = true;
                btnStart.IsEnabled = false;
                cbCaseSensitive.IsEnabled = false;
                sliderDifficulty.IsEnabled = false;

                tbTypedText.Text = "";
                tbTypedText.Focus();

                startTime = DateTime.Now;

                correctlyTypedTextLength = 0;
                fails = 0;
                tbFails.Text = "0";
                tbSpeed.Text = "0";

                int difficulty;
                difficulty = int.Parse(tbDifficulty.Text);

                tbDifficulty.Text = difficulty.ToString();
                tbGeneratedText.Text = GenerateText(difficulty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }
        private void Stop()
        {
            try
            {
                btnStart.IsEnabled = true;
                btnStop.IsEnabled = false;
                cbCaseSensitive.IsEnabled = true;
                sliderDifficulty.IsEnabled = true;

                tbTypedText.Text = "";
                tbGeneratedText.Text = "";
                tbFails.Text = "0";
                tbSpeed.Text = "0";

                fails = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GenerateKeyboardButtons(Dictionary<Key, KeyButton> keyboard)
        {
            try
            {
                KeyboardButtons[Key.Oem3] = new SpecialCharKey("`", "~", 0, 0, 2, Key.Oem3);
                KeyboardButtons[Key.D1] = new DigitKey("1", "!", 0, 2, Key.D1);
                KeyboardButtons[Key.D2] = new DigitKey("2", "@", 0, 4, Key.D2);
                KeyboardButtons[Key.D3] = new DigitKey("3", "#", 0, 6, Key.D3);
                KeyboardButtons[Key.D4] = new DigitKey("4", "$", 0, 8, Key.D4);
                KeyboardButtons[Key.D5] = new DigitKey("5", "%", 0, 10, Key.D5);
                KeyboardButtons[Key.D6] = new DigitKey("6", "^", 0, 12, Key.D6);
                KeyboardButtons[Key.D7] = new DigitKey("7", "&", 0, 14, Key.D7);
                KeyboardButtons[Key.D8] = new DigitKey("8", "*", 0, 16, Key.D8);
                KeyboardButtons[Key.D9] = new DigitKey("9", "(", 0, 18, Key.D9);
                KeyboardButtons[Key.D0] = new DigitKey("0", ")", 0, 20, Key.D0);
                KeyboardButtons[Key.OemMinus] = new SpecialCharKey("-", "_", 0, 22, 2, Key.OemMinus);
                KeyboardButtons[Key.OemPlus] = new SpecialCharKey("=", "+", 0, 24, 2, Key.OemPlus);
                KeyboardButtons[Key.Back] = new ControlKey("Backspace", 0, 26, 4, Key.Back);
                KeyboardButtons[Key.Tab] = new ControlKey("Tab", 1, 0, 3, Key.Tab);
                KeyboardButtons[Key.Q] = new LetterKey("Q", 1, 3, Key.Q);
                KeyboardButtons[Key.W] = new LetterKey("W", 1, 5, Key.W);
                KeyboardButtons[Key.E] = new LetterKey("E", 1, 7, Key.E);
                KeyboardButtons[Key.R] = new LetterKey("R", 1, 9, Key.R);
                KeyboardButtons[Key.T] = new LetterKey("T", 1, 11, Key.T);
                KeyboardButtons[Key.Y] = new LetterKey("Y", 1, 13, Key.Y);
                KeyboardButtons[Key.U] = new LetterKey("U", 1, 15, Key.U);
                KeyboardButtons[Key.I] = new LetterKey("I", 1, 17, Key.I);
                KeyboardButtons[Key.O] = new LetterKey("O", 1, 19, Key.O);
                KeyboardButtons[Key.P] = new LetterKey("p", 1, 21, Key.P);
                KeyboardButtons[Key.OemOpenBrackets] = new SpecialCharKey("[", "{", 1, 23, 2, Key.OemOpenBrackets);
                KeyboardButtons[Key.OemCloseBrackets] = new SpecialCharKey("]", "}", 1, 25, 2, Key.OemCloseBrackets);
                KeyboardButtons[Key.Oem5] = new SpecialCharKey("\\", "|", 1, 27, 3, Key.Oem5);
                KeyboardButtons[Key.CapsLock] = new ControlKey("Caps Lock", 2, 0, 4, Key.CapsLock);
                KeyboardButtons[Key.A] = new LetterKey("A", 2, 4, Key.A);
                KeyboardButtons[Key.S] = new LetterKey("S", 2, 6, Key.S);
                KeyboardButtons[Key.D] = new LetterKey("D", 2, 8, Key.D);
                KeyboardButtons[Key.F] = new LetterKey("F", 2, 10, Key.F);
                KeyboardButtons[Key.G] = new LetterKey("G", 2, 12, Key.G);
                KeyboardButtons[Key.H] = new LetterKey("H", 2, 14, Key.H);
                KeyboardButtons[Key.J] = new LetterKey("J", 2, 16, Key.J);
                KeyboardButtons[Key.K] = new LetterKey("K", 2, 18, Key.K);
                KeyboardButtons[Key.L] = new LetterKey("L", 2, 20, Key.L);
                KeyboardButtons[Key.OemSemicolon] = new SpecialCharKey(";", ":", 2, 22, 2, Key.OemSemicolon);
                KeyboardButtons[Key.OemQuotes] = new SpecialCharKey("'", "\"", 2, 24, 2, Key.OemQuotes);
                KeyboardButtons[Key.Enter] = new ControlKey("Enter", 2, 26, 4, Key.Enter);
                KeyboardButtons[Key.LeftShift] = new ControlKey("Shift", 3, 0, 5, Key.LeftShift);
                KeyboardButtons[Key.Z] = new LetterKey("Z", 3, 5, Key.Z);
                KeyboardButtons[Key.X] = new LetterKey("X", 3, 7, Key.X);
                KeyboardButtons[Key.C] = new LetterKey("C", 3, 9, Key.C);
                KeyboardButtons[Key.V] = new LetterKey("V", 3, 11, Key.V);
                KeyboardButtons[Key.B] = new LetterKey("B", 3, 13, Key.B);
                KeyboardButtons[Key.N] = new LetterKey("N", 3, 15, Key.N);
                KeyboardButtons[Key.M] = new LetterKey("M", 3, 17, Key.M);
                KeyboardButtons[Key.OemComma] = new SpecialCharKey(",", "<", 3, 19, 2, Key.OemComma);
                KeyboardButtons[Key.OemPeriod] = new SpecialCharKey(".", ">", 3, 21, 2, Key.OemPeriod);
                KeyboardButtons[Key.OemQuestion] = new SpecialCharKey("/", "?", 3, 23, 2, Key.OemQuestion);
                KeyboardButtons[Key.RightShift] = new ControlKey("Shift", 3, 25, 5, Key.RightShift);
                KeyboardButtons[Key.LeftCtrl] = new ControlKey("Ctrl", 4, 0, 3, Key.LeftCtrl);
                KeyboardButtons[Key.LWin] = new ControlKey("Win", 4, 3, 3, Key.LWin);
                KeyboardButtons[Key.LeftAlt] = new ControlKey("Alt", 4, 6, 3, Key.LeftAlt);
                KeyboardButtons[Key.Space] = new Space(4, 9, 12, Key.Space);
                KeyboardButtons[Key.RightAlt] = new ControlKey("Alt", 4, 21, 3, Key.RightAlt);
                KeyboardButtons[Key.RWin] = new ControlKey("Win", 4, 24, 3, Key.RWin);
                KeyboardButtons[Key.RightCtrl] = new ControlKey("Ctrl", 4, 27, 3, Key.RightCtrl);

                foreach (KeyButton keyboardButton in KeyboardButtons.Values)
                    KeyboardGrid.Children.Add(keyboardButton.KeyGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void sliderDifficulty_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int diff = (int)Math.Round(sliderDifficulty.Value);
            tbDifficulty.Text = diff.ToString();
            if (diff > 0 && diff <= 25)
                sliderDifficulty.Background = Brushes.LightSeaGreen;
            else if (diff > 25 && diff <= 35)
                sliderDifficulty.Background = Brushes.DarkKhaki;
            else
                sliderDifficulty.Background = Brushes.IndianRed;
        }
        private string GenerateText(int difficulty)
        {
            try
            {
                List<KeyButton> charsForTextGeneration = KeyboardButtons.Values.OfType<LetterKey>().ToList<KeyButton>();

                if (difficulty > 25)
                    charsForTextGeneration.AddRange(KeyboardButtons.Values.OfType<DigitKey>().ToList<KeyButton>());
                if (difficulty > 35)
                    charsForTextGeneration.AddRange(KeyboardButtons.Values.OfType<SpecialCharKey>().ToList<KeyButton>());

                List<char> randomChars = new List<char>();
                int randomIndex;

                for (int i = 0; i < difficulty; ++i)
                {
                    randomIndex = rand.Next(charsForTextGeneration.Count);
                    randomChars.Add(charsForTextGeneration.ElementAt(randomIndex).LowerValue[0]);
                    if (cbCaseSensitive.IsChecked == true)
                        randomChars.Add(charsForTextGeneration.ElementAt(randomIndex).UpperValue[0]);
                }

                for (int i = 5; i <= difficulty; i += 5)
                {
                    randomChars.Add(' ');
                    if (cbCaseSensitive.IsChecked == true)
                        randomChars.Add(' ');
                }

                string result = "";
                if (difficulty > 35)
                    for (int i = 0; i < 100; ++i)
                        result += randomChars[rand.Next(randomChars.Count)];
                else
                    for (int i = 0; i < 70; ++i)
                        result += randomChars[rand.Next(randomChars.Count)];
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }


        private void mainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Key key = (e.Key == Key.System) ? e.SystemKey : e.Key;
                if (!KeyboardButtons.ContainsKey(key))
                    return;

                //KeyboardButtons[key].KeyGrid.Effect = new DropShadowEffect();
                KeyboardButtons[key].KeyGrid.Background = new SolidColorBrush(Color.FromRgb(15, 76, 117));

                if (e.Key == Key.LeftShift || e.Key == Key.RightShift || e.Key == Key.CapsLock)
                {
                    UpdateKeyboard();
                    //KeyboardButtons[e.Key].KeyGrid.Effect = new DropShadowEffect();
                    KeyboardButtons[e.Key].KeyGrid.Background = new SolidColorBrush(Color.FromRgb(15, 76, 117));

                    if (Keyboard.IsKeyToggled(Key.CapsLock))
                        //KeyboardButtons[Key.CapsLock].KeyGrid.Effect = new DropShadowEffect();
                        KeyboardButtons[Key.CapsLock].KeyGrid.Background = new SolidColorBrush(Color.FromRgb(15, 76, 117));
                    return;
                }

                if (btnStart.IsEnabled)
                    return;

                if (e.Key == Key.Back)
                {
                    if (tbTypedText.Text.Length > 0)
                    {
                        tbTypedText.Text = tbTypedText.Text.Remove(tbTypedText.Text.Length - 1);
                        if (correctlyTypedTextLength >= tbTypedText.Text.Length)
                        {
                            correctlyTypedTextLength = tbTypedText.Text.Length;
                            tbTypedText.Foreground = new SolidColorBrush(Colors.Black);
                        }
                        tbTypedText.Select(0, correctlyTypedTextLength);
                    }
                    return;
                }

                if (KeyboardButtons[key] is ControlKey)
                    return;

                if (e.Key == Key.Space)
                    tbTypedText.AppendText(" ");
                else if (KeyboardButtons[e.Key] is LetterKey || KeyboardButtons[e.Key] is SpecialCharKey || KeyboardButtons[e.Key] is DigitKey)
                    tbTypedText.AppendText(KeyboardButtons[e.Key].Content.Text);

                if (tbGeneratedText.Text[correctlyTypedTextLength] == tbTypedText.Text[correctlyTypedTextLength])
                    correctlyTypedTextLength++;

                if (tbTypedText.Text.Length <= tbGeneratedText.Text.Length)
                    if (tbGeneratedText.Text[tbTypedText.Text.Length - 1] != tbTypedText.Text[tbTypedText.Text.Length - 1])
                        fails++;

                if (correctlyTypedTextLength == tbTypedText.Text.Length)
                    tbTypedText.Foreground = new SolidColorBrush(Colors.Black);
                else
                    tbTypedText.Foreground = new SolidColorBrush(Colors.Red);


                tbFails.Text = fails.ToString();
                tbSpeed.Text = Math.Round(tbTypedText.Text.Length / (DateTime.Now - startTime).TotalMinutes).ToString();

                tbTypedText.Select(0, correctlyTypedTextLength);

                if (correctlyTypedTextLength == tbGeneratedText.Text.Length)
                {
                    MessageBox.Show($"You have successfully typed the text\nFails: {fails}", "Finished", MessageBoxButton.OK, MessageBoxImage.Information);
                    Stop();
                    UpdateKeyboard();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void mainWindow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!KeyboardButtons.ContainsKey(e.Key == Key.System ? e.SystemKey : e.Key))
                    return;

                //KeyboardButtons[e.Key == Key.System ? e.SystemKey : e.Key].KeyGrid.Effect = null;
                KeyboardButtons[e.Key].KeyGrid.Background = Brushes.Transparent;

                if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
                    UpdateKeyboard();

                if (Keyboard.IsKeyToggled(Key.CapsLock))
                    //KeyboardButtons[Key.CapsLock].KeyGrid.Effect = new DropShadowEffect();
                    KeyboardButtons[Key.CapsLock].KeyGrid.Background = Brushes.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdateKeyboard()
        {
            try
            {
                bool shiftIsOn = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
                bool capsIsOn = Keyboard.IsKeyToggled(Key.CapsLock);
                foreach (KeyButton keyboardButton in KeyboardButtons.Values)
                {
                    keyboardButton.UpdateValue(shiftIsOn, capsIsOn);
                    keyboardButton.KeyGrid.Effect = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
