using FindMe.Application.Interfaces.Authentication;
using FindMe.Application.Interfaces.Repositories;
using FindMe.Application.Interfaces.Services;
using FindMe.Domain.Identity;
using FindMe.Presistance.Context;
using FindMe.Presistance.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;


namespace FindMe.Presistance.Extention
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddPresistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration)
            .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddTransient<IUnitOfWork, UnitOfWork>();
            //.AddTransient<ISupplierRepository, SupplierRepository>()
            //.AddTransient<IStockRepository, StockRepository>();


            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseLazyLoadingProxies().UseSqlServer(connectionString,
                   builder =>
                   {
                       builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                   }));
            // Identity configuration
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddUserManager<UserManager<ApplicationUser>>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddDefaultTokenProviders();


            return services;
        }


    }
}
