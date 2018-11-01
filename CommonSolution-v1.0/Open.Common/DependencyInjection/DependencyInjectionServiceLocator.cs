using System;
using System.Collections.Generic;
using System.Configuration;

using CommonServiceLocator;
using Microsoft.Extensions.DependencyInjection;

namespace Open.Common.DependencyInjection
{
    public class DependencyInjectionServiceLocator : ServiceLocatorImplBase
    {
        private IServiceProvider _container;
        private static DependencyInjectionServiceLocator _instance;

        public DependencyInjectionServiceLocator(IServiceProvider container)
        {
            _container = container;
        }

        public static DependencyInjectionServiceLocator Initialize()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            return Initialize(serviceCollection);
        }

        public static DependencyInjectionServiceLocator Initialize(IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException("Services", "A Service Collection is required for initialization.");

            Open.Common.Utility.TraceUtility.WriteInformationTrace(typeof(DependencyInjectionServiceLocator), "Initialze", Open.Common.Utility.TraceUtility.TraceType.Begin);

            ServiceRegistration serviceRegistration = new ServiceRegistration();
            serviceRegistration.ConfigureServices(services);

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            DependencyInjectionServiceLocator permissionServiceLocator = new DependencyInjectionServiceLocator(serviceProvider);

            services.AddSingleton(typeof(IServiceLocator), permissionServiceLocator);

            object test = permissionServiceLocator.GetInstance<IServiceLocator>();
            if (test == null)
            {
                Open.Common.Utility.TraceUtility.LogWarningMessage("Unity configuration did not contain an element for IServiceLocator.");
            }

            //test = instance.GetInstance<Open.D3DB.Core.IPermissionCache>();
            //if (test == null)
            //{
            //    Open.Common.Utility.EventLogUtility.LogWarningMessage("Unity configuration did not contain an element for IPermissionCache. Registering a default instance instead.");
            //    container.RegisterInstance(typeof(IPermissionCache), new PermissionCache(), new PerThreadLifetimeManager());
            //}

            //Open.Common.Utility.TraceUtility.WriteTrace(typeof(PermissionServiceLocator), "Initialze", null, String.Format("Locator instance {0} NULL.", ((instance != null) ? "is NOT" : "is")), Open.Common.Utility.TraceUtility.TraceType.End);
            //return instance;
            _instance = permissionServiceLocator;

            return permissionServiceLocator;
        }

        public static IServiceLocator LocatorInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = DependencyInjectionServiceLocator.Initialize();
                }

                return _instance;
            }
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            try
            {
               Open.Common.Utility.TraceUtility.WriteInformationTrace(typeof(DependencyInjectionServiceLocator), "DoGetInstance(Type, string)", "_container.Resolve(serviceType, key)", null, Open.Common.Utility.TraceUtility.TraceType.Begin);
                object instance = _container.GetService(serviceType);
                Open.Common.Utility.TraceUtility.WriteInformationTrace(typeof(DependencyInjectionServiceLocator), "DoGetInstance(Type, string)", "_container.Resolve(serviceType, key)", null, Open.Common.Utility.TraceUtility.TraceType.End);
                return instance;
            }
            catch (Exception ex) // { /* ignore - return null */ }
            {
                Open.Common.Utility.TraceUtility.LogWarningMessage(String.Format("An error occurred while attempting to find the requested instance of type: {0}. \r\n\r\n{1}", serviceType.FullName, Open.Common.Utility.TraceUtility.FormatExceptionMessage(ex)));
            }

            return null;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            try
            {
                Open.Common.Utility.TraceUtility.WriteInformationTrace(typeof(DependencyInjectionServiceLocator), "DoGetAllInstances(Type)", "_container.ResolveAll(serviceType)", null, Open.Common.Utility.TraceUtility.TraceType.Begin);
                IEnumerable<object> instances = _container.GetServices(serviceType);
                Open.Common.Utility.TraceUtility.WriteInformationTrace(typeof(DependencyInjectionServiceLocator), "DoGetAllInstances(Type)", "_container.ResolveAll(serviceType)", null, Open.Common.Utility.TraceUtility.TraceType.End);
                return instances;
            }
            catch (Exception ex) // { /* ignore - return null */ }
            {
                Open.Common.Utility.TraceUtility.LogWarningMessage(String.Format("An error occurred while attempting to find the requested instance of type: {0}. \r\n\r\n{1}", serviceType.FullName, Open.Common.Utility.TraceUtility.FormatExceptionMessage(ex)));
            }

            return null;
        }
    }
}
