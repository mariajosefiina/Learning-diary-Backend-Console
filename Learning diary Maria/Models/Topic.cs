using System;
using System.Collections.Generic;

#nullable disable

namespace Learning_diary_Maria.Models
{
    public partial class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? TimeToMaster { get; set; }
        public int? TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime? StartLearningDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool? InProgress { get; set; }
        public bool? IsLate { get; set; }
    


    public override string ToString()
    {
            return String.Format("Topic Id: {0} \nTitle: {1}\nDescription: {2}\nEstimated time to master the topic in hours: {3}\nTime spent so far: {4}\nThe source of the topic: {5}\nYou started learning this topic on: {6}\nThe topic is finished studying: {7}\n",
                                             Id, Title, Description, TimeToMaster, TimeSpent, Source, StartLearningDate, InProgress);

        }
   }
}
