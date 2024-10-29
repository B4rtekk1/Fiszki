using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Fiszki
{
    public class FlashcardsGame : Form
    {
        private ComboBox categoryComboBox;
        private List<string> categories;
        private List<GetWords> flashcards;
        private Button writingModeButton;
        private Button flashcardsMode;
        public string learningMode;

        public FlashcardsGame()
        {
            categories = LoadCategoriesFromJson("flashcards.json");
            flashcards = LoadFlashcardsFromJson("flashcards.json"); // Załaduj fiszki
            InitializeComponents();
        }

        private List<string> LoadCategoriesFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var flashcards = JsonConvert.DeserializeObject<List<GetWords>>(json) ?? new List<GetWords>();
                return flashcards.Select(f => f.Category).Distinct().ToList();
            }

            return new List<string>();
        }

        private List<GetWords> LoadFlashcardsFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<GetWords>>(json) ?? new List<GetWords>();
            }
            return new List<GetWords>();
        }

        private void InitializeComponents()
        {
            this.Text = "Wybór kategorii";
            this.Size = new System.Drawing.Size(1000, 600);
            this.FormClosing += FlashcardsGame_FormClosing;

            categoryComboBox = new ComboBox
            {
                DataSource = categories,
                Location = new System.Drawing.Point(300, 200),
                Width = 200
            };
            this.Controls.Add(categoryComboBox);

            writingModeButton = new Button
            {
                Location = new System.Drawing.Point(300, 250),
                AutoSize = true,
                Text = "Writing mode"
            };
            writingModeButton.Click += WritingModeButton_Click;
            this.Controls.Add(writingModeButton);

            flashcardsMode = new Button
            {
                Location = new System.Drawing.Point(450, 250),
                AutoSize = true,
                Text = "Flashcards"
            };
            flashcardsMode.Click += FlashcardsMode_Click;
            this.Controls.Add(flashcardsMode);
        }

        private void WritingModeButton_Click(object? sender, EventArgs e)
        {
            string selectedCategory = categoryComboBox.SelectedItem?.ToString();
            List<GetWords> selectedFlashcards = flashcards
                .Where(f => f.Category == selectedCategory)
                .ToList();

            WritingModeForm writingModeForm = new WritingModeForm(selectedFlashcards);
            writingModeForm.Show();
            this.Hide();
        }

        private void FlashcardsMode_Click(object? sender, EventArgs e)
        {
            string selectedCategory = categoryComboBox.SelectedItem?.ToString();
            List<GetWords> selectedFlashcards = flashcards
                .Where(f => f.Category == selectedCategory)
                .ToList();

            // Uruchamiamy FlashcardMode z fiszkami danej kategorii
            FlashcardMode flashcardMode = new FlashcardMode(selectedFlashcards);
            flashcardMode.Show();
            this.Hide();
        }

        private void FlashcardsGame_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
