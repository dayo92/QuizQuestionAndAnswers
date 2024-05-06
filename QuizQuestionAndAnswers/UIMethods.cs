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
        
        
        public static string GetPlayerQuestion()
        {
            string answer = Console.ReadLine();

            return answer;
            
        }  
        
        public static void PrintPromptQuestionOrExit()
        {
            Console.Write($"Enter question (or '{Constants.EXIT_OPTION}' to finish): ");
            
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
            Console.Write($"Enter your answer ({Constants.MIN_OPTION}-{Constants.MAX_OPTION}): ");
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
            Console.WriteLine($"Invalid input. Please enter a number between {Constants.MIN_OPTION} and {Constants.MAX_OPTION}.");
        }
        
        public static void PrintInvalidInputChar()
        {
            Console.WriteLine($"Invalid input. Please enter '{Constants.YES_OPTION}' for Yes or '{Constants.NO_OPTION}' for No.");        }
        
        
        public static void PlayGame(List<QuizQuestionAndAnswers> questionList)
        {
            List<QuizQuestionAndAnswers> remainingQuestions = new List<QuizQuestionAndAnswers>(questionList);
            int playerScore = 0;

            while (remainingQuestions.Count > Constants.NO_QUESTION_LEFT)
            {
                QuizQuestionAndAnswers randomQuestion = Logic.GetRandomQuestion(remainingQuestions);
                PresentQuestion(randomQuestion);
                int userChoice = GetPlayerChoice();
                playerScore = CalculateUsersScoreBasedOnAnswer(userChoice, randomQuestion, playerScore);
                remainingQuestions.Remove(randomQuestion);
                Console.WriteLine("points : " + playerScore);
            }
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
                
                validInput = int.TryParse(userInput, out userChoice) && userChoice >= Constants.MIN_OPTION && userChoice <= Constants.MAX_OPTION;
                
                if (!validInput)
                {
                    PrintInvalidInput();
                }
            } while (!validInput);
            
            return userChoice;
        }
        
        private static int CalculateUsersScoreBasedOnAnswer(int userChoice, QuizQuestionAndAnswers randomQuestion,  int playerScore)
        {
            

            bool isCorrectAnswer = Logic.IsAnswerCorrect(userChoice, randomQuestion);
            int getScore = playerScore;

            if (isCorrectAnswer)
            {
                getScore++;
                PrintScore(getScore);
                PrintCorrectAnswer();
            }
            else
            {
                PrintIncorrectAnswer();
            }

            return getScore;

        }
        
        public static QuizQuestionAndAnswers GetQuestionFromPlayer()
        {
            PrintPromptQuestionOrExit();

            string questionText = GetPlayerQuestion().ToLower();

            if (questionText == Constants.EXIT_OPTION)
            {
                return null;
            }

            List<string> playerAnswers = new List<string>();
            int correctIndex = -1;

            PrintAnswersForQuestionMessage();

            for (int i = 0; i < 4; i++)
            {
                PrintAnswerNumber(i + 1);

                string choice = GetPlayerQuestion().ToLower();
                playerAnswers.Add(choice);

                bool correctAnswerChosen = false;

                while (!correctAnswerChosen)
                {
                    PrintCorrectAnswerQuestionMessage();

                    char userInput = GetPlayerAnswer();

                    if ( userInput == Constants.YES_OPTION || userInput == Constants.NO_OPTION)
                    {
                        if (userInput == Constants.YES_OPTION)
                        {
                            if (correctIndex == -1)
                            {
                                correctIndex = i;
                            }
                            else
                            {
                                Console.WriteLine("Only one correct answer allowed per question. Please choose again.");
                                continue; 
                            }
                        }
                        correctAnswerChosen = true;
                    }
                    else
                    {
                        PrintInvalidInputChar();
                    }
                }
            }

            return new QuizQuestionAndAnswers { PlayerQuestion = questionText, Answers = playerAnswers, CorrectIndex = correctIndex };
        }
        
        
        public static int PrintPlayerOptions()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine($"{Constants.PLAY_GAME}. Play a game");
            Console.WriteLine($"{Constants.CREATE_MODIFY_QUIZ_MODE}. Create/Modify a quiz");
            Console.WriteLine($"{Constants.EXIT_GAME}. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < Constants.PLAY_GAME || choice > Constants.EXIT_GAME)
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
                Console.Write("Enter your choice: ");
            }

            return choice;
        }
        
        public static bool AskToOverrideExisting()
        {
            Console.Write($"Do you want to override the existing file? ({Constants.YES_OPTION}/{Constants.NO_OPTION}): ");
            return Console.ReadLine()?.ToLower() == Constants.YES_OPTION.ToString();
        }
        
        public static void AddQuestionsLoop(List<QuizQuestionAndAnswers> existingQuestions)
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

            if (newQuestions.Count > Constants.NO_QUESTION_LEFT)
            {
                bool overrideExisting = AskToOverrideExisting();

                if (overrideExisting)
                {
                    Logic.SerializeQuestions(newQuestions);
                    Console.WriteLine("New questions added successfully!");
                }
                else
                {
                    existingQuestions.AddRange(newQuestions);
                    Logic.SerializeQuestions(existingQuestions);
                    Console.WriteLine("Questions added to the existing file successfully!");
                }
            }
            else
            {
                Console.WriteLine("No new questions entered. Exiting without making changes.");
            }
        }
        
        public static void PromtToCreateQuestions()
        {
            Console.WriteLine("No quiz available. Please create questions first.");
        }
        
        
        public static char GetPlayerAnswer()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.WriteLine(keyInfo.KeyChar);
            return char.ToLower(keyInfo.KeyChar);
        }
        
    }
}