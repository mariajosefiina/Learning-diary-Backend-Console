using System;
using System.IO;
using static Learning_diary_Maria.Topic;
using System.Collections.Generic;

namespace Learning_diary_Maria
{
    class Program
    {
        static void Main(string[] args)
        {
            //Creating a new dictionary to save objects to 
           


            Console.WriteLine("If you'd like to create a new topic to your learning diary, write A.");
            Console.WriteLine("Or if you'd like to see the topics you have saved, write B.");
            Console.WriteLine("Or if you'd like to search for a topic based on its topic Id, write C.");

            string userInput = Console.ReadLine().ToLower();
            const string a = "a";
            const string b = "b";
            const string c = "c";

            switch (userInput)
            {
                case a:
                    AddTopic();
                    break;

                case b:
                    PrintTopic();
                    break;

                //case c:
                //    LookUpTopic();
                //    break;

            }



            static void AddTopic()
            {
                List<Topic> topicCollection = new List<Topic>();
                //Asking user for the topic properties

                Console.WriteLine("Enter the topic id number (e.g. 0): ");
                    int topicId = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the title of the topic: ");
                    string title = Console.ReadLine();


                    Console.WriteLine("Enter the description of the topic: ");
                    string description = Console.ReadLine();
                    

                    Console.WriteLine("Enter the estimated time to master this topic (in number of hours, e.g. 5): ");
                    int estimatedTimeToMaster = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the time spent (in number of hours, e.g. 5) : ");
                    double timeSpent = double.Parse(Console.ReadLine());

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

                //Adding the user inputted object into topicCollection, the new list of objects
                topicCollection.Add(topic);




                //Printing the user inputted object to file 
                string path = @"C:\Users\Maria T\source\repos\Learning diary Maria\Learning diary Maria\Topic.txt";
                if (!File.Exists(path))
                {

                    using (System.IO.StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(topic.Id.ToString() + "\n " + topic.Title + "\n " + topic.Description + "\n " + topic.EstimatedTimeToMaster.ToString() + "\n " + topic.TimeSpent.ToString() + "\n " + topic.Source +
                        "\n " + topic.StartLearningDay.ToString() + "\n" + topic.CompletionDay.ToString() + "\n " + topic.InProgress.ToString());
                    }
                    Console.WriteLine("Your topic was saved to the learning diary!");
                }
                else
                {
                    using (System.IO.StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(topic.Id.ToString() + "\n " + topic.Title + "\n " + topic.Description + "\n " + topic.EstimatedTimeToMaster.ToString() + "\n " + topic.TimeSpent.ToString() + "\n " + topic.Source +
                        "\n " + topic.StartLearningDay.ToString() + "\n" + topic.CompletionDay.ToString() + "\n " + topic.InProgress.ToString());
                    }
                    Console.WriteLine("Your topic was saved to the learning diary!");

                }

            }
        }




            static void PrintTopic()
            {

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
            }

            //static void LookUpTopic()
            //{

            //    Console.WriteLine("What topic Id would you like to look for? ");
            //     int searchId = Int32.Parse(Console.ReadLine());

            //    if (topicCollection.ContainsKey(searchId))
            //    {
            //        Console.WriteLine("The topic was found!");
            //       }

            //      else
            //      {
            //           Console.WriteLine("Topic was not found! :(");
            //       }

            




        }

        }
    

