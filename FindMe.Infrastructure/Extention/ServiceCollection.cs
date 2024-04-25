using FindMe.Application.Interfaces.Authentication;
using FindMe.Application.Interfaces.Services;
using FindMe.Infrastructure.Services;
using FindMe.Infrastructure.Services.Authentication;
using FindMe.Infrastructure.Services.Localization;
using FindMe.Infrastructure.Services.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;


namespace FindMe.Infrastructure.Extention
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddLocalization()
                    .AddCollections();

            services.Configure<MailSettings>(configuration.GetSection(MailSettings.Section));

            return services;
        }

        private static IServiceCollection AddLocalization(this IServiceCollection services)
        {
            LocalizationServiceCollectionExtensions.AddLocalization(services);


            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

            services.AddMvc().AddDataAnnotationsLocalization(op =>
            {
                op.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(JsonStringLocalizerFactory));
            }
             );

            services.Configure<RequestLocalizationOptions>(op =>
            {
                var supportedCultures = new[] {
                  new CultureInfo("ar-EG"),
                  new CultureInfo("en-US"),
                 };
                op.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0]);

                op.SupportedCultures = supportedCultures;
            });

            return services;
        }
        private static IServiceCollection AddCollections(this IServiceCollection services)
        {
            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddTransient<IMailingService, MailingService>();
            services.AddTransient<IHumanDetectionService,HumanDetectionService>();
            return services;
        }


    }
}
