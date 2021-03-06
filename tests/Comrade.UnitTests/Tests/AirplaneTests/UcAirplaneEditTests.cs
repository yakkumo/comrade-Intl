#region

using System;
using System.Threading.Tasks;
using Comrade.Domain.Models;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Comrade.UnitTests.Tests.AirplaneTests.TestDatas;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Comrade.UnitTests.Tests.AirplaneTests
{
    public sealed class UcAirplaneEditTests

    {
        private readonly ITestOutputHelper _output;
        private readonly UcAirplaneInjection _ucAirplaneInjection = new();

        public UcAirplaneEditTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [ClassData(typeof(AirplaneEditTestData))]
        public async Task UcAirplaneEdit_Test(int expected, Airplane testObjectInput)
        {
            var options = new DbContextOptionsBuilder<ComradeContext>()
                .UseInMemoryDatabase("test_database_UcAirplaneEdit_Test" + testObjectInput.Id)
                .EnableSensitiveDataLogging().Options;

            await using var context = new ComradeContext(options);
            await context.Database.EnsureCreatedAsync();
            InjectDataOnContextBase.InitializeDbForTests(context);

            var ucAirplaneEdit = _ucAirplaneInjection.GetUcAirplaneEdit(context);
            var result = await ucAirplaneEdit.Execute(testObjectInput);

            Assert.Equal(expected, result.Code);
        }

        [Fact]
        public async Task UcAirplaneEdit_Test_Error()
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

            var ucAirplaneEdit = _ucAirplaneInjection.GetUcAirplaneEdit(context);
            try
            {
                var result = await ucAirplaneEdit.Execute(testObject);
                Assert.True(false);
            }
            catch (Exception e)
            {
                Assert.NotEmpty(e.Message);
            }
        }
    }
}