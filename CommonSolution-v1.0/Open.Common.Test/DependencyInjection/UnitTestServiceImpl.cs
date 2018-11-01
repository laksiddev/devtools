using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.Common.Test.DependencyInjection
{
    public class UnitTestServiceImpl : IUnitTestService
    {
        public string UnitTestMethod()
        {
            return "SUCCESS";
        }
    }
}
