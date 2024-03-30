using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizQuestionAndAnswers
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            UIMethods.PrintQuizTitle();

            const string EXIT = "exit";
            const char YES_CHAR = 'y';
            
            bool gettingQuestionAndAnswers = true;
            

            List<QuizQuestions> questionList = new List<QuizQuestions>();
            
            

            UIMethods.PrintPromptForQuestionAndAnswer();

            while (gettingQuestionAndAnswers)
            {
                UIMethods.PrintPromptQuestionOrExit();

                string questionText = UIMethods.GetPlayerQuestion().ToLower();

                if (questionText == EXIT)
                {
                    gettingQuestionAndAnswers = false;
                    continue;
                }

                List<string> playeranswers = new List<string>();

                int correctIndex = 0;

                UIMethods.PrintAnswersForQuestionMessage();

                for (int i = 0; i < 4; i++)
                {
                    UIMethods.PrintAnswerNumber(i + 1);

                    string choice = UIMethods.GetPlayerQuestion().ToLower();

                    playeranswers.Add(choice);

                    UIMethods.PrintCorrectAnswerQuestionMessage();

                    if (UIMethods.GetPlayerQuestion().ToLower()[0] == YES_CHAR)
                    {
                        correctIndex = i;
                    }
                }

                questionList.Add(new QuizQuestions { PlayerQuestion = questionText, Answers = playeranswers, CorrectIndex = correctIndex });
                
                
                
            }

            Logic.SerializerQuestions(questionList);

            UIMethods.PlayGame();
        }

       
    }
}