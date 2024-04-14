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
            List<QuizQuestionAndAnswers> questionList = new List<QuizQuestionAndAnswers>();
            
            bool gettingQuestionAndAnswers = true;

            while (gettingQuestionAndAnswers)
            {
                UIMethods.PrintPromptQuestionOrExit();
                QuizQuestionAndAnswers question = UIMethods.GetQuestionFromPlayer();

                if (question == null)
                {
                    UIMethods.PrintPlayerOptions();
                    int choice = UIMethods.GetMenuChoice();

                    UIMethods.ProcessUserChoice(choice, questionList);

                    gettingQuestionAndAnswers = false; 
                }
                else
                {
                    questionList.Add(question);
                }
            }
        }

       
    }
}