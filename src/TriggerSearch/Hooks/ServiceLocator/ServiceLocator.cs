using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Core.Hooks.ServiceLocator
{
    public static class ServiceLocator
    {
        private static IServiceProvider _serviceLocator;

        public static TService GetService<TService>()
        {
            return _serviceLocator.GetService<TService>();
        }

        public static void SetServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceLocator = serviceProvider;
        }
    }
}
