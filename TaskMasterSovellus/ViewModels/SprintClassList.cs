using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMasterSovellus.ViewModels
{
    public class SprintClassList
    {
        //info from tasks table ZA
        public int TaskId { get; set; }
        public int StateId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public Nullable<int> TaskPoints { get; set; }
        public Nullable<int> TaskPriority { get; set; }
       public Nullable<int> SprintId { get; set; }

        //people connectiontaken out, later when brige to people is built could be included ZA
        //public virtual ICollection<TaskPeople> TaskPeople { get; set; }
        //public virtual TaskState TaskState { get; set; }

        //info from task state table ZA

        public string StateName { get; set; }
        public string StateColor { get; set; }
        //public int TemplateConnectionId { get; set; }
        //public int SprintTemplateId { get; set; }

        //public string SprintName { get; set; }
        ////public Nullable<int> AdminId { get; set; }
        //public Nullable<System.DateTime> StartDate { get; set; }
        //public Nullable<System.DateTime> EndDate { get; set; }
        //public string BackgColor { get; set; }
        //public string ProcessColor { get; set; }

        //public int ProjectId { get; set; }
    }
}