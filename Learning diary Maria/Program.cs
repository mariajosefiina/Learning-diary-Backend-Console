using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Learning_diary_Maria.Models;
using ClassLibrary1;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Learning_diary_Maria
{
    class Program
    {

        static async Task Main(string[] args)
        {


            //Adding a while loop to be able to run the program options as long as the user wants, until they write x and the loop breaks.
            bool programRunning = true;  

            while (programRunning)
            {
                // Clearing the console so user is able to see the menu choices easier. 
                Console.Clear(); 
              
                // Giving the user menu choices and calling the methods asynchronously. 
                Console.WriteLine("Write A, if you'd like to create a new topic to your learning diary.");
                Console.WriteLine("Write B, if you'd like to see all your topics and their contents.");
                Console.WriteLine("Write C, if you'd like to search for a topic based on its topic Id.");
                Console.WriteLine("Write D, if you'd like to change a field in a specific topic Id.");
                Console.WriteLine("Write E, if you'd like to delete a topic based on its topic Id.");
                Console.WriteLine("Write X, if you'd like to exit the learning diary app.");
                try
                {
                    string userInput = Console.ReadLine().ToLower();

                    switch (userInput)
                    {
                        case "a":
                            await AddTopic();
                            continue;

                        case "b":
                            await PrintTopics();
                            continue;

                        case "c":
                            Console.WriteLine("What topic Id would you like to look for? Write a number, e.g. 5.");
                            int search = Convert.ToInt32(Console.ReadLine());

                            await SearchId(search);
                            continue;

                        case "d":
                            Console.WriteLine("What topic Id would you like to change? Write a number, e.g. 5.");
                            int search2 = Convert.ToInt32(Console.ReadLine());
                            await ChangeIdField(search2);
                            continue;

                        case "e":
                            Console.WriteLine("What topic Id would you like to delete? Write a number, e.g. 5.");
                            int search3 = Convert.ToInt32(Console.ReadLine());
                            await DeleteId(search3);
                            continue;

                        case "x":
                            programRunning = false;
                            break;

                        default:
                            Console.WriteLine("Please write either A, B, C, D, E or X. Press enter to continue.");
                            Console.ReadLine();
                            continue;

                    }

                }

                // Catching whether user inputs a number.
                catch (FormatException) 
                {
                    Console.WriteLine("Please write a number in numeric form, such as 3.\n Press any key to continue.");
                    Console.ReadKey(true);
                    continue;
                }

            }



            //*******METHODS**********


            static async Task AddTopic() //Asynchronous method

            {   //Creating new topic class object
                Models.Topic topic = new Models.Topic();

                //Adding a while loop so that the user is able to continue execution despite catching a format error.
                while (true) 
                {
                    try 
                    {
                        //Asking the user new topic parameters and trying to catch format exceptions.
                        Console.WriteLine("Enter the topic id number (e.g. 0): ");
                        topic.Id = int.Parse(Console.ReadLine());

                        Console.WriteLine("Enter the title of the topic: ");
                        topic.Title = Console.ReadLine();

                        Console.WriteLine("Enter the description of the topic: ");
                        topic.Description = Console.ReadLine();

                        Console.WriteLine("Enter the estimated time to master this topic (in days, e.g. 5): ");
                        topic.TimeToMaster = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter the time spent (in days, e.g. 2) : ");
                        topic.TimeSpent = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("Enter the source of the topic (e.g. a website URL) :");
                        topic.Source = Console.ReadLine();

                        Console.WriteLine("What date did you start learning this? (Write the date in dd/mm/yyyy format).");
                        topic.StartLearningDate = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Are you still in the middle of your studies? Answer yes or no.");
                        string InProgress = Console.ReadLine().ToLower();

                        //Using Class Library that we created with Janne. The method checks whether the topic was studied in estimated time. Added this as a property to database and Topic class.
                        DayCalculation dayCalculation = new DayCalculation();
                        

                        if (InProgress == "yes")
                        {
                            Console.WriteLine("Good luck with the studies!");
                        }

                        else if (InProgress == "no")
                        {

                            Console.WriteLine("When did you finish studying this topic? (Write the date in dd/mm/yyyy format).");
                            topic.CompletionDate = DateTime.Parse(Console.ReadLine());

                            var checkingDate = dayCalculation.IsFuture((DateTime)topic.CompletionDate);
                            topic.InProgress = checkingDate;

                            topic.IsLate = dayCalculation.IsLate((DateTime)topic.StartLearningDate, (DateTime)topic.CompletionDate, (int)topic.TimeToMaster);

                            if (topic.IsLate == true)
                            {
                                Console.WriteLine("You didn't finish the topic within the estimated time!");
                            }

                            else if (topic.IsLate == false)
                            {
                                Console.WriteLine("You finished the topic in time!");
                            }

                        }

                        //Establishing connection to the database and saving the new topic there. Made the database connection smaller than it was mid-course.

                        using (LearningDiaryContext TopicConnection = new LearningDiaryContext()) 
                        {
                            TopicConnection.Add(topic);
                            await TopicConnection.SaveChangesAsync();
                        }

                        Console.WriteLine("Your topic was saved to the learning diary!");
                    }
                    catch (FormatException)
                    {

                        Console.WriteLine("Please write the your answer in the format that is instructed (inside parantheses). Press enter and start again.");
                        continue;
                    }
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey(true);
                    break;
                }

            }
        }

        static async Task PrintTopics() //Asynchronous method

        {
            //Adding a while loop to be able to run the program further after executing the the listing.
            while (true) 
            {
                using (LearningDiaryContext TopicConnection = new LearningDiaryContext())
                {
                    //Topics are looped and ordered based on topic Id numerical value.
                    var savedTopics = await Task.Run(() => TopicConnection.Topics.OrderBy(topic => topic)); 
                    foreach (var topic in savedTopics)
                    {
                        Console.WriteLine(topic);
                    }
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey(true);
                break;
            }


        }

        static async Task SearchId(int search) //Asynchronous method

        {
            //Adding a while loop to be able to run the program further after executing the the listing.
            while (true) 
            {
                try
                {
                    using (LearningDiaryContext TopicConnection = new LearningDiaryContext())
                    {
                        var savedTopic = await Task.Run(() => TopicConnection.Topics.Where(topic => topic.Id == search).SingleOrDefault());

                        //First checking whether the id number is found from database.
                        if (savedTopic == null) 
                        {
                            Console.WriteLine("The learning diary does not contain topics with that id :(");
                        }
                       else if (savedTopic.Id == search)

                        {
                            Console.WriteLine("The topic Id {0} was found, and it has the following information \n{1}", search, savedTopic);
                        }
                      
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please write a number, such as 5. Press enter and start again.");
                }

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey(true);
                break;
            }
        }


        static async Task ChangeIdField(int search)  //Asynchronous method


        {
            while (true)
            {
                try
                {
                    using (LearningDiaryContext TopicConnection = new LearningDiaryContext())
                    {
                        var savedTopic = await Task.Run(() => TopicConnection.Topics.Where(topic => topic.Id == search).SingleOrDefault());

                        //First checking whether the id number is found from database.
                        if (savedTopic == null) 
                        {
                            Console.WriteLine("The learning diary does not contain topics with that id :(");
                        }
                       else if (savedTopic.Id == search)

                        {
                            Console.WriteLine("What field would you like to edit?\nA- Topic title\nB- Topic description\nC- Estimated time to master\nD- Estimated new finishing date");
                            char userInput = Convert.ToChar(Console.ReadLine().ToLower());


                            if (userInput == 'a')
                            {
                                Console.WriteLine("What do you want to change the title to?");
                                string newTitle = Console.ReadLine();
                                savedTopic.Title = newTitle;

                                Console.WriteLine("Your topic id {0} title was changed to {1}", search, newTitle);

                            }

                            await TopicConnection.SaveChangesAsync();

                            if (userInput == 'b')
                            {
                                Console.WriteLine("What do you want to change the description to?");
                                string newDescription = Console.ReadLine();
                                savedTopic.Description = newDescription;

                                Console.WriteLine("Your topic id {0} description has now been switched to {1}", search, newDescription);

                            }


                            await TopicConnection.SaveChangesAsync();

                            if (userInput == 'c')
                            {

                                Console.WriteLine("What is the new estimated time to study this topic? (in days, e.g. 8)");
                                int newEstimatedTime = Convert.ToInt32(Console.ReadLine());

                                savedTopic.TimeToMaster = newEstimatedTime;
                                Console.WriteLine("Your topic id {0} estimated time to master has now been switched to {1}", search, newEstimatedTime);

                            }
                            await TopicConnection.SaveChangesAsync();


                            if (userInput == 'd')
                            {
                                Console.WriteLine("When is the new estimated finishing date? (Write the date in dd/mm/yyyy format)");
                                DateTime newFinishDate = DateTime.Parse(Console.ReadLine());
                                savedTopic.CompletionDate = newFinishDate;


                                Console.WriteLine("Your topic id {0} new estimated finishing date has now been switched to {1}", search, newFinishDate);
                            }
                            await TopicConnection.SaveChangesAsync();
                        }
                       
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please write the your new input in the format that is instructed (inside parantheses). Press enter and start again.");
                }

                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The learning diary does not contain topics with that id :(");
                }

                Console.WriteLine("Press any key to continue.");
                Console.ReadKey(true);
                break;
            }

        }

        static async Task DeleteId(int search) //Asynchronous method


        {
            while (true)
            {
                try
                {
                    using (LearningDiaryContext TopicConnection = new LearningDiaryContext())
                    {
                        var savedTopic = await Task.Run(() => TopicConnection.Topics.Where(topic => topic.Id == search).SingleOrDefault());

                        //First checking whether the id number is found from database.
                        if (savedTopic == null) 
                        {
                            Console.WriteLine("The learning diary does not contain topics with that id :(");
                        }
                      else if (savedTopic.Id == search)

                        {
                            TopicConnection.Topics.Remove(savedTopic);
                            Console.WriteLine("The topic Id {0} has now been removed.", search);
                        }

                        await TopicConnection.SaveChangesAsync();

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey(true);
                break;
            }
        }


    }
}
     



    //SAVING FOR LATER USE, PARSING AN OBJECT FROM TXT FILE = 
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





