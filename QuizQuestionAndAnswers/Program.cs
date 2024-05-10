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
                UIMethods.AddQuestionsLoop(existingQuestions, Logic.IsInputValidChar,Logic.IsYesAnswer);
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
                            UIMethods.PlayGame(existingQuestions, () => Logic.GetPlayerChoice(UIMethods.GetPlayerQuestion()), (userChoice, question, score) => Logic.CalUserScoreBasedOnAnswer(userChoice, question, score));
                            break;
                        case Constants.CREATE_MODIFY_QUIZ_MODE:
                            List<QuizQuestionAndAnswers> newQuestions = UIMethods.AddQuestionsLoop(existingQuestions, Logic.IsInputValidChar, Logic.IsYesAnswer);
                            Logic.SerializeQuestions(newQuestions);
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