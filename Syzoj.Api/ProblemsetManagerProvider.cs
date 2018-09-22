using System;
using System.Collections.Generic;
using Syzoj.Api.Services;

namespace Syzoj.Api
{
    public class ProblemsetManagerProvider
    {
        private static Dictionary<string, Type> providers = new Dictionary<string, Type>()
        {
            { "default", typeof(DefaultProblemsetManager) }
        };
        private readonly IServiceProvider serviceProvider;

        public ProblemsetManagerProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public IAsyncProblemsetPermissionManager GetProblemsetPermissionManager(string Name)
        {
            return (IAsyncProblemsetPermissionManager) serviceProvider.GetService(providers.GetValueOrDefault(Name));
        }
        public IAsyncProblemsetManager GetProblemsetManager(string Name)
        {
            return (IAsyncProblemsetManager) serviceProvider.GetService(providers.GetValueOrDefault(Name));
        }
    }
}