using System;

namespace Classes.RetryPolicy
{
    public interface IRetryPolicy
    {
        void Execute(Action action);
    }
}
