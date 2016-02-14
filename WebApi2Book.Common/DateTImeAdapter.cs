using System;

namespace WebApi2Book.Common
{
    public class DateTImeAdapter : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}