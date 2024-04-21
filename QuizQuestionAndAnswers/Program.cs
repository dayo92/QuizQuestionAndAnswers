using System;
using System.Collections.Generic;

namespace QuizQuestionAndAnswers
{
    internal class Program
    {
        public const int PLAY_GAME = 1;
        public const int CREATE_MODIFY_QUIZ_MODE = 2;

        public static void Main(string[] args)
        {
            UIMethods.PrintQuizTitle();
            bool continuePlaying = true;

            while (continuePlaying)
            {
                int choice = UIMethods.PrintPlayerOptions();

                switch (choice)
                {
                    case PLAY_GAME:
                        List<QuizQuestionAndAnswers> questionList = Logic.DeserializeQuestions();
                        if (questionList != null)
                        {
                            UIMethods.PlayGame(questionList);
                        }
                        else
                        {
                            UIMethods.PrintNoQuizMessage();
                            if (UIMethods.AskToCreateNewQuiz())
                            {
                                UIMethods.AddQuestionsLoop();
                            }
                        }
                        break;
                    case CREATE_MODIFY_QUIZ_MODE:
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