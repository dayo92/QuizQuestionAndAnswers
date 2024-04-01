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
        
        public static void PlayGame(List<QuizQuestions> questionList)
        {
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
        
        public static QuizQuestions GetQuestionFromPlayer()
        {

            string questionText = GetPlayerQuestion().ToLower();

            if (questionText == Program.EXIT)
            {
                return null;
            }

            List<string> playerAnswers = new List<string>();


            int correctIndex = 0;
            
            for (int i = 0; i < 4; i++)
            {
                PrintAnswerNumber(i + 1);
                
                string choice = GetPlayerQuestion().ToLower();
                
                playerAnswers.Add(choice);

                PrintCorrectAnswerQuestionMessage();
                
                if (GetPlayerQuestion().ToLower()[0] == Program.YES_CHAR)
                {
                    correctIndex = i;
                }
            }
            

            return new QuizQuestions { PlayerQuestion = questionText, Answers = playerAnswers, CorrectIndex = correctIndex };
        }
        
        public static void PrintPlayerOptons()
        {
            Console.WriteLine("Player Optons:");
            Console.WriteLine("1. Load questions from file");
            Console.WriteLine("2. Create new questions");
            Console.Write("Enter your choice: ");
        }
        
        public static int GetMenuChoice()
        {
            int choice;
            bool validInput = false;

            do
            {
                string userInput = Console.ReadLine();
                validInput = int.TryParse(userInput, out choice) && (choice == 1 || choice == 2);

                if (!validInput)
                {
                    Console.WriteLine("Invalid input. Please enter 1 or 2.");
                }
            } while (!validInput);

            return choice;
        }
        
        public static void InvalidChoice()
        {
            Console.WriteLine("Invalid choice.");
        }
        
    }
}