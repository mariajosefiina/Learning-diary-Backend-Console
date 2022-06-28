using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Learning_diary_Maria.Models;
using ClassLibrary1;


namespace Learning_diary_Maria
{
    class Program
    {

        static void Main(string[] args)
        {
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
                


                using (LearningDiaryContext TopicConnection = new LearningDiaryContext())
                {
                    Models.Topic test = new Models.Topic();

                    Console.WriteLine("Enter the topic id number (e.g. 0): ");
                    test.Id = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the title of the topic: ");
                    test.Title = Console.ReadLine();

                    Console.WriteLine("Enter the description of the topic: ");
                    test.Description = Console.ReadLine();

                    Console.WriteLine("Enter the estimated time to master this topic (in days, e.g. 5): ");
                    test.TimeToMaster = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter the time spent (in days, e.g. 2) : ");
                    test.TimeSpent = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter the source of the topic (e.g. a website URL) :");
                    test.Source = Console.ReadLine();

                    Console.WriteLine("What date did you start learning this? (Write the date in dd/mm/yyyy format).");
                    test.StartLearningDate = DateTime.Parse(Console.ReadLine());

                    Console.WriteLine("Are you still in the middle of your studies? Answer yes or no.");
                    string InProgress = Console.ReadLine().ToLower();

                    DayCalculation dayCalculation = new DayCalculation();

                    if (InProgress == "yes")
                    {
                        Console.WriteLine("Good luck with the studies!");
                    }

                    else if (InProgress == "no")
                    {

                        Console.WriteLine("When did you finish studying this topic? (Write the date in dd/mm/yyyy format).");
                        test.CompletionDate = DateTime.Parse(Console.ReadLine());

                        var checkingDate = dayCalculation.IsFuture((DateTime)test.CompletionDate);
                        test.InProgress = checkingDate;

                        test.IsLate = dayCalculation.IsLate(test.StartLearningDate, test.CompletionDate, test.TimeToMaster);

                        if (test.IsLate == true)
                        {
                            Console.WriteLine("You are late with your studies!");
                        }

                        else if (test.IsLate == false)
                        {
                            Console.WriteLine("You have finished the topic in time!");
                        }

                    }


                    TopicConnection.Topics.Add(test);
                    TopicConnection.SaveChanges();

                    Console.WriteLine("Your topic was saved to the learning diary!");

                }

            }


            void PrintTopics()

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
            {

                using (LearningDiaryContext TopicConnection = new LearningDiaryContext())
                {
                    var savedTopics = TopicConnection.Topics.OrderBy(topic => topic);
                    if (savedTopics.Any(savedobject => savedobject.Id == search))

                    {
                        var searchId = savedTopics.Where(savedobject => savedobject.Id == search);
                        foreach (var topic in searchId)
                        {
                            Console.WriteLine("The topic Id {0} was found, and it is connected to the topic called {1}", search, topic.Title);
                        }

                    }
                    else
                    {
                        Console.WriteLine("The learning diary does not contain topics with that id :(");
                    }

                }
            }

            void ChangeIdField(int search)

