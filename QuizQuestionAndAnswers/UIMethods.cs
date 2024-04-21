using System;
using System.Collections.Generic;

namespace QuizQuestionAndAnswers
{
    public class UIMethods
    {
        const string EXIT = "exit";
        
        private const char YES_CHAR = 'y';
        private const int MIN_OPTION = 1;
        private const int MAX_OPTION = 4;
        private const int NO_QUESTION_LEFT = 0;
        private const int EXIT_GAME = 3;
        public static void PrintQuizTitle()
        {
            Console.WriteLine("Quiz Questions ans Answers");
        }  
        
        
        public static string GetPlayerQuestion()
        {
            string answer = Console.ReadLine();

            return answer;
            
        }  
        
        public static void PrintPromptQuestionOrExit()
        {
            Console.Write($"Enter question (or '{EXIT}' to finish): ");
            
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
            PrintPromptQuestionOrExit();
            
            string questionText = GetPlayerQuestion().ToLower();

            if (questionText == EXIT)
            {
                return null;
            }

            List<string> playerAnswers = new List<string>();


            int correctIndex = 0;

            PrintAnswersForQuestionMessage();
            
            for (int i = 0; i < 4; i++)
            {
                PrintAnswerNumber(i + 1);
                
                string choice = GetPlayerQuestion().ToLower();
                
                playerAnswers.Add(choice);
                
                PrintCorrectAnswerQuestionMessage();
                
                
                string userInput = GetPlayerQuestion().ToLower();
                
                if (!string.IsNullOrEmpty(userInput) && userInput[0] == YES_CHAR)
                {
                    correctIndex = i;
                }
            }
            

            return new QuizQuestionAndAnswers { PlayerQuestion = questionText, Answers = playerAnswers, CorrectIndex = correctIndex };
        }
        
        
        public static int PrintPlayerOptions()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine($"{Program.PLAY_GAME}. Play a game");
            Console.WriteLine($"{Program.CREATE_MODIFY_QUIZ_MODE}. Create/Modify a quiz");
            Console.WriteLine($"{EXIT_GAME}. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < Program.PLAY_GAME || choice > EXIT_GAME)
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
                Console.Write("Enter your choice: ");
            }

            return choice;
        }
        
        
        public static bool AskToCreateNewQuiz()
        {
            Console.Write("Do you want to create a new quiz? (y/n): ");
            return Console.ReadLine()?.ToLower() == "y";
        }
        
        public static bool AskToOverrideExisting()
        {
            Console.Write("Do you want to override the existing file? (y/n): ");
            return Console.ReadLine()?.ToLower() == "y";
        }
        
        public static void AddQuestionsLoop()
        {
            List<QuizQuestionAndAnswers> newQuestions = new List<QuizQuestionAndAnswers>();


            bool gettingQuestionAndAnswers = true;

            while (gettingQuestionAndAnswers)
            {
                QuizQuestionAndAnswers question = GetQuestionFromPlayer();

                if (question == null)
                {
                    Console.WriteLine("Exiting the loop as the user entered 'exit'.");
                    gettingQuestionAndAnswers = false;
                }
                else
                {
                    Console.WriteLine("Adding the question to the list of new questions.");
                    newQuestions.Add(question);
                }
            }

            if (newQuestions.Count > 0)
            {
                bool overrideExisting = AskToOverrideExisting();

                if (overrideExisting)
                {
                    Logic.SerializerQuestions(newQuestions);
                    Console.WriteLine("New questions added successfully!");
                }
                else
                {
                    List<QuizQuestionAndAnswers> existingQuestions = Logic.DeserializeQuestions() ?? new List<QuizQuestionAndAnswers>();
                    existingQuestions.AddRange(newQuestions);
                    Logic.SerializerQuestions(existingQuestions);
                    Console.WriteLine("Questions added to the existing file successfully!");
                }
            }
            else
            {
                Console.WriteLine("No new questions entered. Exiting without making changes.");
            }
        }
        
        public static void PrintNoQuizMessage()
        {
            Console.WriteLine("No quiz available.");
        }
        
    }
}