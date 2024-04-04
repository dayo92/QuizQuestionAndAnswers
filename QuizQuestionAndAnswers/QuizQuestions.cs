using System.Collections.Generic;

namespace QuizQuestionAndAnswers
{
    public class QuizQuestionAndAnswers
    {
        
        public string PlayerQuestion { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectIndex { get; set; }
    }
}