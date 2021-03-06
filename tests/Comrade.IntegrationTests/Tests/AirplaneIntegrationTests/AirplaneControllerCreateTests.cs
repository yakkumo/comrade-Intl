#region

using System.Linq;
using System.Threading.Tasks;
using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests
{
    public sealed class AirplaneControllerCreateTests
    {
        private readonly AirplaneInjectionController _airplaneInjectionController = new();

        [Fact]
        public async Task AirplaneController_Create()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_AirplaneController_Create")
                .EnableSensitiveDataLogging().Options;


            var testObject = new AirplaneCreateDto
            {
                Code = "123",
                Model = "234",
                PassengerQuantity = 456
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            var airplaneController = _airplaneInjectionController.GetAirplaneController(context);
            var result = await airplaneController.Create(testObject);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(200, actualResultValue?.Code);
            }

            Assert.Equal(1, context.Airplanes.Count());
        }

        [Fact]
        public async Task AirplaneController_Create_Error()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_AirplaneController_Create_Error")
                .EnableSensitiveDataLogging().Options;


            var testObject = new AirplaneCreateDto
            {
                Code = "123",
                PassengerQuantity = 456
            };

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            var airplaneController = _airplaneInjectionController.GetAirplaneController(context);
            var result = await airplaneController.Create(testObject);

            if (result is OkObjectResult okResult)
            {
                var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(400, actualResultValue?.Code);
            }

            Assert.Equal(0, context.Airplanes.Count());
        }

        [Fact]
        public async Task UcAirplaneCreate_Test_Exception()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseSqlServer("error")
                .EnableSensitiveDataLogging().Options;

            var testObject = new AirplaneCreateDto
            {
                Code = "123",
                Model = "234",
                PassengerQuantity = 456
            };

            await using var context = new ComradeContext(options);

            var airplaneController = _airplaneInjectionController.GetAirplaneController(context);
            var result = await airplaneController.Create(testObject);

            if (result is ObjectResult objectResult)
            {
                var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
                Assert.NotNull(actualResultValue);
                Assert.Equal(500, actualResultValue?.Code);
            }
        }
    }
}