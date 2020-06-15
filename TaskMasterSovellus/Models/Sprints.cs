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
    
    public partial class Sprints
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sprints()
        {
            this.ProjectConnection = new HashSet<ProjectConnection>();
            this.SprintTemplateConnection = new HashSet<SprintTemplateConnection>();
            this.Tasks = new HashSet<Tasks>();
        }
    
        public int SprintId { get; set; }
        public string SprintName { get; set; }
        public Nullable<int> AdminId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> BackgColor { get; set; }
        public Nullable<int> ProcessColor { get; set; }
    
        public virtual Colors Colors { get; set; }
        public virtual Colors Colors1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProjectConnection> ProjectConnection { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SprintTemplateConnection> SprintTemplateConnection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
