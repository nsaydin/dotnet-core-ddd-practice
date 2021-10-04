using System;

namespace Core.WorkTime
{
    public class WorkTime : IWorkTime
    {
        private long Ticks { get; set; }

        public WorkTime()
        {
            Ticks = 0;
        }

        public DateTime Now()
        {
            return DateTime.Now.Date.AddTicks(Ticks);
        }

        public DateTime Increase(int hour)
        {
            Ticks += TimeSpan.FromHours(hour).Ticks;

            return Now();
        }

        public void AddTick()
        {
            Ticks += 1;
        }
    }
}