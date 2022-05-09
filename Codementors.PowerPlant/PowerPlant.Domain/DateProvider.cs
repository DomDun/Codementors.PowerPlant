using System;

namespace PowerPlantCzarnobyl.Domain
{
    public interface IDateProvider
    {
        DateTime Now { get; }
    }

    public class DateProvider : IDateProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
