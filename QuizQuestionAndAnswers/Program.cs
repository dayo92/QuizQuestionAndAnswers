﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizQuestionAndAnswers
{
    internal class Program
    {
        
        public const string EXIT = "exit";
        
        public const char YES_CHAR = 'y';
        
        public static void Main(string[] args)
        {
            UIMethods.PrintQuizTitle();
            
            bool gettingQuestionAndAnswers = true;
            

            List<QuizQuestionAndAnswers> questionList = new List<QuizQuestionAndAnswers>();
            
            

            UIMethods.PrintPromptQuestionOrExit();

            while (gettingQuestionAndAnswers)
            {
                
                QuizQuestionAndAnswers question = UIMethods.GetQuestionFromPlayer();
                
                if (question == null)
                {
                    gettingQuestionAndAnswers = false;
                }
                
                UIMethods.PrintPlayerOptons();
                
                int choice = UIMethods.GetMenuChoice();
                
                Logic.ProcessUserChoice(choice);
                
                questionList.Add(question);

                
            }
            
            

            Logic.SerializerQuestions(questionList);

            UIMethods.PlayGame(questionList);
        }

       
    }
}