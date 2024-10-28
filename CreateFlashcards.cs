using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Fiszki
{
    public class CreateFlashcards : Form
    {
        private Label wordLabel;
        private Label wordTranstaledLabel;
        private Label categoryLabel;
        private TextBox wordTextBox;
        private TextBox wordTranstaledTextBox;
        private TextBox categoryTextBox;
        private Button addButton;
        private string placeHolder = "Write here...";
        public string filePath = "flashcards.json";

        public CreateFlashcards()
        {
            GenerateMenu();
        }

        void GenerateMenu()
        {
            // Zapis przykładowych fiszek przy pierwszym uruchomieniu
            if (!File.Exists(filePath))
            {
                var flashcards = new List<GetWords>
                {
                    new GetWords { Word = "Hello", Translation = "Cześć", Category = "Culture" },
                    new GetWords { Word = "Goodbye", Translation = "Do widzenia", Category = "Culture" }
                };
                string json = JsonConvert.SerializeObject(flashcards, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }

            this.Text = "Fiszki";
            this.Size = new Size(500, 250);

            // Label: New phrase
            wordLabel = new Label();
            wordLabel.AutoSize = true;
            wordLabel.Text = "New phrase";
            wordLabel.Left = 50;
            this.Controls.Add(wordLabel);

            // Label: Translated
            wordTranstaledLabel = new Label();
            wordTranstaledLabel.AutoSize = true;
            wordTranstaledLabel.Text = "Translated";
            wordTranstaledLabel.Left = 200;
            this.Controls.Add(wordTranstaledLabel);

            // Label: Category
            categoryLabel = new Label();
            categoryLabel.AutoSize = true;
            categoryLabel.Text = "Category";
            categoryLabel.Left = 350;
            this.Controls.Add(categoryLabel);

            // TextBox: New phrase
            wordTextBox = new TextBox();
            wordTextBox.Size = new Size(100, 40);
            wordTextBox.Left = 45;
            wordTextBox.Top = 25;
            wordTextBox.Text = placeHolder;
            wordTextBox.ForeColor = Color.Gray;
            wordTextBox.Enter += WordTextBox_Enter;
            wordTextBox.Leave += WordTextBox_Leave;
            this.Controls.Add(wordTextBox);

            // TextBox: Translated
            wordTranstaledTextBox = new TextBox();
            wordTranstaledTextBox.Size = new Size(100, 40);
            wordTranstaledTextBox.Left = 200;
            wordTranstaledTextBox.Top = 25;
            wordTranstaledTextBox.Text = placeHolder;
            wordTranstaledTextBox.ForeColor = Color.Gray;
            wordTranstaledTextBox.Enter += WordTranstaledTextBox_Enter;
            wordTranstaledTextBox.Leave += WordTranstaledTextBox_Leave;
            this.Controls.Add(wordTranstaledTextBox);

            // TextBox: Category
            categoryTextBox = new TextBox();
            categoryTextBox.Size = new Size(100, 40);
            categoryTextBox.Left = 350;
            categoryTextBox.Top = 25;
            categoryTextBox.Text = placeHolder;
            categoryTextBox.ForeColor = Color.Gray;
            categoryTextBox.Enter += CategoryTextBox_Enter;
            categoryTextBox.Leave += CategoryTextBox_Leave;
            this.Controls.Add(categoryTextBox);

            // Button: Add
            addButton = new Button();
            addButton.Size = new Size(100, 40);
            addButton.Text = "Add";
            addButton.Top = 80;
            addButton.Left = 200;
            addButton.Click += AddButton_Click;
            this.Controls.Add(addButton);

            this.FormClosing += CreateFlashcards_FormClosing;
        }

        private void AddButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(wordTextBox.Text) || wordTextBox.Text == placeHolder ||
                string.IsNullOrWhiteSpace(wordTranstaledTextBox.Text) || wordTranstaledTextBox.Text == placeHolder ||
                string.IsNullOrWhiteSpace(categoryTextBox.Text) || categoryTextBox.Text == placeHolder)
            {
                MessageBox.Show("Please fill in all fields before adding a flashcard.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<GetWords> flashcards;
            try
            {
                string json = File.Exists(filePath) ? File.ReadAllText(filePath) : "[]";
                flashcards = JsonConvert.DeserializeObject<List<GetWords>>(json) ?? new List<GetWords>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading JSON file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool exists = flashcards.Any(f =>
        f.Word.Equals(wordTextBox.Text, StringComparison.OrdinalIgnoreCase) &&
        f.Translation.Equals(wordTranstaledTextBox.Text, StringComparison.OrdinalIgnoreCase) &&
        f.Category.Equals(categoryTextBox.Text, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                MessageBox.Show("This flashcard already exists in the database.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            flashcards.Add(new GetWords
            {
                Word = wordTextBox.Text,
                Translation = wordTranstaledTextBox.Text,
                Category = categoryTextBox.Text
            });

            try
            {
                string updatedJson = JsonConvert.SerializeObject(flashcards, Formatting.Indented);
                File.WriteAllText(filePath, updatedJson);
                MessageBox.Show($"Flashcard added to {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving to JSON file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateFlashcards_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void WordTranstaledTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(wordTranstaledTextBox.Text))
            {
                wordTranstaledTextBox.Text = placeHolder;
                wordTranstaledTextBox.ForeColor = Color.Gray;
            }
        }

        private void WordTranstaledTextBox_Enter(object? sender, EventArgs e)
        {
            if (wordTranstaledTextBox.Text == placeHolder)
            {
                wordTranstaledTextBox.Text = "";
                wordTranstaledTextBox.ForeColor = Color.Black;
            }
        }

        private void WordTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(wordTextBox.Text))
            {
                wordTextBox.Text = placeHolder;
                wordTextBox.ForeColor = Color.Gray;
            }
        }

        private void WordTextBox_Enter(object? sender, EventArgs e)
        {
            if (wordTextBox.Text == placeHolder)
            {
                wordTextBox.Text = "";
                wordTextBox.ForeColor = Color.Black;
            }
        }

        private void CategoryTextBox_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(categoryTextBox.Text))
            {
                categoryTextBox.Text = placeHolder;
                categoryTextBox.ForeColor = Color.Gray;
            }
        }

        private void CategoryTextBox_Enter(object? sender, EventArgs e)
        {
            if (categoryTextBox.Text == placeHolder)
            {
                categoryTextBox.Text = "";
                categoryTextBox.ForeColor = Color.Black;
            }
        }
    }
}
