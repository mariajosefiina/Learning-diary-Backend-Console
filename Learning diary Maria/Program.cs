using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Learning_diary_Maria.Models;

namespace Learning_diary_Maria
{
    class Program
    {
        List<Topic> savedObjects = new List<Topic>();
        static void Main(string[] args)
        {
            // Creating a list of Topic class objects
            List<Topic> topicCollection = new List<Topic>();

            // Asking the user what they would like to do and executing the action via if-statements.
            Console.WriteLine("Write A, if you'd like to create a new topic to your learning diary.");
            Console.WriteLine("Write B, if you'd like to see all your topics and their contents.");
            Console.WriteLine("Write C, if you'd like to search for a topic based on its topic Id.");
            Console.WriteLine("Write D, if you'd like to change a field in a specific topic Id.");
            Console.WriteLine("Write E, if you'd like to delete a topic based on its topic Id.");

            string userInput = Console.ReadLine().ToLower();

            if (userInput == "a")
            {
                AddTopic();
            }


            else if (userInput == "b")
            {
                PrintTopics();
            }

            else if (userInput == "c")
            {
                Console.WriteLine("What topic Id would you like to look for? Write a number, e.g. 5.");
                int search = Convert.ToInt32(Console.ReadLine());
                SearchId(search);
            }

            else if (userInput == "d")
            {
                Console.WriteLine("What topic Id would you like to change? Write a number, e.g. 5.");
                int search = Convert.ToInt32(Console.ReadLine());
                ChangeIdField(search);
            }

            else if (userInput == "e")
            {
                Console.WriteLine("What topic Id would you like to delete? Write a number, e.g. 5.");
                int search = Convert.ToInt32(Console.ReadLine());
                DeleteId(search);
            }

            else
            {
                Console.WriteLine("Please write either A, B, C, D or E.");
            }

            //*******METHODS**********


            void AddTopic()
            {
                //If input is a, user is asked bunch of questions about the topic which are saved to object properties and saved to text file. An instance of Topic class is created.

                //bool inProgress = completionDay.ToLocalTime() < DateTime.Now;
                //if (inProgress == true)
                //{
                //    Console.WriteLine("You have completed studying the topic!");
                //}

                //else if (inProgress == false)
                //{
                //    Console.WriteLine("You are still in the middle of studying the topic.");
                //}


                using (LearningDiaryContext TopicConnection = new LearningDiaryContext())
                {

                    //Creating a new object of topic class
                    Models.Topic test = new Models.Topic(); 

                    Console.WriteLine("Enter the topic id number (e.g. 0): ");
                    test.Id = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the title of the topic: ");
                    test.Title = Console.ReadLine();

                    Console.WriteLine("Enter the description of the topic: ");
                    test.Description = Console.ReadLine();

                    Console.WriteLine("Enter the estimated time to master this topic (in number of hours, e.g. 5): ");
                    test.TimeToMaster = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter the time spent (in number of hours, e.g. 2) : ");
                    test.TimeSpent = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter the source of the topic (e.g. a website URL) :");
                    test.Source = Console.ReadLine();

                    Console.WriteLine("What date did you start learning this? (Write the date in dd/mm/yyyy format).");
                    test.StartLearningDate = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Are you still in the middle of your studies? Answer yes or no.");
                    string InProgress = Console.ReadLine().ToLower();

                    if (InProgress == "yes")
                    {
                        test.InProgress = true;
                        Console.WriteLine("Good luck with the studies!");

                    }
                    else if (InProgress == "no")
                    {
                        test.InProgress = false;
                        Console.WriteLine("When did you finish studying this topic? (Write the date in dd/mm/yyyy format).");
                        test.CompletionDate = DateTime.Parse(Console.ReadLine());
                        TimeSpan studyTime = (TimeSpan)(test.CompletionDate - test.StartLearningDate);
                        Console.WriteLine("You spent a total of {0} days to study this topic." , studyTime.Days.ToString());
                    }

                    TopicConnection.Topics.Add(test);
                    TopicConnection.SaveChanges();

                    Console.WriteLine("Your topic was saved to the learning diary!");

                  
                }


            }



            void PrintTopics()
            // If input is b, the topic list in text file is opened and read to the end.
            {
                using (LearningDiaryContext TopicConnection = new LearningDiaryContext())
                {
                    var savedTopics = TopicConnection.Topics.OrderBy(topic => topic);
                    foreach (var topic in savedTopics)
                    {
                        Console.WriteLine("Topic Id: {0} \nTitle: {1}\nDescription: {2}\nEstimated time to master the topic in hours: {3}\nTime spent so far: {4}\nThe source of the topic: {5}\nYou started learning this topic on: {6}\nThe topic is finished studying: {7}\n",
                                             topic.Id, topic.Title, topic.Description, topic.TimeToMaster, topic.TimeSpent, topic.Source, topic.StartLearningDate, topic.InProgress);
                        
                    }

                }

            }

            void SearchId(int search)
            // If input is c, the topic list in text file is opened. I want to read the file and split its content to Topic class objects based on comma ,. Then I save them to a new list called savedObjects.
            {
                string path = @"C:\Users\Maria T\source\repos\Learning diary Maria\Learning diary Maria\Topic.txt";

                List<Topic> savedObjects = new List<Topic>();

                // The numbers inside the new topic refer to text indexes, not individual lines 
                foreach (string line in File.ReadLines(path))

                {
                    string[] lineArray = line.Split(',');
                    Topic testTopic = new Topic(Int32.Parse(lineArray[0]), lineArray[1], lineArray[2], Double.Parse(lineArray[3]), Double.Parse(lineArray[4]),
                    lineArray[5], DateTime.Parse(lineArray[6]), DateTime.Parse(lineArray[7]), bool.Parse(lineArray[8]));
                    savedObjects.Add(testTopic);
                }

                // Now we check whether the topic Id exists in the new list savedObjects. To use the if statement, I needed linq "Any" command. But to get out the correct Id topic, I needed to loop the list
                // through an interface.

                if (savedObjects.Any(savedobject => savedobject.Id == search))

                {
                    IEnumerable<Topic> searchId = savedObjects.Where(savedobject => savedobject.Id == search);
                    foreach (Topic topic in searchId)
                    {
                        Console.WriteLine("The topic Id {0} was found, and it is connected to the topic called {1}", search, topic.Title);
                    }

                }
                else
                {
                    Console.WriteLine("The learning diary does not contain topics with that id :(");
                }



            }

            void ChangeIdField(int search)
            // If input is d, the topic list in text file is opened. I want to read the file and split its content to Topic class objects based on comma ,. Then I save them to a new list called savedObjects.
            {
                string path = @"C:\Users\Maria T\source\repos\Learning diary Maria\Learning diary Maria\Topic.txt";
                List<Topic> savedObjects = new List<Topic>();
                foreach (string line in File.ReadLines(path))

                {
                    string[] lineArray = line.Split(',');
                    Topic testTopic = new Topic(Int32.Parse(lineArray[0]), lineArray[1], lineArray[2], Double.Parse(lineArray[3]), Double.Parse(lineArray[4]),
                    lineArray[5], DateTime.Parse(lineArray[6]), DateTime.Parse(lineArray[7]), bool.Parse(lineArray[8]));
                    savedObjects.Add(testTopic);
                }

                // Now we check whether the topic Id exists in the new list savedObjects. Only if it does, we can move on to the next part where I ask the user for update fields and input.
                // Else, use is informed that the topic id does not exist in the learning diary. 

                if (savedObjects.Any(savedobject => savedobject.Id == search))

                {
                    Console.WriteLine("Write A, if you want to change the topic title.\nWrite B, if you want to change the topic description.\nWrite C, if you'd like to change the estimated finishing date.");
                    char userInput = Convert.ToChar(Console.ReadLine().ToLower());

                    //Currently, the changes are not printed to a txt file, but you can see that the code works by putting breakpoints to the if statements and seeing how the object values change.

                    if (userInput == 'a')
                    {
                        Console.WriteLine("What do you want to change the title to?");
                        string newTitle = Console.ReadLine();

                        IEnumerable<string> changeTitle = savedObjects.Where(savedobject => savedobject.Id.Equals(search)).Select(savedobject => savedobject.Title = newTitle);
                        foreach (string title in changeTitle)
                        {
                            Console.WriteLine("The topic with Id {0} has now been switched to {1}", search, title);
                        }

                    }

                    if (userInput == 'b')
                    {
                        Console.WriteLine("What do you want to change the description to?");
                        string newDescription = Console.ReadLine();

                        IEnumerable<string> changeDescription = savedObjects.Where(savedobject => savedobject.Id.Equals(search)).Select(savedobject => savedobject.Description = newDescription);
                        foreach (string description in changeDescription)
                        {
                            Console.WriteLine("The topic with Id {0} has now been switched to {1}", search, description);
                        }
                    }

                    if (userInput == 'c')
                    {
                        Console.WriteLine("When is the new estimated finishing date?");
                        DateTime newFinishDate = DateTime.Parse(Console.ReadLine());

                        IEnumerable<DateTime> changeFinishDate = savedObjects.Where(savedobject => savedobject.Id.Equals(search)).Select(savedobject => savedobject.CompletionDay = newFinishDate);
                        foreach (DateTime date in changeFinishDate)
                        {
                            Console.WriteLine("The topic with Id {0} has now been switched to {1}", search, newFinishDate);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The learning diary does not contain topics with that id :(");
                }
            }

            void DeleteId(int search)
            //Currently, the deletion is not printed to a txt file, but you can see that the code works by putting breakpoints to var removetopic-row and seeing how the object amount changes.
            {
                string path = @"C:\Users\Maria T\source\repos\Learning diary Maria\Learning diary Maria\Topic.txt";
                List<Topic> savedObjects = new List<Topic>();
                foreach (string line in File.ReadLines(path))

                {
                    string[] lineArray = line.Split(',');
                    Topic testTopic = new Topic(Int32.Parse(lineArray[0]), lineArray[1], lineArray[2], Double.Parse(lineArray[3]), Double.Parse(lineArray[4]),
                    lineArray[5], DateTime.Parse(lineArray[6]), DateTime.Parse(lineArray[7]), bool.Parse(lineArray[8]));
                    savedObjects.Add(testTopic);
                }


                if (savedObjects.Any(savedobject => savedobject.Id == search))
                {
                    var removeTopic = savedObjects.RemoveAll(savedobject => savedobject.Id == search);

                    Console.WriteLine("The topic with id {0} called {1} was removed.", search, removeTopic.ToString());
                }

                else
                {
                    Console.WriteLine("The learning diary does not contain topics with that id :(");
                }

            }



        }
    } 
}

    


    

    

