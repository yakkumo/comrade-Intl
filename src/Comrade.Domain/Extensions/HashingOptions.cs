﻿#region

using System;

#endregion

namespace comrade.Domain.Extensions
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 100;

        public static implicit operator HashingOptions(int v)
        {
            throw new NotImplementedException();
        }
    }
}