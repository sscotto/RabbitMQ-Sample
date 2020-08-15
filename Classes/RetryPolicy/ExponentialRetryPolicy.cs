using Classes.Logger;
using System;

namespace Classes.RetryPolicy
{
    public class ExponentialRetryPolicy : SimpleRetryPolicy
    {
        public ExponentialRetryPolicy(ILogger logger) : base(logger) { }
        protected override void WaitPolicyTime()
        {
            int sleepTime = CalculateExponentialSleepTime();
            _logger.Log($"Waiting {sleepTime} to try again");
            System.Threading.Thread.Sleep(sleepTime);
        }

        private int CalculateExponentialSleepTime()
        {
            return Convert.ToInt32(Math.Pow((double)_baseWaitingTime / 100, (double)_currentAttempt)) * 100;
        }
    }
}
