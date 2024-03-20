using System;

namespace QuizQuestionAndAnswers
{
    public class UIMethods
    {
        public static void PrintQuizTitle()
        {
            Console.WriteLine("Quiz Questions ans Answers");
        }  
        
        public static void PrintPromptForQuestionAndAnswer()
        {
            Console.WriteLine("Enter questions and their answers. Type 'exit' to finish.");
        }  
        
        public static void PrintPromptQuestionOrExit()
        {
            Console.Write("Enter question (or 'exit' to finish): ");
            
        }  
        
        public static string GetPlayerQuestion()
        {
            string answer = Console.ReadLine();

            return answer;
            
        }  
        
        public static void PrintAnswersForQuestionMessage()
        {
            Console.WriteLine("Enter answers for the question:");
            
        }  
        public static void PrintCorrectAnswerQuestionMessage()
        {
            Console.Write("Is this the correct answer? (y/n): ");
            
        }  
        
        
        
        
    }
}