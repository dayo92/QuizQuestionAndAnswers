using System;
using System.Collections.Generic;

namespace QuizQuestionAndAnswers
{
    internal class Program
    {
        

        public static void Main(string[] args)
        {
           
            List<QuizQuestionAndAnswers> existingQuestions = Logic.DeserializeQuestions();

            if (existingQuestions == null || existingQuestions.Count == 0)
            {
                UIMethods.PromtToCreateQuestions();
                UIMethods.AddQuestionsLoop(existingQuestions);
            }
            else
            {
                UIMethods.PrintQuizTitle();
                bool continuePlaying = true;

                while (continuePlaying)
                {
                    int choice = UIMethods.PrintPlayerOptions();

                    switch (choice)
                    {
                        case Constants.PLAY_GAME:
                            UIMethods.PlayGame(existingQuestions);
                            break;
                        case Constants.CREATE_MODIFY_QUIZ_MODE:
                            UIMethods.AddQuestionsLoop(existingQuestions);
                            break;
                        default:
                            continuePlaying = false;
                            break;
                    }
                }
            }
            
        }
        
        
        
        
    }
}