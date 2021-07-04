﻿#region

using System;
using Comrade.Core.Helpers.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;

#endregion

namespace Comrade.Core.Helpers.Models.Results
{
    public class EditResult<TEntity> : SingleResult<TEntity>
        where TEntity : Entity
    {
        public EditResult()
        {
            Code = (int) EnumResultadoAcao.Success;
            Success = true;
            Message = BusinessMessage.ResourceManager.GetString("MSG02");
        }

        public EditResult(bool success, string message)
        {
            Code = success ? (int) EnumResultadoAcao.Success : (int) EnumResultadoAcao.ErroNaoEncontrado;
            Success = success;
            Message = message;
        }

        public EditResult(Exception ex)
        {
            Code = (int) EnumResultadoAcao.ErroServidor;
            Success = false;
            ExceptionMessage = ex.Message;
            Message = BusinessMessage.ResourceManager.GetString("MSG07");
        }
    }
}