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

            PlayGame(questionList);
        }

        private static void PlayGame(List<QuizQuestions> questionList)
        {
            Random random = new Random();
            int randomIndex = random.Next(questionList.Count);
            QuizQuestions randomQuestion = questionList[randomIndex];
            

            UIMethods.PrintQuestion(randomQuestion.PlayerQuestion);

            for (int i = 0; i < randomQuestion.Answers.Count; i++)
            {
                UIMethods.PrintAnswerNumber(i + 1);
                UIMethods.PrintAnswer(randomQuestion.Answers[i]);
            }
            
            
            int userChoice;
            bool validInput = false;
            int playerScore = 0;
            
            do
            { 
                UIMethods.PrintEnterAnswer();
                
                string userInput = UIMethods.GetPlayerQuestion();
                
                validInput = int.TryParse(userInput, out userChoice) && userChoice >= 1 && userChoice <= 4;
                
                if (!validInput)
                {
                    UIMethods.PrintInvalidInput();
                }
                
            } while (!validInput);

            if (userChoice - 1 == randomQuestion.CorrectIndex)
            {
                playerScore += 1;
                UIMethods.PrintScore(playerScore);
                UIMethods.PrintCorrectAnswer();
            }
            else
            {
                UIMethods.PrintIncorrectAnswer();
            }
        }
    }
}