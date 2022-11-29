﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Shared.Models
{
    [Table("Cancelled Projects")]
    public partial class CancelledProject
    {
        [Key]
        public int CancelledProjectId { get; set; }
        [Required]
        [StringLength(255)]
        public string CancellationReason { get; set; }
        public int? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        [InverseProperty("CancelledProjects")]
        public virtual Project Project { get; set; }
    }
}