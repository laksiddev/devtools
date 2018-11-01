using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Open.Common.Configuration;

namespace Open.Common.DependencyInjection
{
    public class ServiceRegistration
    {
        private string _serviceRegistrationTypeString;
        private Type _serviceRegistrationType;

        public ServiceRegistration()
        {
            _serviceRegistrationTypeString = null;
            _serviceRegistrationType = null;
        }

        public ServiceRegistration(string serviceRegistrationTypeString)
        {
            _serviceRegistrationTypeString = serviceRegistrationTypeString;
            _serviceRegistrationType = null;
        }

        public ServiceRegistration(Type serviceRegistrationType)
        {
            _serviceRegistrationTypeString = null;
            _serviceRegistrationType = serviceRegistrationType;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ServiceRegistration serviceRegistrationInstance = ServiceRegistrationInstance;
            serviceRegistrationInstance.DoConfigureServices(services);
        }

        public ServiceRegistration ServiceRegistrationInstance
        {
            get
            {
                Type serviceRegistrationType;

                if (_serviceRegistrationType != null)
                {
                    serviceRegistrationType = _serviceRegistrationType;
                }
                else
                {
                    string serviceRegistrationTypeString = ((_serviceRegistrationTypeString != null) ? _serviceRegistrationTypeString : CommonConfiguration.Item(CommonConstants.AppSettings.ServiceRegistrationType));
                    if (serviceRegistrationTypeString == null)
                    {
                        throw new ArgumentNullException("ServiceRegistrationType", "No value was provided for the service registration type string.");
                    }
                    else if (String.IsNullOrWhiteSpace(serviceRegistrationTypeString))
                    {
                        throw new ArgumentOutOfRangeException("ServiceRegistrationType", "An empty service registration type string was provided.");
                    }
                    else if (serviceRegistrationTypeString.Contains("Analysts.SkillsInventory.DependencyInjection.ServiceRegistration"))
                    {
                        throw new ArgumentNullException("ServiceRegistrationType", "The configuration for the service registration could not be found.");
                    }

                    serviceRegistrationType = Type.GetType(serviceRegistrationTypeString);

                    if (serviceRegistrationType == null)
                    {
                        throw new ArgumentOutOfRangeException("ServiceRegistrationType", String.Format("The type for the configured service registration could not be found: {0}", serviceRegistrationTypeString));
                    }
                }

                if (serviceRegistrationType == null)
                {
                    throw new ArgumentNullException("ServiceRegistrationType", "No value was provided for the service registration type.");
                }

                if (!(typeof(ServiceRegistration).IsAssignableFrom(serviceRegistrationType)))
                {
                    throw new ArgumentOutOfRangeException("ServiceRegistrationType", String.Format("The type for the configured service registration is not capable of performing service registration: {0}", serviceRegistrationType.FullName));
                }

                ConstructorInfo serviceRegistrationTypeConstructor = serviceRegistrationType.GetConstructor(new Type[] { });
                if (serviceRegistrationTypeConstructor == null)
                {
                    throw new InvalidOperationException(String.Format("Unable to obtain the default constructor for the service registration type: {0}", serviceRegistrationType.FullName));
                }

                object serviceRegistrationObject = serviceRegistrationTypeConstructor.Invoke(new Type[] { });
                if (serviceRegistrationObject == null)
                {
                    throw new InvalidOperationException(String.Format("Unable to instantiate an object for configured service registration type: {0}", serviceRegistrationType.FullName));
                }

                if (!(serviceRegistrationObject is ServiceRegistration))
                {
                    throw new ArgumentOutOfRangeException("ServiceRegistrationType", String.Format("An incorrect service registration type was configured: {0}", serviceRegistrationType.FullName));
                }

                return (ServiceRegistration)serviceRegistrationObject;
            }
        }

        // This is the method where all local types are registered.
        protected virtual void DoConfigureServices(IServiceCollection services) { throw new NotImplementedException("The implementation for DoRegisterServices must be provided in the implementation of the child class."); }
    }
}
