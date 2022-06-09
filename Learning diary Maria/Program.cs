using System;
using static Learning_diary_Maria.CreateTopic;

namespace Learning_diary_Maria
{
    class Program
    {
        static void Main(string[] args)
        {
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
                    Console.WriteLine("Failure.");
                    break;
            }
        }
    }
}
