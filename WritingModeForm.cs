using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Fiszki
{
    public class WritingModeForm : Form
    {
        private Label wordLabel;
        private TextBox translationTextBox;
        private Button checkButton;
        private Label resultLabel;

        private List<GetWords> flashcards;
        private GetWords currentWord;

        public WritingModeForm(List<GetWords> flashcards)
        {
            this.flashcards = flashcards;
            InitializeComponents();
            LoadNewWord();
        }

        private void InitializeComponents()
        {
            this.Text = "Writing Mode - Translate the word";
            this.Size = new Size(400, 200);
            this.FormClosing += WritingModeForm_FormClosing;

            wordLabel = new Label
            {
                Font = new Font("Arial", 14),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(wordLabel);

            translationTextBox = new TextBox
            {
                Location = new Point(20, 60),
                Width = 200
            };
            translationTextBox.KeyDown += TranslationTextBox_KeyDown;
            this.Controls.Add(translationTextBox);

            checkButton = new Button
            {
                Text = "Check",
                Location = new Point(230, 60),
                AutoSize = true
            };
            checkButton.Click += CheckButton_Click;
            this.Controls.Add(checkButton);

            resultLabel = new Label
            {
                Location = new Point(20, 100),
                AutoSize = true,
                ForeColor = Color.DarkBlue,
                Font = new Font("Arial", 12)
            };
            this.Controls.Add(resultLabel);
        }

        private void WritingModeForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void LoadNewWord()
        {
            // Losuj nowe słowo do przetłumaczenia
            Random rand = new Random();
            if (flashcards.Count == 0)
            {
                MessageBox.Show("You made it");
            }
            else
            {


                currentWord = flashcards[rand.Next(flashcards.Count)];
                wordLabel.Text = $"Translate: {currentWord.Word}";
                translationTextBox.Text = string.Empty;
                resultLabel.Text = string.Empty;
            }
        }

        private void CheckButton_Click(object? sender, EventArgs e)
        {
            CheckAnswer();
        }

        private void TranslationTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckAnswer();
                e.SuppressKeyPress = true;
            }
        }

        private async Task CheckAnswer()
        {
            if (translationTextBox.Text.Trim().Equals(currentWord.Translation, StringComparison.OrdinalIgnoreCase))
            {
                resultLabel.ForeColor = Color.Green;
                resultLabel.Text = "Correct!";
                flashcards.Remove(currentWord);
                await Task.Delay(1000);
            }
            else
            {
                resultLabel.ForeColor = Color.Red;
                resultLabel.Text = $"Incorrect. The correct translation is: {currentWord.Translation}";
                await Task.Delay(2000);
            }
            LoadNewWord();
        }
    }
}
