using System;
using System.Collections.Generic;

namespace QuizQuestionAndAnswers
{
    public class UIMethods
    {
        const string EXIT = "exit";
        private const int MIN_OPTION = 1;
        private const int MAX_OPTION = 4;
        private const int OPTION_ONE = 1;
        private const int OPTION_TWO = 2;

        private const int NO_QUESTION_LEFT = 0;
        public static void PrintQuizTitle()
        {
            Console.WriteLine("Quiz Questions ans Answers");
        }  
        
        
        public static void PrintPromptQuestionOrExit()
        {
            Console.Write($"Enter question (or '{EXIT}' to finish): ");
            
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
            Console.Write($"Enter your answer ({MIN_OPTION}-{MAX_OPTION}): ");
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
            Console.WriteLine($"Invalid input. Please enter a number between {MIN_OPTION} and {MAX_OPTION}.");
        }
        
        
        public static void PlayGame(List<QuizQuestionAndAnswers> questionList)
        {
            
            List<QuizQuestionAndAnswers> remainingQuestions = new List<QuizQuestionAndAnswers>(questionList);

            while (remainingQuestions.Count > NO_QUESTION_LEFT)
            {
                QuizQuestionAndAnswers randomQuestion = GetRandomQuestion(remainingQuestions);

                PresentQuestion(randomQuestion);

                int userChoice = GetPlayerChoice();

                CheckPlayerAnswer(userChoice, randomQuestion);

                remainingQuestions.Remove(randomQuestion);
            }
        }
        
        private static QuizQuestionAndAnswers GetRandomQuestion(List<QuizQuestionAndAnswers> questionList)
        {
            Random random = new Random();
            
            int randomIndex = random.Next(questionList.Count);
            
            return questionList[randomIndex];
        }
        
        
        private static void PresentQuestion(QuizQuestionAndAnswers randomQuestion)
        {
            PrintQuestion(randomQuestion.PlayerQuestion);
            
            for (int i = 0; i < randomQuestion.Answers.Count; i++)
            {
                PrintAnswerNumber(i + 1);
                PrintAnswer(randomQuestion.Answers[i]);
            }
        }
        
        
        private static int GetPlayerChoice()
        {
            int userChoice;
            bool validInput = false;
            
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
            
            return userChoice;
        }
        
        private static void CheckPlayerAnswer(int userChoice, QuizQuestionAndAnswers randomQuestion)
        {
            
            int playerScore = 0;
            
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
        
        public static QuizQuestionAndAnswers GetQuestionFromPlayer()
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
                
                
                string userInput = GetPlayerQuestion().ToLower();
                
                if (!string.IsNullOrEmpty(userInput) && userInput[0] == Program.YES_CHAR)
                {
                    correctIndex = i;
                }
            }
            

            return new QuizQuestionAndAnswers { PlayerQuestion = questionText, Answers = playerAnswers, CorrectIndex = correctIndex };
        }
        
        public static void PrintPlayerOptions()
        {
            Console.WriteLine("Player Options:");
            Console.WriteLine($"{OPTION_ONE}. Load questions from file");
            Console.WriteLine($"{OPTION_TWO}. Create new questions");
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
                    Console.WriteLine($"Invalid input. Please enter {OPTION_ONE} or {OPTION_TWO}.");
                }
            } while (!validInput);

            return choice;
        }
        
        public static void InvalidChoice()
        {
            Console.WriteLine("Invalid choice.");
        }
        
        
        public static void ProcessUserChoice(int choice, List<QuizQuestionAndAnswers> questionList)
        {
            switch (choice)
            {
                case 1:
                    Logic.SerializerQuestions(questionList);
                    PlayGame(questionList);
                    break;
                case 2:
                    PrintPromptQuestionOrExit();
                    
                    List<QuizQuestionAndAnswers> newQuestions = new List<QuizQuestionAndAnswers>();
                    
                    bool gettingQuestionAndAnswers = true;

                    while (gettingQuestionAndAnswers)
                    {
                        QuizQuestionAndAnswers question = GetQuestionFromPlayer();
                        if (question == null)
                        {
                            gettingQuestionAndAnswers = false;
                        }
                        else
                        {
                            newQuestions.Add(question);
                        }
                    }

                    Logic.SerializerQuestions(newQuestions);
                    
                    PlayGame(newQuestions);
                    break;
                default:
                    InvalidChoice();
                    break;
            }
        }
        
    }
}