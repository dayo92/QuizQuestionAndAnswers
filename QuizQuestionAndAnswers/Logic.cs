using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace QuizQuestionAndAnswers
{
    public class Logic
    {
        public static void SerializerQuestions(List<QuizQuestions> Questions)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<QuizQuestions>));
            var path = @"/Users/danielataiyero/Desktop/QuestionList.xml";
            
            using (FileStream file = File.Create(path))
            {
                Console.WriteLine("**TEST** Questions in logic: " + Questions);
                serializer.Serialize(file, Questions);
            }

           
        }
    }
}