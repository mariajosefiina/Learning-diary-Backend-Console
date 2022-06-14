using System;
using System.IO;
using static Learning_diary_Maria.Topic;
using System.Collections.Generic;
using System.Linq;

namespace Learning_diary_Maria
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Topic> topicCollection = new List<Topic>();
            Console.WriteLine("If you'd like to create a new topic to your learning diary, write A.");
            Console.WriteLine("Or if you'd like to see the topics you have saved, write B.");
            Console.WriteLine("Or if you'd like to search for a topic based on its topic Id, write C.");

            string userInput = Console.ReadLine().ToLower();

            if (userInput == "a")

            {
                AddTopic();
                //foreach (Topic topic in topicCollection)
                //{
                //    Console.WriteLine(topic.Id);
                //}
            }

            else if (userInput == "b")
            {
                try

                {
                    string path = @"C:\Users\Maria T\source\repos\Learning diary Maria\Learning diary Maria\Topic.txt";
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

            else if (userInput == "c") //tulkitse tekstitiedoston sisältö topic-olioiksi -> txt. revi sieltä joka 8. rivi... lue tekstitiedosto arrayhyn -8 rivin välein..
            {
                string path = @"C:\Users\Maria T\source\repos\Learning diary Maria\Learning diary Maria\Topic.txt";

                string lines = File.ReadAllText(path);

                string [] lineArray = lines.Split(',');
                Topic testTopic = new Topic(Int32.Parse(lineArray[0]), lineArray[1], lineArray[2], Double.Parse(lineArray[3]), Double.Parse(lineArray[4]), 
                  lineArray[5], DateTime.Parse(lineArray[6]), DateTime.Parse(lineArray[7]), bool.Parse(lineArray[8]));

                topicCollection.Add(testTopic);

                //Console.WriteLine("Which topic id would you like to look up?");
                //int userSearch = Int32.Parse(Console.ReadLine());


                List<string> outContents = new List<string>();

                foreach (Topic aTopic in topicCollection)
                {
                    outContents.Add(aTopic.ToString());
                }


                string outFile = @"C:\Users\Maria T\source\repos\Learning diary Maria\Learning diary Maria\outFile.txt";
                File.WriteAllLines(outFile, outContents);

            }

            else
            {
                Console.WriteLine("Please write either A, B or C.");
            }



            void AddTopic()
            {
                //Asking user for the topic properties

                Console.WriteLine("Enter the topic id number (e.g. 0): ");
                int topicId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the title of the topic: ");
                string title = Console.ReadLine();


                Console.WriteLine("Enter the description of the topic: ");
                string description = Console.ReadLine();


                Console.WriteLine("Enter the estimated time to master this topic (in number of hours, e.g. 5.5): ");
                double estimatedTimeToMaster = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Enter the time spent (in number of hours, e.g. 5.2) : ");
                double timeSpent = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Enter the source of the topic (e.g. a website URL) :");
                string source = Console.ReadLine();

                Console.WriteLine("What date did you start learning this? (Write the date in dd/mm/yyyy format).");
                DateTime startLearningDay = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("What day have you completed the subject? (Write the date in dd/mm/yyyy format).");
                DateTime completionDay = DateTime.Parse(Console.ReadLine());

                //Checking if the studies are in progress by comparing the finishing date to today's date.
                bool inProgress = completionDay.ToLocalTime() < DateTime.Now;
                if (inProgress == true)
                {
                    Console.WriteLine("You have completed studying the topic!");
                }

                else if (inProgress == false)
                {
                    Console.WriteLine("You are still in the middle of studying the topic.");
                }

                //Creating a new object of topic class

                Topic topic = new Topic(topicId, title, description, estimatedTimeToMaster, timeSpent, source, startLearningDay, completionDay, inProgress);

                //Adding the user inputted object into topicCollection, the list of Topic objects
                topicCollection.Add(topic);


                //Printing the user inputted object to file 
                string path = @"C:\Users\Maria T\source\repos\Learning diary Maria\Learning diary Maria\Topic.txt";
                if (!File.Exists(path))
                {

                    using (System.IO.StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(topic.Id.ToString() + "\n," + topic.Title + "\n," + topic.Description + "\n," + topic.EstimatedTimeToMaster.ToString() + "\n," + topic.TimeSpent.ToString() + "\n," + topic.Source +
                        "\n," + topic.StartLearningDay.ToString() + "\n," + topic.CompletionDay.ToString() + "\n," + topic.InProgress.ToString() + "\n,");
                    }
                    Console.WriteLine("Your topic was saved to the learning diary!");
                }
                else
                {
                    using (System.IO.StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(topic.Id.ToString() + "\n," + topic.Title + "\n," + topic.Description + "\n," + topic.EstimatedTimeToMaster.ToString() + "\n," + topic.TimeSpent.ToString() + "\n," + topic.Source +
                        "\n," + topic.StartLearningDay.ToString() + "\n," + topic.CompletionDay.ToString() + "\n," + topic.InProgress.ToString() + "\n,");
                    }
                    Console.WriteLine("Your topic was saved to the learning diary!");

                }


            }
        }
    }
}

    

    

