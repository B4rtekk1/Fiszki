using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Threading.Tasks;

namespace Fiszki
{
    public class FlashcardMode : Form
    {
        private List<GetWords> flashcards;
        private Button wordButton;
        private TextBox translationTextBox;
        private int currentIndex;
        private bool isSpacePressed = false;


        public FlashcardMode(List<GetWords> flashcards)
        {
            this.flashcards = flashcards;
            currentIndex = 0;
            InitializeComponents();
            ShowCurrentFlashcard();
        }

        private void InitializeComponents()
        {
            this.Text = "Flashcard Mode";
            this.Size = new Size(400, 300);
            this.FormClosing += FlashcardMode_FormClosing;

            wordButton = new Button
            {
                Location = new Point(100, 50),
                Size = new Size(200, 200),
                Font = new Font("Arial", 22, FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                
            };
            wordButton.MouseUp += WordButton_MouseUp;
            wordButton.KeyDown += WordButton_KeyDown;
            wordButton.KeyUp += WordButton_KeyUp;
            this.Controls.Add(wordButton);


        }

        private void WordButton_MouseUp(object? sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && e.Button == MouseButtons.Left)
            {
                if (wordButton.Text == flashcards[currentIndex].Word)
                {
                    wordButton.Text = flashcards[currentIndex].Translation;
                }
                else if (wordButton.Text == flashcards[currentIndex].Translation)
                {
                    wordButton.Text = flashcards[currentIndex].Word;
                }
            }
        }

        private void WordButton_KeyUp(object? sender, KeyEventArgs e)
        {
            isSpacePressed = false;
        }

        private void WordButton_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !isSpacePressed)
            {
                currentIndex++;
                ShowCurrentFlashcard();
                isSpacePressed = true;
            }
        }

        private void FlashcardMode_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ShowCurrentFlashcard()
        {
            if (currentIndex < flashcards.Count)
            {
                wordButton.Text = flashcards[currentIndex].Word;
            }
            else
            {
                MessageBox.Show("Koniec fiszek!");
                Application.Exit(); // Zakończ program po przejściu przez wszystkie fiszki
            }
        }

       
    }
}
