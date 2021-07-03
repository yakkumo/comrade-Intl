﻿#region

using System;
using System.Reflection;
using Comrade.Domain.Models;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Extensions;

#endregion

namespace Comrade.UnitTests.Helpers
{
    public static class Utilities
    {
        private const string JsonPath = "Comrade.Infrastructure.SeedData";

        #region DadosIniciais

        public static void InitializeDbForTests(ComradeContext db)
        {
            try
            {
                var assembly = Assembly.GetAssembly(typeof(JsonUtilities));

                if (assembly is not null)
                {
                    db.Airplanes.AddRange(
                        JsonUtilities.GetListFromJson<Airplane>(
                            assembly.GetManifestResourceStream($"{JsonPath}.airplane.json")));

                    db.UserSystems.AddRange(
                        JsonUtilities.GetListFromJson<UserSystem>(
                            assembly.GetManifestResourceStream($"{JsonPath}.userSystem.json")));
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion
    }
}