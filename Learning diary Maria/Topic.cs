﻿using System;
using System.Collections.Generic;
using System.IO;


namespace Learning_diary_Maria
{
    class Topic
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



        public Topic(int id, string title, string description, double estimatedTimeToMaster, double timeSpent,
           string source, DateTime startLearningDay, DateTime completionDay, bool inProgress)
        {

           this.Id = id;
           this.Title = title;
           this.Description = description;
           this.EstimatedTimeToMaster = estimatedTimeToMaster;
           this.TimeSpent = timeSpent;
           this.Source = source;
           this.StartLearningDay = startLearningDay;
           this.CompletionDay = completionDay;
           this.InProgress = inProgress;

        }


        

    }
}
   


    
