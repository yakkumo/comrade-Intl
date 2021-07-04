﻿#region

using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.BaseInterfaces
{
    public interface ISingleResultDto<TDto> : IResultDto
        where TDto : Dto
    {
        TDto Data { get; }
    }
}