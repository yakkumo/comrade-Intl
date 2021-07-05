#region

using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Dtos;
using Comrade.Infrastructure.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.AuthenticationTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.TokenIntegrationTests
{
    public sealed class TokenControllerGenerateTokenTests
    {
        private readonly TokenInjectionController _tokenInjectionController = new();

        [Fact]
        public async Task TokenController_GenerateToken()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_TokenController_GenerateToken")
                .Options;


            var testObject = new AuthenticationDto
            {
                Key = "1",
                Password = "123456",
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            Utilities.InitializeDbForTests(context);

            var tokenController = _tokenInjectionController.GetTokenController(context);
            var result = await tokenController.GenerateToken(testObject);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as SingleResultDto<UserDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue?.Code);
            }
        }
    }
}