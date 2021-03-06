#region

using System;
using System.Threading.Tasks;
using Comrade.Domain.Models;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Comrade.UnitTests.Tests.AirplaneTests.TestDatas;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Comrade.UnitTests.Tests.AirplaneTests
{
    public sealed class UcAirplaneCreateTests

    {
        private readonly ITestOutputHelper _output;
        private readonly UcAirplaneInjection _ucAirplaneInjection = new();

        public UcAirplaneCreateTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [ClassData(typeof(AirplaneCreateTestData))]
        public async Task UcAirplaneCreate_Test(int expected, Airplane testObjectInput)
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_UcAirplaneCreate_Test")
                .EnableSensitiveDataLogging().Options;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();

            var airplaneCreate = _ucAirplaneInjection.GetUcAirplaneCreate(context);
            var result = await airplaneCreate.Execute(testObjectInput);

            Assert.Equal(expected, result.Code);
        }

        [Fact]
        public async Task UcAirplaneCreate_Test_Error()
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseSqlServer("error")
                .EnableSensitiveDataLogging().Options;

            var testObject = new Airplane
            {
                Id = 1,
                Code = "123",
                Model = "234",
                PassengerQuantity = 456
            };

            await using var context = new ComradeContext(options);

            var ucAirplaneCreate = _ucAirplaneInjection.GetUcAirplaneCreate(context);
            try
            {
                var result = await ucAirplaneCreate.Execute(testObject);
                Assert.True(false);
            }
            catch (Exception e)
            {
                Assert.NotEmpty(e.Message);
            }
        }
    }
}