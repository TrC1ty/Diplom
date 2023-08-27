// <copyright file="ApplicationDbContext.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;

    using Diplom.Core.Data.Entities;
    using Diplom.Core.Data.Entities.Pdf;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Data context class.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">>Database context options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets EF collection of ApplicationUserRole objects (relationship between users and roles).
        /// </summary>
        public DbSet<ApplicationUserRole> ApplicationUserRoles => this.Set<ApplicationUserRole>();

        /// <summary>
        /// Gets EF collection of users rights objects.
        /// </summary>
        public DbSet<UserRights> UsersRights => this.Set<UserRights>();

        /// <summary>
        /// Gets EF collection of Project objects.
        /// </summary>
        public DbSet<Project> Projects => this.Set<Project>();

        /// <summary>
        /// Gets EF collection of NaturalPerson objects.
        /// </summary>
        public DbSet<NaturalPerson> NaturalPersons => this.Set<NaturalPerson>();

        /// <summary>
        /// Gets EF collection of IndividualEntrepreneur objects.
        /// </summary>
        public DbSet<IndividualEntrepreneur> IndividualEntrepreneurs => this.Set<IndividualEntrepreneur>();

        /// <summary>
        /// Gets EF collection of IndividualEntrepreneur objects.
        /// </summary>
        public DbSet<LegalEntity> LegalEntities => this.Set<LegalEntity>();

        /// <summary>
        /// Gets EF collection of ProjectSection objects.
        /// </summary>
        public DbSet<ProjectSection> ProjectSections => this.Set<ProjectSection>();

        /// <summary>
        /// Gets EF collection of Work objects.
        /// </summary>
        public DbSet<Work> Works => this.Set<Work>();

        /// <summary>
        /// Gets EF collection of Material objects.
        /// </summary>
        public DbSet<Material> Materials => this.Set<Material>();

        /// <summary>
        /// Gets EF collection of ExecutiveScheme objects.
        /// </summary>
        public DbSet<ExecutiveScheme> ExecutiveSchemes => this.Set<ExecutiveScheme>();

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            base.OnModelCreating(modelBuilder);

            // Simplify common identity core table names.
            modelBuilder.Entity<ApplicationUser>()
                .ToTable(name: "Users");
            modelBuilder.Entity<ApplicationRole>()
                .ToTable(name: "Roles");
            modelBuilder.Entity<ApplicationUserRole>()
                .ToTable(name: "UserRoles");

            // Allow role IDs to be specified.
            modelBuilder.Entity<ApplicationRole>()
                .Property(r => r.Id)
                .ValueGeneratedNever();

            // Configure ASP.NET identity core as per
            // https://stackoverflow.com/questions/51004516/net-core-2-1-identity-get-all-users-with-their-associated-roles/51005445#51005445
            // https://github.com/aspnet/Identity/issues/1361#issuecomment-419612809
            modelBuilder.Entity<ApplicationUserRole>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });

                ur.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                ur.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Projects)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Participants)
                .WithOne(p => p.Project)
                .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Sections)
                .WithOne(s => s.Project)
                .HasForeignKey(s => s.ProjectId);

            modelBuilder.Entity<ProjectSection>()
                .HasMany(s => s.Works)
                .WithOne(w => w.Section)
                .HasForeignKey(w => w.SectionId);

            modelBuilder.Entity<Work>()
                .HasMany(w => w.Materials)
                .WithOne(m => m.Work)
                .HasForeignKey(m => m.WorkId);

            modelBuilder.Entity<Work>()
               .HasMany(w => w.ExecutiveSchemes)
               .WithOne(s => s.Work)
               .HasForeignKey(s => s.WorkId);

            modelBuilder.Entity<WorkDocumentation>()
                .HasOne(m => m.Pdf)
                .WithOne(p => p.WorkDocumentation)
                .HasForeignKey<WorkDocumentationPdf>(p => p.Id);

            modelBuilder.Entity<WorkDocumentation>().ToTable("WorkDocumentations");
            modelBuilder.Entity<WorkDocumentationPdf>().ToTable("WorkDocumentations");
        }
    }
}
