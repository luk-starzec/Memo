using Memo.DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Memo.DataAccess
{
    public static class ServiceCollectionExtension
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            services.AddTransient<IMemosRepo, MemosRepo>();
        }
    }
}
