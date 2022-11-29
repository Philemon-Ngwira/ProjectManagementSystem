﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Shared.Models
{
    [Table("Completed Projects")]
    public partial class CompletedProject
    {
        [Key]
        public int CompletedProjectId { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateOfCompletion { get; set; }
        public int? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        [InverseProperty("CompletedProjects")]
        public virtual Project Project { get; set; }
    }
}