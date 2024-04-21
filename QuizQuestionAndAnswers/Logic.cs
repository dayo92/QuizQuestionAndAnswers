using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizQuestionAndAnswers
{

    public class Logic
    {

        

        private static  XmlSerializer serializer = new XmlSerializer(typeof(List<QuizQuestionAndAnswers>));

        public static void SerializerQuestions(List<QuizQuestionAndAnswers> Questions)
        {
            using (FileStream file = File.Create(Constants.PATH))
            {
                serializer.Serialize(file, Questions);
            }
        }

        public static List<QuizQuestionAndAnswers> DeserializeQuestions()
        {
            List<QuizQuestionAndAnswers> questions;

            using (FileStream file = File.OpenRead(Constants.PATH))
            {
                questions = (List<QuizQuestionAndAnswers>)serializer.Deserialize(file);
            }

            return questions;
        }
        
        
        

        
        
    }
}