using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizQuestionAndAnswers
{
    internal class Program
    {
        
        public const string EXIT = "exit";
        public const char YES_CHAR = 'y';
        
        public static void Main(string[] args)
        {
            UIMethods.PrintQuizTitle();
            
            bool gettingQuestionAndAnswers = true;
            

            List<QuizQuestions> questionList = new List<QuizQuestions>();
            
            

            UIMethods.PrintPromptForQuestionAndAnswer();

            while (gettingQuestionAndAnswers)
            {
                
                QuizQuestions question = UIMethods.GetQuestionFromPlayer();
                
                if (question == null)
                {
                    gettingQuestionAndAnswers = false;
                }
                questionList.Add(question);

                
            }

            Logic.SerializerQuestions(questionList);

            UIMethods.PlayGame();
        }

       
    }
}