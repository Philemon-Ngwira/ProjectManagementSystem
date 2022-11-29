﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Shared.Models
{
    [Table("Status")]
    public partial class Status
    {
        public Status()
        {
            Projects = new HashSet<Project>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string StatusType { get; set; }

        [InverseProperty("StatusNavigation")]
        public virtual ICollection<Project> Projects { get; set; }
    }
}