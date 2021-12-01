using System;

using Microsoft.Extensions.DependencyInjection;

namespace DumDumPay.DI
{
    public class Bootstrapper
    {
        private const string ExceptionMessage = "Bootstrapper hasn't been started!";

        private static readonly object _rootScopeSync = new();

        private static ServiceProvider _serviceProvider;

        public static void Start()
        {
            if (_serviceProvider != null) return;

            lock (_rootScopeSync) {
                if (_serviceProvider != null) return;

                //
            }
        }

        public static void Stop()
        {
            lock (_rootScopeSync) {
                _serviceProvider?.Dispose();

                _serviceProvider = null;
            }
        }

        public static T Resolve<T>()
        {
            lock (_rootScopeSync) {
                if (_serviceProvider == null)
                    throw new InvalidOperationException(ExceptionMessage);

                return _serviceProvider.GetService<T>();
            }
        }
    }
}