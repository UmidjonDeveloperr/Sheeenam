
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Sheeenam.Api.Brokers.DateTimes;
using Sheeenam.Api.Brokers.Logging;
using Sheeenam.Api.Brokers.Storages;
using Sheeenam.Api.Services.Foundation.Guests;

namespace Sheeenam.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<StorageBroker>();
			services.AddControllers();
			AddBrokers(services);
			AddFoundationservices(services);
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sheeenam.Api", Version = "v1" });
			});
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sheeenam.Api v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private static void AddBrokers(IServiceCollection services)
		{
			services.AddTransient<IStorageBroker, StorageBroker>();
			services.AddTransient<ILoggingBroker, LoggingBroker>();
			services.AddTransient<IDateTimeBroker, DateTimeBroker>();
		}

		private static void AddFoundationservices(IServiceCollection services)
		{
			services.AddTransient<IGuestService, GuestService>();
		}
	}
}
