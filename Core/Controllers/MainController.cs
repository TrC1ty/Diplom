// <copyright file="MainController.cs" company="Test Company">
// Copyright © 2023 Test Company
// </copyright>

namespace Diplom.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using Diplom.Core.Data;
    using Diplom.Core.Data.Entities;
    using Diplom.Core.Data.Enums;
    using Diplom.Core.Diagnostics;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Main controller.
    /// </summary>
    public class MainController
    {
        private readonly ApplicationDbContext dataContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<MainController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        /// <param name="dataContext">Data context.</param>
        /// <param name="userManager">Identity framework user manager.</param>
        /// <param name="logger">Logging service.</param>
        public MainController(ApplicationDbContext dataContext, UserManager<ApplicationUser> userManager, ILogger<MainController> logger)
        {
            this.dataContext = dataContext;
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// Generates strong random string to be used for password.
        /// </summary>
        /// <param name="passwordLength">Length of password to be generated.</param>
        /// <returns>Random string of specified length.</returns>
        public static string CreateRandomPassword(int passwordLength)
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";

            var randomRytes = new byte[passwordLength * 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomRytes);
            }

            var passwordCharArray = new char[passwordLength];
            for (var i = 0; i < passwordLength; i++)
            {
                var value = BitConverter.ToUInt64(randomRytes, i * 8);
                passwordCharArray[i] = allowedChars[(int)(value % (uint)allowedChars.Length)];
            }

            return new string(passwordCharArray);
        }

        /// <summary>
        /// Get participant name.
        /// </summary>
        /// <param name="participant">Participant.</param>
        /// <returns>Participant name.</returns>
        public static string GetParticipantName(Participant participant)
        {
            if (participant is NaturalPerson)
            {
                var person = (NaturalPerson)participant;
                return $"{person.Surname} {person.Name} {person.Patronymic}";
            }
            else if (participant is LegalEntity)
            {
                var entity = (LegalEntity)participant;
                return entity.LegalName!;
            }
            else
            {
                var enterpreneur = (IndividualEntrepreneur)participant;
                return enterpreneur.LegalName!;
            }
        }

        /// <summary>
        /// Get participant status.
        /// </summary>
        /// <param name="participant">Participant.</param>
        /// <returns>Participant type.</returns>
        public static string GetParticipantStatus(Participant participant)
        {
            var name = participant.ParticipantType switch
            {
                ParticipantType.Type1 => "Застройщик",
                ParticipantType.Type2 => "Лицо, осуществляющее строительство",
                ParticipantType.Type3 => "Лицо, осуществляющее подготовку проектной документации",
                ParticipantType.Type4 => "Лицо, выполнившее работы",
                ParticipantType.Type5 => "Представитель застройщика",
                ParticipantType.Type6 => "Представитель лица, осуществляющего строительство",
                ParticipantType.Type7 => "Представитель лица по вопросам строительного контроля",
                ParticipantType.Type8 => "Представитель лица, осуществляющего подготовку проектной документации",
                ParticipantType.Type9 => "Представитель лица, выполнившего работы, подлежащие освидетельствованию",
                _ => "Иные представители лиц",
            };

            return name;
        }

        /// <summary>
        /// Get participant type.
        /// </summary>
        /// <param name="participant">Participant.</param>
        /// <returns>Participant type.</returns>
        public static int GetParticipantType(Participant participant)
        {
            if (participant is NaturalPerson)
            {
                return 0;
            }
            else if (participant is LegalEntity)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        /// <summary>
        /// Adds new user. Saves changes to the database.
        /// </summary>
        /// <param name="userName">User name of the new user.</param>
        /// <param name="userEmail">New user's email address.</param>
        /// <param name="password">Password to be set for the new user.</param>
        /// <param name="role">Name of role to assign to new user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with just created user in Task's result.</returns>
        public async Task<ApplicationUser> AddNewUserAsync(string userName, string userEmail, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("User name cannot be empty.", nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(userEmail) && userName != ApplicationRole.System)
            {
                throw new ArgumentException("User email cannot be empty.", nameof(userEmail));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Password cannot be empty.", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("Role name cannot be empty.", nameof(role));
            }

            var newUser = new ApplicationUser()
            {
                UserName = userName,
                Email = userEmail,
                DateTimeCreated = DateTime.UtcNow,
            };

            // Register with Identity Core and save.
            var identityResult = await this.userManager.CreateAsync(newUser, password);
            EnsureIdentityResultIsSucceeded(identityResult);

            // Add role to the user.
            identityResult = await this.userManager.AddToRoleAsync(newUser, role);
            EnsureIdentityResultIsSucceeded(identityResult);

            return newUser;
        }

        /// <summary>
        /// Adds SYSTEM user to database. Saves changes to the database.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with just created user in Task's result.</returns>
        public async Task<ApplicationUser> CreateSystemUserAsync()
        {
            var newUser = new ApplicationUser()
            {
                UserName = ApplicationRole.System,
                DateTimeCreated = DateTime.UtcNow,
            };

            // Register with Identity Core and save.
            var identityResult = await this.userManager.CreateAsync(newUser, CreateRandomPassword(16) + "aA0_");
            EnsureIdentityResultIsSucceeded(identityResult);

            // Add role to the user.
            identityResult = await this.userManager.AddToRoleAsync(newUser, ApplicationRole.System);
            EnsureIdentityResultIsSucceeded(identityResult);

            return newUser;
        }

        /// <summary>
        /// Checks passed <see cref="IdentityResult"/> and if it's not successfull throws <see cref="GeneralException"/>.
        /// </summary>
        /// <param name="result">Result object to check.</param>
        internal static void EnsureIdentityResultIsSucceeded(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errorsString = string.Join(", ", result.Errors.Select(e => $"[{e.Code}: {e.Description}]"));
                throw new GeneralException($"Cannot perform identity operation. Error(s): {errorsString}.");
            }
        }
    }
}
