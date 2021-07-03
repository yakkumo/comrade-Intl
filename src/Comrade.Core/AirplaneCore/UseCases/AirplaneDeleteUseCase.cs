﻿#region

using System;
using System.Threading.Tasks;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Helpers.Bases;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Helpers.Models.Results;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.UseCases
{
    public class AirplaneDeleteUseCase : Service
    {
        private readonly AirplaneDeleteValidation _airplaneDeleteValidation;
        private readonly IAirplaneRepository _repository;

        public AirplaneDeleteUseCase(IAirplaneRepository repository, AirplaneDeleteValidation airplaneDeleteValidation,
            IUnitOfWork uow)
            : base(uow)
        {
            _repository = repository;
            _airplaneDeleteValidation = airplaneDeleteValidation;
        }

        public async Task<ISingleResult<Airplane>> Execute(int id)
        {
            try
            {
                var validate = await _airplaneDeleteValidation.Execute(id);
                if (!validate.Success) return validate;

                _repository.Remove(id);

                var success = await Commit();
            }
            catch (Exception)
            {
                return new SingleResult<Airplane>(BusinessMessage.MSG07);
            }

            return new DeleteResult<Airplane>();
        }
    }
}