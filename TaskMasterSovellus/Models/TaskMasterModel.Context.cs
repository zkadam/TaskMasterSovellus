﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TaskMasterTietokantaEntities : DbContext
    {
        public TaskMasterTietokantaEntities()
            : base("name=TaskMasterTietokantaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<GroupLink> GroupLink { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<ProjectConnection> ProjectConnection { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<SprintTemplate> SprintTemplate { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TaskPeople> TaskPeople { get; set; }
        public virtual DbSet<TemplateTaskConnection> TemplateTaskConnection { get; set; }
        public virtual DbSet<UserGroupLink> UserGroupLink { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<SprintTemplateConnection> SprintTemplateConnection { get; set; }
        public virtual DbSet<Sprints> Sprints { get; set; }
    }
}
