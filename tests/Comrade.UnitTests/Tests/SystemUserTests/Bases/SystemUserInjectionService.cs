﻿#region

using AutoMapper;
using Comrade.Application.Services.SystemUserServices.Commands;
using Comrade.Application.Services.SystemUserServices.Queries;
using Comrade.Core.SystemUserCore.UseCases;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Infrastructure.DataAccess;
using Comrade.Infrastructure.Repositories;

#endregion

namespace Comrade.UnitTests.Tests.SystemUserTests.Bases
{
    public sealed class SystemUserInjectionService
    {
        public SystemUserCommand GetSystemUserCommand(ComradeContext context, IMapper mapper)
        {
            var uow = new UnitOfWork(context);
            var systemUserRepository = new SystemUserRepository(context);
            var passwordHasher = new PasswordHasher(new HashingOptions());

            var systemUserEditValidation =
                new SystemUserEditValidation(systemUserRepository);
            var systemUserDeleteValidation = new SystemUserDeleteValidation(systemUserRepository);

            var systemUserCreateUseCase =
                new SystemUserCreateUseCase(systemUserRepository, passwordHasher,
                    uow);
            var systemUserDeleteUseCase =
                new SystemUserDeleteUseCase(systemUserRepository, systemUserDeleteValidation, uow);
            var systemUserEditUseCase =
                new SystemUserEditUseCase(systemUserRepository, systemUserEditValidation, uow);

            return new SystemUserCommand(systemUserEditUseCase, systemUserCreateUseCase,
                systemUserDeleteUseCase,
                mapper);
        }

        public SystemUserQuery GetSystemUserQuery(ComradeContext context, IMapper mapper)
        {
            var systemUserRepository = new SystemUserRepository(context);

            return new SystemUserQuery(systemUserRepository, mapper);
        }
    }
}