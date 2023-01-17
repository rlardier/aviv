using AVIV.SharedKernel.Interfaces;
using System;

namespace AVIV.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTimeOffset Now => DateTime.Now;
    }
}
