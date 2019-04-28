using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ryanair.Reservation.Application.Mediator;
using Ryanair.Reservation.Application.Profiles;
using Ryanair.Reservation.Domain.Interfaces;
using Ryanair.Reservation.Infrastructure.Repositories;
using Ryanair.Reservation.Middleware;
using Swashbuckle.AspNetCore.Swagger;

namespace Ryanair.Reservation
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
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            }).AddXmlSerializerFormatters();

            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<SingleInstanceFactory>(sp => sp.GetService);
            services.AddTransient<MultiInstanceFactory>(sp => sp.GetServices);
            services.AddMediatorHandlers(typeof(Startup).Assembly);

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RyanairProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Adding repositories as singleton because we are using in memory collections.
            services.AddSingleton<IRepository<Domain.Entities.FlightAggregate.Flight>, FlightRepository>();
            services.AddSingleton<IRepository<Domain.Entities.ReservationAggregate.Reservation>, ReservationRepository>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Ryanair Reservation", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseUnhandledExceptionMiddleware();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReservationAPI");
            });

            app.UseMvc();
        }
    }
}
