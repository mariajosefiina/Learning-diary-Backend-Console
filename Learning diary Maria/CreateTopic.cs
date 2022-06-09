using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Learning_diary_Maria
{
    class CreateTopic
    {

        public class Topic
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public double EstimatedTimeToMaster { get; set; }
            public double TimeSpent { get; set; }
            public string Source { get; set; }
            public DateTime StartLearningDay { get; set; }
            public DateTime CompletionDay { get; set; }
            public bool InProgress { get; set; }


            public Topic()

            {
            }


            public Topic(int Id, string Title, string Description, double EstimatedTimeToMaster, double TimeSpent, string Source,
                DateTime StartLearningDay, DateTime CompletionDay, bool InProgress)
            {

            }


            public static void AddTopic()
            {
                //Creating a list of Topic class objects called topicCollection
                List<Topic> topicCollection = new List<Topic>();

                // Creating a new topic
                Topic topic = new Topic();

                //Asking user for the topic properties
                Console.WriteLine("Enter the topic id number: ");
                topic.Id = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the title of the topic: ");
                topic.Title = Console.ReadLine();


                Console.WriteLine("Enter the description of the topic: ");
                topic.Description = Console.ReadLine();
                string description = topic.Description;

                Console.WriteLine("Enter the estimated time to master this topic (in hours): ");
                topic.EstimatedTimeToMaster = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the time spent (in hours) : ");
                topic.TimeSpent = double.Parse(Console.ReadLine());

                Console.WriteLine("Enter the source of the topic (e.g. a website) :");
                topic.Source = Console.ReadLine();

                Console.WriteLine("What date did you start learning this?");
                topic.StartLearningDay = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("What day have you completed the subject?");
                topic.CompletionDay = DateTime.Parse(Console.ReadLine());

                //Checking if the studies are in progress by comparing the finishing date to today's date.
                topic.InProgress = topic.CompletionDay.ToLocalTime() < DateTime.Now;
                if (topic.InProgress == true)
                {
                    Console.WriteLine("You have completed studying the topic!");
                }

                else if (topic.InProgress == false)
                {
                    Console.WriteLine("You are still in the middle of studying the topic.");
                }

                //Adding the user inputted object into topicCollection, the list of objects 
                topicCollection.Add(new Topic(topic.Id, topic.Title, topic.Description, topic.EstimatedTimeToMaster, topic.TimeSpent, topic.Source, topic.StartLearningDay, topic.CompletionDay, topic.InProgress));

                //Printing the user inputted object to file 
                string path = @"C:\Users\Maria T\source\repos\Learning diary\Learning diary";
                if (!File.Exists(path))
                {
                    using (System.IO.StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(topic.Id.ToString() + " " + topic.Title + " " + topic.Description + " " + topic.EstimatedTimeToMaster.ToString() + " " + topic.TimeSpent.ToString() + " " + topic.Source +
                        " " + topic.StartLearningDay.ToString() + " " + topic.CompletionDay.ToString() + " " + topic.InProgress.ToString());
                    }
                    Console.WriteLine("The topic is saved to your learning diary.");
                }



            }
        }
    }
}
