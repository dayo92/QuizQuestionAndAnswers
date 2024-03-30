using System;
using System.Collections.Generic;

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
        public static void PrintQuestion(string question)
        {
            Console.WriteLine($"Question: {question}");
            
        }  
        public static void PrintAnswer( string answer)
        {
            Console.WriteLine($"Answer: {answer}");
        }
        public static void PrintCorrectAnswer()
        {
            Console.WriteLine("Correct!");
        }
        
        public static void PrintEnterAnswer()
        {
            Console.Write("Enter your answer (1-4): ");
        }
        
        public static void PrintIncorrectAnswer()
        {
            Console.WriteLine("Incorrect!");
        }
        public static void PrintAnswerNumber(int number)
        {
            Console.Write($"Answer {number}: ");
        }
        public static void PrintScore(int number)
        {
            Console.Write($"You scored {number} point. ");
        }
        
        public static void PrintInvalidInput()
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
        }
        
        public static void PlayGame()
        {
            List<QuizQuestions> questionList = Logic.DeserializeQuestions();
            Random random = new Random();
            int randomIndex = random.Next(questionList.Count);
            
            
            QuizQuestions randomQuestion = questionList[randomIndex];
            
            
            PrintQuestion(randomQuestion.PlayerQuestion);

            for (int i = 0; i < randomQuestion.Answers.Count; i++)
            {
                PrintAnswerNumber(i + 1);
                PrintAnswer(randomQuestion.Answers[i]);
            }
            
            
            int userChoice;
            bool validInput = false;
            int playerScore = 0;
            
            do
            { 
                PrintEnterAnswer();
                
                string userInput = GetPlayerQuestion();
                
                validInput = int.TryParse(userInput, out userChoice) && userChoice >= 1 && userChoice <= 4;
                
                if (!validInput)
                {
                    PrintInvalidInput();
                }
                
            } while (!validInput);

            if (userChoice - 1 == randomQuestion.CorrectIndex)
            {
                playerScore += 1;
                PrintScore(playerScore);
                PrintCorrectAnswer();
            }
            else
            {
                PrintIncorrectAnswer();
            }
        }
        
    }
}