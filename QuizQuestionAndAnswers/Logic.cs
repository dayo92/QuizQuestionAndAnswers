using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizQuestionAndAnswers
{

    public class Logic
    {

        const string PATH = @"../../questionAndAnswerDoc.xml";

        public static void SerializerQuestions(List<QuizQuestionAndAnswers> Questions)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<QuizQuestionAndAnswers>));

            using (FileStream file = File.Create(PATH))
            {
                serializer.Serialize(file, Questions);
            }


        }


        public static List<QuizQuestionAndAnswers> DeserializeQuestions()
        {
            List<QuizQuestionAndAnswers> questions;
            XmlSerializer serializer = new XmlSerializer(typeof(List<QuizQuestionAndAnswers>));

            using (FileStream file = File.OpenRead(PATH))
            {
                questions = (List<QuizQuestionAndAnswers>)serializer.Deserialize(file);
            }

            return questions;
        }

        public static void ProcessUserChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    List<QuizQuestionAndAnswers> questionList = DeserializeQuestions();
                    
                    UIMethods.PlayGame(questionList);
                    
                    break;
                case 2:
                    UIMethods.PrintPromptQuestionOrExit();
                    
                    List<QuizQuestionAndAnswers> newQuestions = new List<QuizQuestionAndAnswers>();
                    
                    bool gettingQuestionAndAnswers = true;

                    while (gettingQuestionAndAnswers)
                    {
                        QuizQuestionAndAnswers question = UIMethods.GetQuestionFromPlayer();
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