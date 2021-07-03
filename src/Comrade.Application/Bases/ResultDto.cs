﻿#region

using System.Collections.Generic;
using Comrade.Application.Utils;

#endregion

namespace Comrade.Application.Bases
{
    public class ResultDto : IResultDto
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public IList<string> Mensagens { get; set; }
    }
}