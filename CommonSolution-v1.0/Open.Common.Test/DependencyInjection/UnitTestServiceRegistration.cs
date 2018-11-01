using Open.Common.DependencyInjection;
using Open.Common.Utility;
//using Open.SPF.Utility.Dev;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.Common.Test.DependencyInjection
{
    public class UnitTestServiceRegistration : ServiceRegistration
    {
        protected override void DoConfigureServices(IServiceCollection services)
        {
            // For service registration tests only
            services.AddTransient<IUnitTestService, UnitTestServiceImpl>();
            services.AddTransient<ICompoundService, CompoundServiceImpl>();
        }
    }
}
