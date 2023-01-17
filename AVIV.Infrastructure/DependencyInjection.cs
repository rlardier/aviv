using AVIV.Core.Common.Interfaces;
using AVIV.Infrastructure.Data;
using AVIV.Infrastructure.Services;
using AVIV.SharedKernel.Interface;
using AVIV.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AVIV.Infrastructure
{
	public static class DependencyInjection
	{

		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(ServiceLifetime.Scoped);
			services.AddTransient<IApplicationDbContext>(provider => provider.GetService<AppDbContext>());

			var coreAssembly = Assembly.GetAssembly(typeof(Core.DependencyInjection));
			var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository<>));
			services.AddMediatR(coreAssembly, infrastructureAssembly);


			services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
			services.AddScoped(typeof(IReadRepository<>), typeof(EFReadRepository<>));

			services.AddScoped<IDomainEventService, DomainEventService>();
			services.AddMemoryCache();

			services.AddTransient<IDateTime, DateTimeService>();


			return services;
		}
	}
}