using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizQuestionAndAnswers
{

    public class Logic
    {

        const string PATH = @"../../readme";

        public static void SerializerQuestions(List<QuizQuestions> Questions)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<QuizQuestions>));

            using (FileStream file = File.Create(PATH))
            {
                serializer.Serialize(file, Questions);
            }


        }


        public static List<QuizQuestions> DeserializeQuestions()
        {
            List<QuizQuestions> questions;
            XmlSerializer serializer = new XmlSerializer(typeof(List<QuizQuestions>));

            using (FileStream file = File.OpenRead(PATH))
            {
                questions = (List<QuizQuestions>)serializer.Deserialize(file);
            }

            return questions;
        }

        public static void ProcessUserChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    List<QuizQuestions> questionList = DeserializeQuestions();
                    
                    UIMethods.PlayGame(questionList);
                    
                    break;
                case 2:
                    UIMethods.PrintPromptQuestionOrExit();
                    
                    List<QuizQuestions> newQuestions = new List<QuizQuestions>();
                    
                    bool gettingQuestionAndAnswers = true;

                    while (gettingQuestionAndAnswers)
                    {
                        QuizQuestions question = UIMethods.GetQuestionFromPlayer();
                        if (question == null)
                        {
                            gettingQuestionAndAnswers = false;
                        }
                        else
                        {
                            newQuestions.Add(question);
                        }
                    }

                    SerializerQuestions(newQuestions);
                    
                    UIMethods.PlayGame(newQuestions);
                    
                    break;
                default:
                    UIMethods.InvalidChoice();
                    break;
            }
        }
    }
}