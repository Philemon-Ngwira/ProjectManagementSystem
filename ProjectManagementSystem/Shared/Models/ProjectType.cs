// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Shared.Models
{
    [Table("ProjectType")]
    public partial class ProjectType
    {
        public ProjectType()
        {
            Projects = new HashSet<Project>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ProjectTypeName { get; set; }

        [InverseProperty("ProjectTypeNavigation")]
        public virtual ICollection<Project> Projects { get; set; }
    }
}