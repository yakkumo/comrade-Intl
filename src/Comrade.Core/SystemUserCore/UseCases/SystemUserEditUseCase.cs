﻿#region

using System;
using System.Threading.Tasks;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore.UseCases
{
    public class SystemUserEditUseCase : Service
    {
        private readonly ISystemUserRepository _repository;
        private readonly SystemUserEditValidation _systemUserEditValidation;

        public SystemUserEditUseCase(ISystemUserRepository repository,
            SystemUserEditValidation systemUserEditValidation, IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _systemUserEditValidation = systemUserEditValidation;
        }

        public async Task<ISingleResult<SystemUser>> Execute(SystemUser entity)
        {
            var isValid = ValidateEntity(entity);
            if (!isValid.Success)
            {
                return isValid;
            }

            var result = await _systemUserEditValidation.Execute(entity).ConfigureAwait(false);
            if (!result.Success) return result;

            var obj = result.Data!;

            HydrateValues(obj, entity);

            _repository.Update(obj);

            _ = await Commit().ConfigureAwait(false);

            return new EditResult<SystemUser>();
        }

        private static void HydrateValues(SystemUser target, SystemUser source)
        {
            target.Name = source.Name;
            target.Email = source.Email;
            target.Registration = source.Registration;
        }
    }
}