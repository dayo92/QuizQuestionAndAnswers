using System;

namespace QuizQuestionAndAnswers
{
    public class Constants
    {
      
        public const string EXIT_OPTION = "exit";
        public const string PATH = "../../questionAndAnswerDoc.xml";
        
        public const char YES_OPTION = 'y';
        public const char NO_OPTION = 'n';
        
        public const int MIN_OPTION = 1;
        public const int MAX_OPTION = 4;
        public const int EXIT_GAME = 3;
        public const int PLAY_GAME = 1;
        public const int CREATE_MODIFY_QUIZ_MODE = 2;
        
        public static readonly  Random RandomGenerator = new Random();
        
    }
}