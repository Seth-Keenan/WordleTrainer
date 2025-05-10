using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace WordleTrainer
{
    public partial class MainWindow : Window
    {
        private int currentAttempt = 1;
        private TextBox[] currentInputBoxes;
        private string targetWord;
        private const int MaxAttempts = 6;
        private const int WordLength = 5;
        private WordleGridManager gridManager;

        public MainWindow()
        {
            InitializeComponent();
            gridManager = new WordleGridManager(WordleGrid, this);
            InitializeGame();
        }

        private void InitializeGame()
        {
            gridManager.CreateInitialGrid();
            WireTextBoxEventHandlers();
            SetupCurrentInputBoxes();
            targetWord = WordSelector.GetRandomWord();
            targetWord = targetWord.ToUpper();
        }

        private void WireTextBoxEventHandlers()
        {
            var allTextBoxes = gridManager.GetAllTextBoxes();

            foreach (TextBox box in allTextBoxes)
            {
                // fixes duplicates issue
                box.TextChanged -= TextBox_TextChanged;
                box.KeyDown -= TextBox_KeyDown;
                box.PreviewKeyDown -= TextBox_PreviewKeyDown;

                // Add new handlers
                box.TextChanged += TextBox_TextChanged;
                box.KeyDown += TextBox_KeyDown;
                box.PreviewKeyDown += TextBox_PreviewKeyDown;
            }
        }

        private void SetupCurrentInputBoxes()
        {
            currentInputBoxes = gridManager.GetTextBoxesForAttempt(currentAttempt);

            if (currentInputBoxes.Length > 0)
            {
                currentInputBoxes[0].Focus();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;

            if (!currentTextBox.IsFocused) return;

            // Convert input to uppercase
            string text = currentTextBox.Text;
            if (!string.IsNullOrEmpty(text))
            {
                currentTextBox.Text = text.ToUpper();

                // Prevent cursor from going to start
                currentTextBox.SelectionStart = currentTextBox.Text.Length;

                if (currentTextBox.Text.Length == 1)
                {
                    MoveToNextTextBox(currentTextBox);
                }
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                TextBox currentTextBox = (TextBox)sender;

                if (string.IsNullOrEmpty(currentTextBox.Text))
                {
                    int currentIndex = Array.IndexOf(currentInputBoxes, currentTextBox);
                    if (currentIndex > 0)
                    {
                        e.Handled = true;
                        TextBox previousBox = currentInputBoxes[currentIndex - 1];
                        previousBox.Focus();
                        previousBox.SelectionStart = previousBox.Text.Length;
                    }
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox currentTextBox = (TextBox)sender;

            if (e.Key == Key.Enter)
            {
                ProcessGuess();
                e.Handled = true;
            }
            else if (e.Key == Key.Left)
            {
                MoveToPreviousTextBox(currentTextBox);
                e.Handled = true;
            }
            else if (e.Key == Key.Right || e.Key == Key.Tab)
            {
                MoveToNextTextBox(currentTextBox);
                e.Handled = true;
            }
        }

        private void MoveToNextTextBox(TextBox currentTextBox)
        {
            int currentIndex = Array.IndexOf(currentInputBoxes, currentTextBox);
            if (currentIndex >= 0 && currentIndex < currentInputBoxes.Length - 1)
            {
                currentInputBoxes[currentIndex + 1].Focus();
            }
        }

        private void MoveToPreviousTextBox(TextBox currentTextBox)
        {
            int currentIndex = Array.IndexOf(currentInputBoxes, currentTextBox);
            if (currentIndex > 0)
            {
                currentInputBoxes[currentIndex - 1].Focus();
            }
        }

        private void ProcessGuess()
        {
            if (currentInputBoxes.Length != WordLength)
            {
                MessageBox.Show($"Please enter a {WordLength}-letter word.");
                return;
            }

            string currentGuess = "";
            foreach (TextBox box in currentInputBoxes)
            {
                currentGuess += box.Text.ToUpper();
            }

            if (currentGuess.Length != WordLength)
            {
                MessageBox.Show($"Please enter a {WordLength}-letter word.");
                return;
            }

            DisplayGuessResult(currentGuess, currentAttempt);
            UpdateKeyboardLetters(currentGuess);
            gridManager.ConvertToLabels(currentAttempt);

            bool won = currentGuess == targetWord;
            bool gameOver = won || currentAttempt >= MaxAttempts;

            if (gameOver)
            {
                EndGame(won);
            }
            else
            {
                currentAttempt++;
                SetupCurrentInputBoxes();
            }
        }

        private void DisplayGuessResult(string guess, int attempt)
        {
            if (attempt < 1 || attempt > MaxAttempts || guess.Length != WordLength)
            {
                return;
            }

            TextBox[] textBoxes = gridManager.GetTextBoxesForAttempt(attempt);
            if (textBoxes.Length != WordLength) return;

            for (int i = 0; i < WordLength; i++)
            {
                TextBox textBox = textBoxes[i];
                if (textBox == null) continue;

                if (i < targetWord.Length && i < guess.Length && targetWord[i] == guess[i])
                {
                    textBox.Background = Brushes.LightGreen;
                }
                else if (targetWord.Contains(guess[i]))
                {
                    textBox.Background = Brushes.Yellow;
                }
                else
                {
                    textBox.Background = Brushes.Gray;
                }
                textBox.Foreground = Brushes.Black;
            }
        }

        private void UpdateKeyboardLetters(string guess)
        {
            if (string.IsNullOrEmpty(guess) || guess.Length != WordLength)
                return;

            for (int i = 0; i < guess.Length; i++)
            {
                char letter = guess[i];
                string keyName = $"Key{letter}";
                Label keyLabel = FindName(keyName) as Label;

                if (keyLabel != null)
                {
                    // If the letter is in the correct position, mark it LightGreen
                    if (i < targetWord.Length && targetWord[i] == letter)
                    {
                        keyLabel.Background = Brushes.LightGreen;
                        keyLabel.Foreground = Brushes.Black;
                    }
                    // If the letter is in the word but wrong position, mark it yellow
                    // (only if it's not already LightGreen)
                    else if (targetWord.Contains(letter) && keyLabel.Background != Brushes.LightGreen)
                    {
                        keyLabel.Background = Brushes.Yellow;
                        keyLabel.Foreground = Brushes.Black;
                    }
                    // If the letter is not in the word, mark it gray
                    // (only if it's not already LightGreen or yellow)
                    else if (keyLabel.Background != Brushes.LightGreen && keyLabel.Background != Brushes.Yellow)
                    {
                        keyLabel.Background = Brushes.Gray;
                        keyLabel.Foreground = Brushes.Black;
                    }
                }
            }
        }

        private void EndGame(bool won)
        {
            string message = won ? "Congratulations! You won!" : "Game over! You lost. The word was " + targetWord;
            MessageBox.Show(message);
            ResetGame();
        }

        private void ResetGame()
        {
            currentAttempt = 1;
            gridManager.ResetGrid();
            ResetKeyboard();
            WireTextBoxEventHandlers();
            SetupCurrentInputBoxes();
            targetWord = WordSelector.GetRandomWord();
            targetWord = targetWord.ToUpper();
        }

        private void ResetKeyboard()
        {
            // Reset all keyboard letter labels to their default style
            for (char c = 'A'; c <= 'Z'; c++)
            {
                string keyName = $"Key{c}";
                Label keyLabel = FindName(keyName) as Label;

                if (keyLabel != null)
                {
                    keyLabel.Background = Brushes.White;
                    keyLabel.Foreground = Brushes.Black;
                }
            }
        }

        public new object FindName(string name)
        {
            object result = base.FindName(name);
            if (result != null)
                return result;

            if (gridManager != null)
                return gridManager.FindName(name);

            return null;
        }
    }
}