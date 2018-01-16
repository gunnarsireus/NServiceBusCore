using System;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Server.Requesthandler;
using Server.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;


namespace Server
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		IContainer ApplicationContainer { get; set; }
		IConfigurationRoot Configuration { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			var dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
			dbContextOptionsBuilder.UseSqlite("DataSource=App_Data/Car.db");

			var builder = new ContainerBuilder();
			builder.Populate(services);
			builder.RegisterType<DbContextOptionsBuilder<CarApiContext>>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			builder.RegisterType<CreateCarRequestHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			builder.RegisterType<CreateCompanyRequestHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			builder.RegisterType<DeleteCarRequestHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			builder.RegisterType<DeleteCompanyRequestHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			builder.RegisterType<GetCarRequestHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			builder.RegisterType<GetCarsRequestHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			builder.RegisterType<GetCompanyRequestHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			builder.RegisterType<GetCompaniesRequestHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			builder.RegisterType<UpdateCarRequestHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			builder.RegisterType<UpdateCompanyRequestHandler>().AsSelf().WithParameter("dbContextOptionsBuilder", dbContextOptionsBuilder);
			ApplicationContainer = builder.Build();

			IEndpointInstance serverEndpoint = null;
			var endpoint = serverEndpoint;
			builder.Register(c => endpoint)
				.As<IEndpointInstance>()
				.SingleInstance();

			var endpointConfiguration = new EndpointConfiguration("NServiceBusCore.Server");
			endpointConfiguration.EnableCallbacks(makesRequests: false);
			endpointConfiguration.UsePersistence<LearningPersistence>();
			endpointConfiguration.UseTransport<LearningTransport>();
			endpointConfiguration.UseContainer<AutofacBuilder>(
				customizations: customizations =>
				{
					customizations.ExistingLifetimeScope(ApplicationContainer);
				});

			Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
			return new AutofacServiceProvider(ApplicationContainer);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();
			appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
		}
	}
}