using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using System.Collections.Generic;

namespace WordleTrainer
{
    public class WordleGridManager
    {
        private Grid wordleGrid;
        private const int MaxAttempts = 6;
        private const int WordLength = 5;
        private MainWindow mainWindow;
        private Dictionary<string, Control> nameToControlMap = new Dictionary<string, Control>();

        public WordleGridManager(Grid grid, MainWindow window)
        {
            wordleGrid = grid;
            mainWindow = window;

            // Initialize the keyboard controls dictionary
            InitializeKeyboardControls();
        }

        private void InitializeKeyboardControls()
        {
            // Add all keyboard letters to our dictionary
            for (char c = 'A'; c <= 'Z'; c++)
            {
                string keyName = $"Key{c}";
                Control keyControl = mainWindow.FindName(keyName) as Control;
                if (keyControl != null)
                {
                    nameToControlMap[keyName] = keyControl;
                }
            }
        }

        public void CreateInitialGrid()
        {
            var textBoxes = wordleGrid.Children.OfType<TextBox>().ToList();
            foreach (var tb in textBoxes)
            {
                wordleGrid.Children.Remove(tb);
            }

            // Remove only the grid controls from our dictionary
            var keysToRemove = nameToControlMap.Keys
                .Where(k => k.StartsWith("Attempt") && k.Contains("Letter"))
                .ToList();

            foreach (var key in keysToRemove)
            {
                nameToControlMap.Remove(key);
            }

            for (int row = 0; row < MaxAttempts; row++)
            {
                for (int col = 0; col < WordLength; col++)
                {
                    string elementName = $"Attempt{row + 1}Letter{col + 1}";
                    TextBox newTextBox = new TextBox
                    {
                        Style = (Style)mainWindow.FindResource("LetterBoxStyle"),
                        MaxLength = 1,
                        TextAlignment = TextAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontSize = 40
                    };

                    Grid.SetRow(newTextBox, row);
                    Grid.SetColumn(newTextBox, col);
                    wordleGrid.Children.Add(newTextBox);

                    // Register the control in our dictionary
                    nameToControlMap[elementName] = newTextBox;
                }
            }
        }

        public TextBox[] GetAllTextBoxes()
        {
            return wordleGrid.Children.OfType<TextBox>().ToArray();
        }

        public TextBox[] GetTextBoxesForAttempt(int attempt)
        {
            TextBox[] rowTextBoxes = new TextBox[WordLength];
            for (int i = 0; i < WordLength; i++)
            {
                string name = $"Attempt{attempt}Letter{i + 1}";
                if (nameToControlMap.TryGetValue(name, out Control control) && control is TextBox textBox)
                {
                    rowTextBoxes[i] = textBox;
                }
                else
                {
                    // Handle the case where the control is not found or is not a TextBox
                    rowTextBoxes[i] = null;
                }
            }
            return rowTextBoxes.Where(tb => tb != null).ToArray();
        }

        public void ConvertToLabels(int attempt)
        {
            if (attempt < 1 || attempt > MaxAttempts) return;

            string guess = "";
            TextBox[] textBoxes = GetTextBoxesForAttempt(attempt);
            if (textBoxes != null && textBoxes.Length == WordLength)
            {
                guess = string.Join("", textBoxes.Select(tb => tb.Text.ToUpper()));
            }
            else
            {
                return; // Cannot convert if TextBoxes are not found or incomplete
            }

            for (int col = 0; col < WordLength; col++)
            {
                string elementName = $"Attempt{attempt}Letter{col + 1}";

                if (nameToControlMap.TryGetValue(elementName, out Control control) && control is TextBox oldTextBox)
                {
                    Label newLabel = new Label
                    {
                        Style = (Style)mainWindow.FindResource("LabelBoxStyle"),
                        Content = guess[col],
                        Background = oldTextBox.Background,
                        Foreground = Brushes.Black,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center
                    };

                    Grid.SetRow(newLabel, attempt - 1);
                    Grid.SetColumn(newLabel, col);

                    wordleGrid.Children.Remove(oldTextBox);
                    wordleGrid.Children.Add(newLabel);

                    // Update our dictionary
                    nameToControlMap[elementName] = newLabel;
                }
            }
        }

        public void ResetGrid()
        {
            var textBoxes = wordleGrid.Children.OfType<TextBox>().ToList();
            foreach (var tb in textBoxes)
            {
                wordleGrid.Children.Remove(tb);
            }

            // Only clear the grid-related controls, not keyboard controls
            var keysToRemove = nameToControlMap.Keys
                .Where(k => k.StartsWith("Attempt") && k.Contains("Letter"))
                .ToList();

            foreach (var key in keysToRemove)
            {
                nameToControlMap.Remove(key);
            }

            CreateInitialGrid();
        }

        // Add a method to find a control by name in our dictionary
        public Control FindName(string name)
        {
            nameToControlMap.TryGetValue(name, out Control control);
            return control;
        }
    }
}