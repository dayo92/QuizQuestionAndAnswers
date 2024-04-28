using System;
using System.Collections.Generic;

namespace QuizQuestionAndAnswers
{
    internal class Program
    {
        

        public static void Main(string[] args)
        {
           
                UIMethods.PrintQuizTitle();
                bool continuePlaying = true;

                while (continuePlaying)
                {
                    int choice = UIMethods.PrintPlayerOptions();

                    switch (choice)
                    {
                        case Constants.PLAY_GAME:
                            UIMethods.RunQuizLogic();
                            break;
                        case Constants.CREATE_MODIFY_QUIZ_MODE:
                            UIMethods.AddQuestionsLoop();
                            break;
                        default:
                            continuePlaying = false;
                            break;
                    }
                }
            
        }
    }
}