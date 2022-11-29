﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectManagementLibrary;
using ProjectManagementSystem.Shared.Models;

namespace ProjectManagementLibrary.Data
{
    public partial class ProjectManagementSystemDBContext : DbContext
    {
        public ProjectManagementSystemDBContext()
        {
        }

        public ProjectManagementSystemDBContext(DbContextOptions<ProjectManagementSystemDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<CancelledProject> Cancelled_Projects { get; set; }
        public virtual DbSet<CompletedProject> Completed_Projects { get; set; }
        public virtual DbSet<DelayType> DelayTypes { get; set; }
        public virtual DbSet<DeviceCode> DeviceCodes { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<GenderTable> GenderTables { get; set; }
        public virtual DbSet<Key> Keys { get; set; }
        public virtual DbSet<PersistedGrant> PersistedGrants { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectProgressTable> ProjectProgressTables { get; set; }
        public virtual DbSet<ProjectType> ProjectTypes { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<CancelledProject>(entity =>
            {
                entity.HasKey(e => e.CancelledProjectId)
                    .HasName("cancelled projects_id_primary");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CancelledProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Cancelled Projects_Project");
            });

            modelBuilder.Entity<CompletedProject>(entity =>
            {
                entity.HasKey(e => e.CompletedProjectId)
                    .HasName("completed projects_id_primary");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CompletedProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Completed Projects_Project");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Gender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_GenderTable");

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_AspNetRoles");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasOne(d => d.DelayTypeNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.DelayType)
                    .HasConstraintName("FK_Project_DelayTypes");

                entity.HasOne(d => d.ProjectProgressNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProjectProgress)
                    .HasConstraintName("FK_Project_ProjectProgressTable");

                entity.HasOne(d => d.ProjectTypeNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProjectType)
                    .HasConstraintName("FK_Project_ProjectType");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}