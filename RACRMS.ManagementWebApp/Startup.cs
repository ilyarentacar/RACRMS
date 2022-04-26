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
using RACRMS.MailServiceWebAPI.Consumers;
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
            services.AddScoped<ICarBrandBL, CarBrandBL>();
            services.AddScoped<ICarChassisTypeBL, CarChassisTypeBL>();
            services.AddScoped<ICarClassBL, CarClassBL>();
            services.AddScoped<ICarFuelTypeBL, CarFuelTypeBL>();
            services.AddScoped<ICarGearTypeBL, CarGearTypeBL>();
            services.AddScoped<ICarTypeBL, CarTypeBL>();
            services.AddScoped<IPreferenceBL, PreferenceBL>();
            services.AddScoped<IRequirementBL, RequirementBL>();
            services.AddScoped<IUserRoleBL, UserRoleBL>();
            services.AddScoped<IUserBL, UserBL>();
            services.AddScoped<ICarBL, CarBL>();
            services.AddScoped<ICarModelBL, CarModelBL>();
            services.AddScoped<ICarRentalPriceBL, CarRentalPriceBL>();
            services.AddScoped<ICustomerBL, CustomerBL>();
            services.AddScoped<ICarPreferenceBL, CarPreferenceBL>();
            services.AddScoped<ICarRentalRequirementBL, CarRentalRequirementBL>();
            services.AddScoped<IReservationBL, ReservationBL>();
            services.AddScoped<IPaymentTypeBL, PaymentTypeBL>();
            services.AddScoped<IContractBL, ContractBL>();

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
