﻿#region

using System.Threading.Tasks;
using Comrade.Domain.Models;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests
{
    public class SystemUserContextTests
    {
        private readonly SystemUserInjectionController _systemUserInjectionController = new();

        [Fact]
        public async Task SystemUser_Context()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_SystemUser_Context")
                .Options;

            SystemUser systemUser = null;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);
            var repository = new SystemUserRepository(context);
            systemUser = await repository.GetById(1);
            Assert.NotNull(systemUser);
        }
    }
}