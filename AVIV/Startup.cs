using System.Reflection;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using AVIV.Infrastructure;
using AVIV.Core;
using AVIV.Infrastructure.Data;
using Ardalis.ListStartupServices;
using Microsoft.OpenApi.Models;
using AVIV.API.Filters;
using FluentValidation.AspNetCore;

namespace AVIV.API
{
    public class Startup
	{
		private readonly IWebHostEnvironment _env;

		public Startup(IConfiguration config, IWebHostEnvironment env)
		{
			Configuration = config;
			_env = env;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<Seeder>();

			//services.AddSingleton<IFileStorageService, AzureFileStorageService>();

			services.AddApplication();
			services.AddInfrastructure(Configuration);


			//services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<AppDbContext>();

			services
				.AddControllers(
					options => options
						.Filters
						.Add<ApiExceptionFilterAttribute>())
						.AddFluentValidation(x => x.AutomaticValidationEnabled = false
				);

            services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { 
					Title = "AVIV API", 
					Version = "v1" 
				});
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,
						},
						new List<string>()
					}
				});
				c.EnableAnnotations();
			});

			// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
			services.Configure<ServiceConfig>(config =>
			{
				config.Services = new List<ServiceDescriptor>(services);

				// optional - default path to view services is /listallservices - recommended to choose your own path
				config.Path = "/listservices";
			});

			services.Configure<FormOptions>(options =>
			{
				options.ValueCountLimit = int.MaxValue;
			});

			// Customise default API behaviour
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seeder)
		{
			//app.UseAllElasticApm(Configuration);


			if (env.EnvironmentName == "Development")
			{
				app.UseDeveloperExceptionPage();
				app.UseShowAllServicesMiddleware();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}


			app.UseHealthChecks("/health");
			app.UseStaticFiles();

			app.UseSerilogRequestLogging();
			app.UseRouting();

			//app.UseAuthentication();
			//app.UseAuthorization();

			app.UseHttpsRedirection();
			app.UseStaticFiles();


			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AVIV API V1"));

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});

			seeder.Seed().Wait();
		}
	}
}
