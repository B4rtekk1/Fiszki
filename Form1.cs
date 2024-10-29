using System;
using System.Drawing;
using System.Windows.Forms;

namespace Fiszki
{
    public partial class Form1 : Form
    {
        Button addFlashcardsButton;
        Button flashcardsButton;
        Button writingModeButton;

        public Form1()
        {
            InitializeComponent();
            Menu();
        }

        private void Menu()
        {
            this.Size = new Size(600, 200);

            addFlashcardsButton = new Button();
            addFlashcardsButton.Text = "Add flashcards";
            addFlashcardsButton.AutoSize = true;
            addFlashcardsButton.Location = new Point(40, 50);
            addFlashcardsButton.Click += AddFlashcardsButton_Click;
            this.Controls.Add(addFlashcardsButton);

            flashcardsButton = new Button();
            flashcardsButton.Text = "Learn";
            flashcardsButton.AutoSize = true;
            flashcardsButton.Location = new Point(350, 50);
            flashcardsButton.Click += FlashcardsButton_Click;
            this.Controls.Add(flashcardsButton);
        }

        private void AddFlashcardsButton_Click(object? sender, EventArgs e)
        {
            CreateFlashcards createFlashcards = new CreateFlashcards();
            createFlashcards.Show();
            this.Hide();
        }

        private void FlashcardsButton_Click(object? sender, EventArgs e)
        {
            // Zmieniamy tutaj, aby poprawnie utworzyæ now¹ instancjê FlashcardsGame
            FlashcardsGame flashcardsGame = new FlashcardsGame();
            flashcardsGame.Show();
            this.Hide();
        }
    }
}