            {
                using (LearningDiaryContext TopicConnection = new LearningDiaryContext())
                {
                    var savedTopics = TopicConnection.Topics.OrderBy(topic => topic);
                    if (savedTopics.Any(savedobject => savedobject.Id == search))

                    {
                        Console.WriteLine("What title would you like to switch?\nA- Topic title\nB- Topic description\nC- Estimated time to master\nD- Estimated new finishing date");
                        char userInput = Convert.ToChar(Console.ReadLine().ToLower());


                        if (userInput == 'a')
                        {
                            Console.WriteLine("What do you want to change the title to?");
                            string newTitle = Console.ReadLine();

                            var changeTitle =
                                                from savedobject in TopicConnection.Topics
                                                where savedobject.Id == search
                                                select savedobject;

                            foreach (Models.Topic topic in changeTitle)
                            {
                                topic.Title = newTitle;
                                Console.WriteLine("Your topic id {0} title was changed to {1}", topic.Id, newTitle);
                            }

                        }

                        TopicConnection.SaveChanges();

                        if (userInput == 'b')
                        {
                            Console.WriteLine("What do you want to change the description to?");
                            string newDescription = Console.ReadLine();

                            var changeDescription = from savedobject in TopicConnection.Topics
                                                    where savedobject.Id == search
                                                    select savedobject;

                            foreach (Models.Topic topic in changeDescription)
                            {
                                topic.Description = newDescription;
                                Console.WriteLine("Your topic id {0} description has now been switched to {1}", topic.Id, newDescription);
                            }
                        }


                        TopicConnection.SaveChanges();

                        if (userInput == 'c')
                        {
                            //! Here I would like to receive comments: Would it have been a smoother execution to put this code bit in the very beginnig: TopicConnection.Topics.Where(topic => topic.Id == search).Single();
                            // And afterwards do each field like I've done it here -> now my a b d options have a bit too many code rows that could be eliminated (although I enjoyed trying out different LINQ versions)?

                            Console.WriteLine("What is the new estimated time to study this topic?");
                            int newEstimatedTime = Convert.ToInt32(Console.ReadLine());

                            var changeEstimatedTime = TopicConnection.Topics.Where(topic => topic.Id == search).Single();
                            changeEstimatedTime.TimeToMaster = newEstimatedTime;
                            Console.WriteLine("Your topic id {0} estimated time to master has now been switched to {1}", changeEstimatedTime.Id, newEstimatedTime);

                        }
                        TopicConnection.SaveChanges();


                        if (userInput == 'd')
                        {
                            Console.WriteLine("When is the new estimated finishing date?");
                            DateTime newFinishDate = DateTime.Parse(Console.ReadLine());

                            var changeFinishDate = from savedobject in TopicConnection.Topics
                                                   where savedobject.Id == search
                                                   select savedobject;

                            foreach (Models.Topic topic in changeFinishDate)
                            {
                                topic.CompletionDate = newFinishDate;
                                Console.WriteLine("Your topic id {0} new estimated finishing date has now been switched to {1}", topic.Id, newFinishDate);
                            }
                        }
                        TopicConnection.SaveChanges();


                    }
                    else
                    {
                        Console.WriteLine("The learning diary does not contain topics with that id :(");
                    }



                }
            }

            void DeleteId(int search)

            {
                using (LearningDiaryContext TopicConnection = new LearningDiaryContext())
                {
                    var savedTopics = TopicConnection.Topics.OrderBy(topic => topic);
                    if (savedTopics.Any(savedobject => savedobject.Id == search))

                    {
                        var removeTopic = from savedobject in TopicConnection.Topics
                                               where savedobject.Id == search
                                               select savedobject;
                        foreach (Models.Topic topic in removeTopic)
                        {
                            TopicConnection.Topics.Remove(topic);
                            Console.WriteLine("The topic Id {0} has now been removed.", search);
                        }
                    }

                    else
                    {
                      Console.WriteLine("The learning diary does not contain topics with that id :(");
                    }

                    TopicConnection.SaveChanges();

                }



            }
        }
    } 
}


    //SAVING FOR LATER USE = 
  //string path = @"C:\Users\Maria T\source\repos\Learning diary Maria\Learning diary Maria\Topic.txt";
 //List<Topic> savedObjects = new List<Topic>();
//foreach (string line in File.ReadLines(path))

//{
//    string[] lineArray = line.Split(',');
//    Topic testTopic = new Topic(Int32.Parse(lineArray[0]), lineArray[1], lineArray[2], Double.Parse(lineArray[3]), Double.Parse(lineArray[4]),
//    lineArray[5], DateTime.Parse(lineArray[6]), DateTime.Parse(lineArray[7]), bool.Parse(lineArray[8]));
//    savedObjects.Add(testTopic);
//}


//if (savedObjects.Any(savedobject => savedobject.Id == search))
//{
//    var removeTopic = savedObjects.RemoveAll(savedobject => savedobject.Id == search);

//    Console.WriteLine("The topic with id {0} called {1} was removed.", search, removeTopic.ToString());
//}





