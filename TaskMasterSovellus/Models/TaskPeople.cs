//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskMasterSovellus.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskPeople
    {
        public int TaskPeopleId { get; set; }
        public int TaskId { get; set; }
        public Nullable<int> UserGroupLinkId { get; set; }
    
        public virtual UserGroupLink UserGroupLink { get; set; }
        public virtual Tasks Tasks { get; set; }
    }
}
