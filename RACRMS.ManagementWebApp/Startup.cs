using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RACRMS.BusinessLayer.Abstract;
using RACRMS.BusinessLayer.Concrete;
using RACRMS.MailServiceShared.Statics;
using RACRMS.MailServiceWebApi.Consumers;
using RACRMS.ManagementWebApp.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RACRMS.ManagementWebApp
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
            services.AddTransient<ICarBrandBL, CarBrandBL>();
            services.AddTransient<ICarChassisTypeBL, CarChassisTypeBL>();
            services.AddTransient<ICarClassBL, CarClassBL>();
            services.AddTransient<ICarFuelTypeBL, CarFuelTypeBL>();
            services.AddTransient<ICarGearTypeBL, CarGearTypeBL>();
            services.AddTransient<ICarTypeBL, CarTypeBL>();
            services.AddTransient<IPreferenceBL, PreferenceBL>();
            services.AddTransient<IRequirementBL, RequirementBL>();
            services.AddTransient<IUserRoleBL, UserRoleBL>();
            services.AddTransient<IUserBL, UserBL>();
            services.AddTransient<ICarBL, CarBL>();
            services.AddTransient<ICarModelBL, CarModelBL>();
            services.AddTransient<ICarRentalPriceBL, CarRentalPriceBL>();
            services.AddTransient<ICustomerBL, CustomerBL>();
            services.AddTransient<ICarPreferenceBL, CarPreferenceBL>();
            services.AddTransient<ICarRentalRequirementBL, CarRentalRequirementBL>();
            services.AddTransient<IReservationBL, ReservationBL>();
            services.AddTransient<IPaymentTypeBL, PaymentTypeBL>();
            services.AddTransient<IContractBL, ContractBL>();

            services.AddAuthentication("Default").AddCookie("Default");

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<ReservationAcceptedMailSentEventConsumer>();
                configure.AddConsumer<ReservationRejectedMailSentEventConsumer>();

                configure.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(Configuration.GetConnectionString("RabbitMQConnectionString"));

                    configurator.ReceiveEndpoint(RabbitMQSettings.ReservationAcceptedMailSentEventQueue, configuration =>
                    {
                        configuration.ConfigureConsumer<ReservationAcceptedMailSentEventConsumer>(context);
                    });

                    configurator.ReceiveEndpoint(RabbitMQSettings.ReservationRejectedMailSentEventQueue, configuration =>
                    {
                        configuration.ConfigureConsumer<ReservationRejectedMailSentEventConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication().UseCookiePolicy();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Car/Error");
            }
            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Reservation}/{action=Index}/{id?}");
            });
        }
    }
}
