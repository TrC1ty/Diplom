// <copyright file="Add.cshtml.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Web.Areas.Admin.Pages.Employees
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Diplom.Core.Services;
    using Diplom.Web.Pages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// The Add model.
    /// </summary>
    public class Add : AdminBasePageModel
    {
        private readonly EmployeeService employeeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Add"/> class.
        /// </summary>
        /// <param name="employeeService">Employee service.</param>
        public Add(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        /// <summary>
        /// Gets or sets email.
        /// </summary>
        [BindProperty]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets pswd.
        /// </summary>
        [BindProperty]
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether employee is created.
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public bool EmployeeCreated { get; set; }

        /// <summary>
        /// Post method.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<IActionResult> OnPost()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                this.ModelState.AddModelError(nameof(this.Email), "Поле 'Email' не может быть пустым.");
            }
            else
            {
                if (!MailAddress.TryCreate(this.Email, out _))
                {
                    this.ModelState.AddModelError(nameof(this.Email), "Необходимо указать верный Email.");
                }
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.ModelState.AddModelError(nameof(this.Password), "Поле 'Пароль' не может быть пустым.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var employee = await this.employeeService.CreateEmployeeAsync(this.Email!, this.Email!,  null, this.Password!);
            employee.EmailConfirmed = true;
            this.DataContext.SaveChanges();

            this.EmployeeCreated = true;

            return this.Page();
        }
    }
}
