using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerSearch.Search
{
    public static class SearchServiceLocator
    {
        private static IServiceProvider _serviceLocator;

        public static TService GetService<TService>()
        {
           return _serviceLocator.GetService<TService>();
        }

        internal static void SetServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceLocator = serviceProvider;
        }
    }
}
