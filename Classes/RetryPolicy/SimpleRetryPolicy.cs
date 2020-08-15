using Classes.Exceptions;
using Classes.Logger;
using System;

namespace Classes.RetryPolicy
{
    public class SimpleRetryPolicy : IRetryPolicy
    {
        private int _maxAttempts = 5;
        protected int _currentAttempt;
        
        protected int _baseWaitingTime = 200;
        protected ILogger _logger;

        public SimpleRetryPolicy(ILogger logger)
        {
            _logger = logger;
        }

        public void Execute(Action action)
        {
            _currentAttempt = 1;
            do
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception ex)
                {
                    _logger.Log(ex.Message);
                    WaitPolicyTime();
                }
                _currentAttempt++;
            } while (_currentAttempt < _maxAttempts);

            if (_currentAttempt == _maxAttempts)
                throw new HandleMessageException("Can't Handle the Message");
                
        }

        protected virtual void WaitPolicyTime()
        {
            _logger.Log($"Waiting {_baseWaitingTime * 5} to try again");
            System.Threading.Thread.Sleep(_baseWaitingTime * 5);
            
        }
    }
}
