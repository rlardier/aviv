using System;

namespace AVIV.SharedKernel.Interfaces
{
    public interface IDateTime
    {
        DateTimeOffset Now { get; }
    }
}
