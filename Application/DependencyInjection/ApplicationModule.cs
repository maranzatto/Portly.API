using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Portly.Application.Ports.Input.Resident;
using Portly.Application.Ports.Input.Visitor;
using Portly.Application.UseCases.Auth;
using Portly.Application.UseCases.Resident;
using Portly.Application.UseCases.Visitor;

namespace Portly.Application.DependencyInjection
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Visitor Use Cases
            services.AddScoped<ICreateVisitorUseCase, CreateVisitorUseCase>();
            services.AddScoped<IUpdateVisitorUseCase, UpdateVisitorUseCase>();
            services.AddScoped<IGetVisitorByIdUseCase, GetVisitorByIdUseCase>();
            services.AddScoped<IGetAllVisitorsUseCase, GetAllVisitorsUseCase>();
            services.AddScoped<IDeleteVisitorUseCase, DeleteVisitorUseCase>();
            services.AddScoped<IRestoreVisitorUseCase, RestoreVisitorUseCase>();

            // Resident Use Cases
            services.AddScoped<ICreateResidentUseCase, CreateResidentUseCase>();
            services.AddScoped<IUpdateResidentUseCase, UpdateResidentUseCase>();
            services.AddScoped<IGetResidentByIdUseCase, GetResidentByIdUseCase>();
            services.AddScoped<IGetAllResidentsUseCase, GetAllResidentsUseCase>();
            services.AddScoped<IDeleteResidentUseCase, DeleteResidentUseCase>();
            services.AddScoped<IRestoreResidentUseCase, RestoreResidentUseCase>();

            // Auth Use Cases
            services.AddScoped<LoginUseCase>();
            services.AddScoped<RegisterUserUseCase>();

            return services;
        }
    }
}

