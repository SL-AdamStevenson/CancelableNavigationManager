using Microsoft.Extensions.DependencyInjection;
using NavManagerExample.Client.Services;

namespace Microsoft.AspNetCore.Components.WebAssembly.Hosting
{
    public static class CancelableNavigationManagerExtensions
    {
        public static void AddCancelableNavigationManager(this WebAssemblyHostBuilder builder)
        {
            ServiceDescriptor descriptor = null;

            foreach (var serviceRegistration in builder.Services)
            {
                if (serviceRegistration.ServiceType.FullName == "Microsoft.AspNetCore.Components.NavigationManager")
                {
                    if (serviceRegistration.ImplementationInstance != null)
                    {
                        descriptor = serviceRegistration;

                        break;
                    }

                }
            }

            if (descriptor != null)
            {
                builder.Services.Remove(descriptor);

                var cancelableNavigationManager = new CancelableNavigationManager((Microsoft.AspNetCore.Components.NavigationManager)descriptor.ImplementationInstance);

                builder.Services.AddSingleton<Microsoft.AspNetCore.Components.NavigationManager>(sp => cancelableNavigationManager);

                builder.Services.AddSingleton<CancelableNavigationManager>(sp => cancelableNavigationManager);
            }
        }
    }
}
