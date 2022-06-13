using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RACRMS.MailServiceShared.Abstract;
using RACRMS.MailServiceShared.Concrete;
using RACRMS.MailServiceShared.Statics;
using RACRMS.MailServiceWebApi.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.MailServiceWebApi
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
            services.AddTransient<IEmailHelper, EmailHelper>();

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<ReservationAcceptedEventConsumer>();
                configure.AddConsumer<ReservationRejectedEventConsumer>();
                configure.AddConsumer<PasswordRecoveryEmailEventConsumer>();

                configure.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(Configuration.GetConnectionString("RabbitMQConnectionString"));

                    configurator.ReceiveEndpoint(RabbitMQSettings.ReservationAcceptedEventQueue, configuration =>
                    {
                        configuration.ConfigureConsumer<ReservationAcceptedEventConsumer>(context);
                    });

                    configurator.ReceiveEndpoint(RabbitMQSettings.ReservationRejectedEventQueue, configuration =>
                    {
                        configuration.ConfigureConsumer<ReservationRejectedEventConsumer>(context);
                    });

                    configurator.ReceiveEndpoint(RabbitMQSettings.PasswordRecoveryEmailEventQueue, configuration =>
                    {
                        configuration.ConfigureConsumer<PasswordRecoveryEmailEventConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
