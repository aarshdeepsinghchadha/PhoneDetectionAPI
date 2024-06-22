using PhoneNumberDetection.Common.Interface;
using PhoneNumberDetection.Services;

namespace PhoneNumberDetection.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IPhoneNumberDetector, PhoneNumberDetector>();
        }
    }
}
