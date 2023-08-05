// <copyright file="SectionController.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Controllers
{
    using Diplom.Core.Data;
    using Diplom.Core.Data.Entities;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Section controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionController"/> class.
        /// Section controller initialization.
        /// </summary>
        /// <param name="dbContext">Db context.</param>
        public SectionController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Save section.
        /// </summary>
        /// <param name="name">Section name.</param>
        /// <param name="projectId">Project id.</param>
        /// <param name="id">Section id.</param>
        [HttpPost]
        public IActionResult OnPost(string name, int projectId, int id = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                return this.BadRequest();
            }

            var project = this.dbContext.Projects.Single(p => p.Id == projectId);

            if (id == 0)
            {
                var section = new ProjectSection(name);
                section.Project = project;
                this.dbContext.ProjectSections.Add(section);
            }
            else
            {
                var section = this.dbContext.ProjectSections.Single(s => s.Id == id);
                section.Name = name;
            }

            this.dbContext.SaveChanges();

            return this.Ok();
        }
    }
}
