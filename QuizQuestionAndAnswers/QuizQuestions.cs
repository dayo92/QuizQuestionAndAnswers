using System.Collections.Generic;

namespace QuizQuestionAndAnswers
{
    public class QuizQuestions
    {
        
        public string PlayerQuestions { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectIndex { get; set; }
    }
}