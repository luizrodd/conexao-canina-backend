using ConexaoCaninaApp.Application.Interfaces;
using ConexaoCaninaApp.Application.Services;
using ConexaoCaninaApp.Infra.Data.Interfaces;
using ConexaoCaninaApp.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ConexaoCaninaApp.Infra.IoC
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddProjectServices(this IServiceCollection services)
		{
			services.AddScoped<INotificacaoService, NotificacaoService>();
            return services;
		}
	}
}
