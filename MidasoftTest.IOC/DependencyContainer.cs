using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MidasoftTest.Data.Repositories;
using MidasoftTest.Data.Repositories.Interface;
using MidasoftTest.Domain.Interface;
using MidasoftTest.Domain.Services;
using MidasoftTest.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidasoftTest.IOC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IFamilyGroupService, FamilyGroupService>();
            services.AddScoped<IFamilyGroupRepository, FamilyGroupRepository>();
            services.AddScoped<IUsersService, UsersService>();
            return services;
        }
        public static IServiceCollection AddValidations(this IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<FamilyGroupValidator>());
            return services;
        }
    }
}
