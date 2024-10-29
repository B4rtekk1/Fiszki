using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki
{
    public class LearningMode
    {
        string category;
        public LearningMode(string category) 
        {
            this.category = category;
            InitializeComponents();
        }
        private void InitializeComponents()
        {
            FlashcardsGame mode = new FlashcardsGame();
            if(mode.learningMode == "Writing")
            {

            }
            if(mode.learningMode == "flashcards")
            {

            }
        }
    }
}
