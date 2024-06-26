using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizQuestionAndAnswers
{
    public class Logic
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(List<QuizQuestionAndAnswers>));
        
        

        public static void SerializeQuestions(List<QuizQuestionAndAnswers> Questions)
        {
            using (FileStream file = File.Create(Constants.PATH))
            {
                serializer.Serialize(file, Questions);
            }
        }

        public static List<QuizQuestionAndAnswers> DeserializeQuestions()
        {
            List<QuizQuestionAndAnswers> questions = new List<QuizQuestionAndAnswers>();

            if (File.Exists(Constants.PATH))
            {
                using (FileStream file = File.OpenRead(Constants.PATH))
                {
                    questions = (List<QuizQuestionAndAnswers>)serializer.Deserialize(file);
                }
            }

            return questions;
        }

        public static bool IsAnswerCorrect(int userChoice, QuizQuestionAndAnswers randomQuestion)
        {
            return userChoice - 1 == randomQuestion.CorrectIndex;
        }
        
        public static QuizQuestionAndAnswers GetRandomQuestion(List<QuizQuestionAndAnswers> questionList)
        {
            int randomIndex = Constants.RandomGenerator.Next(questionList.Count);
            
            return questionList[randomIndex];
        }
        
        public static bool TryParseValidUserChoice(string userInput, out int userChoice)
        {
            bool validInput = int.TryParse(userInput, out userChoice) 
                              && userChoice >= Constants.MIN_OPTION 
                              && userChoice <= Constants.MAX_OPTION;

            return validInput;
        }

        public static bool IsInputValidChar(char userInput)
        {
            return userInput == Constants.YES_OPTION || userInput == Constants.NO_OPTION;
        }

        public static bool IsYesAnswer(char userInput)
        {
            return userInput == Constants.YES_OPTION;
        }
        
        public static int CalUserScoreBasedOnAnswer(int userChoice, QuizQuestionAndAnswers randomQuestion,  int playerScore)
        {
            

            bool isCorrectAnswer = IsAnswerCorrect(userChoice, randomQuestion);
            int getScore = playerScore;

            if (isCorrectAnswer)
            {
                getScore++;
            }

            return getScore;

        }
        
        public static int GetPlayerChoice(string userInput)
        {
            int userChoice;
            bool validInput = false;
            
            do
            { 
        
                validInput = TryParseValidUserChoice(userInput, out userChoice);
        
                if (!validInput)
                {
                    userChoice = -1;
                }
            } while (!validInput);
    
            return userChoice;
        }
        
        
    }
}