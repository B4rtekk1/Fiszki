namespace Fiszki
{
    public partial class Form1 : Form
    {
        Button flashcardButton;
        public Form1()
        {
            InitializeComponent();
            Menu();
        }
        private void Menu()
        {
            this.Size = new Size(200, 200);
            flashcardButton = new Button();
            flashcardButton.Text = "Add flashcards";
            flashcardButton.AutoSize = true;
            flashcardButton.Location = new Point(40, 50);
            flashcardButton.Click += FlashcardButton_Click;
            this.Controls.Add(flashcardButton);
        }

        private void FlashcardButton_Click(object? sender, EventArgs e)
        {
            CreateFlashcards createFlashcards = new CreateFlashcards();
            createFlashcards.Show();
            this.Hide();
        }
    }
}
