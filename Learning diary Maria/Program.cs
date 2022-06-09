using System;
using System.IO;
using static Learning_diary_Maria.CreateTopic;

namespace Learning_diary_Maria
{
    class Program
    {
        static void Main(string[] args)
        {
            //PROBLEM with code = does not save the data properly.
            Console.WriteLine("If you'd like to create a new topic to your learning diary, write A.");
            Console.WriteLine("Or if you'd like to see the topics you have saved, write B.");

            string userInput = Console.ReadLine().ToLower();
            const string a = "a";
            const string b = "b";

            switch (userInput)
            {
                case a:
                    Topic.AddTopic();
                    break;

                case b:
                    PrintTopic();
                    break;
            }
        }


        static void PrintTopic()
        {

            {
                try

                {
                    string path = @"C:\Users\Maria T\source\repos\Learning diary\Topic.txt";
                    using (var sr = new StreamReader(path))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }

                catch (IOException e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
