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
    
    public partial class SprintTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SprintTemplate()
        {
            this.Sprints = new HashSet<Sprints>();
            this.TemplateTaskConnection = new HashSet<TemplateTaskConnection>();
        }
    
        public int SprintTemplateId { get; set; }
        public string SprintTemplateName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sprints> Sprints { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TemplateTaskConnection> TemplateTaskConnection { get; set; }
    }
}