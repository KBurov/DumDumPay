using System;

using DumDumPay.API;
using DumDumPay.Utils;

using Microsoft.Extensions.DependencyInjection;

namespace DumDumPay.DI
{
    public static class Bootstrapper
    {
        private const string ExceptionMessage = "Bootstrapper hasn't been started!";

        private static readonly object _rootScopeSync = new();

        private static ServiceProvider _serviceProvider;

        public static void Start(string endPoint, string merchantId, string secretKey)
        {
            Ensure.ArgumentNotNullOrEmpty(endPoint, nameof(endPoint));
            Ensure.ArgumentNotNullOrEmpty(merchantId, nameof(merchantId));
            Ensure.ArgumentNotNullOrEmpty(secretKey, nameof(secretKey));
            
            if (_serviceProvider != null) return;

            lock (_rootScopeSync) {
                if (_serviceProvider != null) return;

                var services = new ServiceCollection();

                services.AddSingleton<IDumDumPayProvider>(s =>
                                                              new DumDumPayProvider(endPoint, merchantId, secretKey));

                _serviceProvider = services.BuildServiceProvider();
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