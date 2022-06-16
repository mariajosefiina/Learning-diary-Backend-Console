using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Learning_diary_Maria
{
    class Program
    {

        static void Main(string[] args)
        {
            // Creating a list of Topic class objects
            List<Topic> topicCollection = new List<Topic>();

            // Asking the user what they would like to do and executing the action via if-statements.
            Console.WriteLine("If you'd like to create a new topic to your learning diary, write A.");
            Console.WriteLine("Or if you'd like to see the topics you have saved, write B.");
            Console.WriteLine("Or if you'd like to search for a topic based on its topic Id, write C.");
            string userInput = Console.ReadLine().ToLower();


            //If input is a, user is asked bunch of questions about the topic which are printed out and saved to text file. An instance of Topic class is created.
            if (userInput == "a")

            {
                AddTopic();
            }



            // If input is b, the topic list in text file is opened and read to the use. Added file reading issues catch in case the path needed working.
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
                    Console.WriteLine("The file could not be read.");
                    Console.WriteLine(e.Message);
                }
            }
            


            // If input is c, the topic list in text file is opened and split into Topic class objects using split. I split the lines based on comma ,
            else if (userInput == "c") 
            {
                string path = @"C:\Users\Maria T\source\repos\Learning diary Maria\Learning diary Maria\Topic.txt";
                List<Topic> savedObjects = new List<Topic>();

                // Reading the existing txt file with topics, and splitting them into objects row by row. The numbers inside the new topic refer to indexes, not line
                foreach (string line in File.ReadLines(path))

                {
                    string[] lineArray = line.Split(',');
                    Topic testTopic = new Topic(Int32.Parse(lineArray[0]), lineArray[1], lineArray[2], Double.Parse(lineArray[3]), Double.Parse(lineArray[4]),
                    lineArray[5], DateTime.Parse(lineArray[6]), DateTime.Parse(lineArray[7]), bool.Parse(lineArray[8]));
                    savedObjects.Add(testTopic);

                }

                foreach (Topic topic in savedObjects)
                {
                    Console.WriteLine(topic.Title);
                }
              


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
                

                    using (System.IO.StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(topic.Id.ToString() + "," + topic.Title + "," + topic.Description + "," + topic.EstimatedTimeToMaster.ToString() + "," + topic.TimeSpent.ToString() + "," + topic.Source +
                        "," + topic.StartLearningDay.ToString() + "," + topic.CompletionDay.ToString() + "," + topic.InProgress.ToString() +",");
                    }
                    Console.WriteLine("Your topic was saved to the learning diary!");
                

                }


            }
        }
    }

    

    

