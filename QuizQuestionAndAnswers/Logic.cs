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
                Console.WriteLine("**TEST** Questions in logic: " + Questions);
                serializer.Serialize(file, Questions);
            }

           
        }
    }
}