using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open.Common.Test.DependencyInjection
{
    public class CompoundServiceImpl : ICompoundService
    {
        private IUnitTestService _internalService;
        public CompoundServiceImpl(IUnitTestService internalService)
        {
            _internalService = internalService;
        }

        public string CompoundMethod()
        {
            if (_internalService == null)
                throw new ArgumentNullException("InternalService");

            return _internalService.UnitTestMethod();
        }

        public string InternalServiceName()
        {
            if (_internalService == null)
                throw new ArgumentNullException("InternalService");

            return _internalService.GetType().FullName;
        }
    }
}
