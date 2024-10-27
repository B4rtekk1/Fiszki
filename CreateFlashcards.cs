using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki
{
    public class CreateFlashcards : Form
    {
        private Label wordLabel;
        private Label wordTranstaledLabel;
        private TextBox wordTextBox;
        private TextBox wordTranstaledTextBox;
        private Button addButton;
        private string placeHolder = "Write here...";
        public string filePath = "flashcards.json";
        public CreateFlashcards()
        {
            GenerateMenu();
        }
        void GenerateMenu()
        {
            var flashcards = new List<Test>
            {
                new Test { Word = "Hello", Translation = "Cześć" },
                new Test { Word = "Goodbye", Translation = "Do widzenia" }
            };
            string json = JsonConvert.SerializeObject(flashcards, Formatting.Indented);


            // Zapisz JSON do pliku
            File.WriteAllText(filePath, json);

            MessageBox.Show($"Plik JSON zapisany jako {filePath}");

            this.FormClosing += CreateFlashcards_FormClosing;
            this.Text = "Fiszki";
            this.Size = new Size(400, 200);

            //labele
            wordLabel = new Label();
            wordLabel.AutoSize = true;
            wordLabel.Text = "New phrase";
            wordLabel.Left = 50;
            this.Controls.Add(wordLabel);

            wordTranstaledLabel = new Label();
            wordTranstaledLabel.AutoSize = true;
            wordTranstaledLabel.Text = "Translated";
            wordTranstaledLabel.Left = 200;
            this.Controls.Add(wordTranstaledLabel);

            //textboxy

            wordTextBox = new TextBox();
            wordTextBox.Size = new Size(100, 40);
            wordTextBox.Left = 45;
            wordTextBox.Top = 25;

            wordTextBox.Text = placeHolder;
            wordTextBox.ForeColor = Color.Gray;
            wordTextBox.Enter += WordTextBox_Enter;
            wordTextBox.Leave += WordTextBox_Leave;
            this.Controls.Add(wordTextBox);

            wordTranstaledTextBox = new TextBox();
            wordTranstaledTextBox.Size = new Size(100, 40);
            wordTranstaledTextBox.Left = 200;
            wordTranstaledTextBox.Top = 25;
            wordTranstaledTextBox.Text = placeHolder;
            wordTranstaledTextBox.ForeColor = Color.Gray;
            wordTranstaledTextBox.Enter += WordTranstaledTextBox_Enter;
            wordTranstaledTextBox.Leave += WordTranstaledTextBox_Leave;
            this.Controls.Add(wordTranstaledTextBox);

            //button
            addButton = new Button();
            addButton.AutoSize = true;
            addButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            addButton.Text = "Add";
            addButton.Top = 75;
            addButton.Left = 150;
            addButton.Click += AddButton_Click;
            this.Controls.Add(addButton);

        }

        private void AddButton_Click(object? sender, EventArgs e)
        {
            List<Test> flashcards;
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                flashcards = JsonConvert.DeserializeObject<List<Test>>(json) ?? new List<Test>();
            }
            else
            {
                flashcards = new List<Test>(); // jeśli plik nie istnieje, twórz nową listę
            }
            flashcards.Add(new Test { Word = wordTextBox.Text, Translation = wordTranstaledTextBox.Text });

            // Serializacja i zapisanie zaktualizowanej listy do pliku
            string updatedJson = JsonConvert.SerializeObject(flashcards, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);

            MessageBox.Show($"Zaktualizowany plik JSON zapisany jako {filePath}");
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
    }
}

