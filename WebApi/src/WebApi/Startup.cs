using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using NLog.Extensions.Logging;
using NLog.Web;
using WebApi.Models;
using WebApi.Owin;

namespace WebApi
{
	public sealed class Startup
	{
		public Startup(IHostingEnvironment environment)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(environment.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true);

			_configuration = builder.Build();

			var nlogConfigPath = _configuration.GetSection("nlogConfigPath").Value;
			environment.ConfigureNLog(nlogConfigPath);
		}

		private readonly IConfigurationRoot _configuration;

		private IContainer _applicationContainer;

		private Bootstrapper _bootstrapper;

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			var sqlConnectionString = _configuration.GetConnectionString(nameof(ApiDbContext));
			services.AddDbContext<ApiDbContext>(__x => __x
				.UseNpgsql(sqlConnectionString, __options => __options.MigrationsAssembly(nameof(WebApi))));

			var builder = new ContainerBuilder();
			builder.Populate(services);
			_applicationContainer = builder.Build();

			return new AutofacServiceProvider(_applicationContainer);
		}

		public void Configure(IApplicationBuilder applicationBuilder, IApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddNLog();

			_bootstrapper = new Bootstrapper(_applicationContainer);

			applicationBuilder
				.UseOwin()
				.UseNancy(__options => __options.Bootstrapper = _bootstrapper);

			applicationBuilder.AddNLogWeb();

			applicationLifetime.ApplicationStopped.Register(() =>
			{
				_bootstrapper.Dispose();
				_applicationContainer.Dispose();
			});
		}
	}
}