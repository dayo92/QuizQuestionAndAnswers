using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizQuestionAndAnswers
{

    public class Logic
    {

        const string PATH = "../../questionAndAnswerDoc.xml";

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
        
        
        

        
        
    }
}