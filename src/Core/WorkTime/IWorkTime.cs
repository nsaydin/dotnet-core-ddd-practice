using System;

namespace Core.WorkTime
{
    public interface IWorkTime
    {
        DateTime Now();

        DateTime Increase(int hour);
        
        void AddTick();
    }
}