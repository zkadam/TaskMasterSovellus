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
    
    public partial class TaskState
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaskState()
        {
            this.TemplateTaskConnection = new HashSet<TemplateTaskConnection>();
            this.Tasks = new HashSet<Tasks>();
        }
    
        public int StateId { get; set; }
        public string StateName { get; set; }
        public Nullable<int> ColorId { get; set; }
    
        public virtual Colors Colors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TemplateTaskConnection> TemplateTaskConnection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
