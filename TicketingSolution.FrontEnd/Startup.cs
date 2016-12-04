using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TicketingSolution.FrontEnd
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            //IoC configuration

            switch (Configuration.GetValue<string>("QueueWriter"))
            {
                case "WcfMsmq":
                    //WCF MSMQ
                    services.Configure<Messaging.QueueWriting.Msmq.MsmqWriterConfiguration>(Configuration.GetSection("MsmqWriterConfiguration"));
                    services.AddSingleton<Messaging.QueueWriting.Msmq.MsmqWriterConfiguration>((a) => a.GetService<IOptions<Messaging.QueueWriting.Msmq.MsmqWriterConfiguration>>().Value);
                    services.AddSingleton<
                        Messaging.QueueWriting.Interfaces.IQueueWriter,
                        Messaging.QueueWriting.Msmq.MsmqQueueWriter>();
                    break;
                case "NServiceBus":
                    //NServiceBus MSMQ
                    services.Configure<Messaging.QueueWriting.NServiceBus.NServiceBusWriterConfiguration>(Configuration.GetSection("NServiceBusWriterConfiguration"));
                    services.AddSingleton<Messaging.QueueWriting.NServiceBus.NServiceBusWriterConfiguration>((a) => a.GetService<IOptions<Messaging.QueueWriting.NServiceBus.NServiceBusWriterConfiguration>>().Value);
                    services.AddSingleton<
                        Messaging.QueueWriting.Interfaces.IQueueWriter,
                        Messaging.QueueWriting.NServiceBus.NServiceBusQueueWriter>();
                    break;
                case "SqlServiceBroker":
                    //SQL Service Broker
                    services.Configure<Messaging.QueueWriting.SqlServiceBroker.SqlServiceBrokerWriterConfiguration>(Configuration.GetSection("SqlServiceBrokerWriterConfiguration"));
                    services.AddSingleton<Messaging.QueueWriting.SqlServiceBroker.SqlServiceBrokerWriterConfiguration>((a) => a.GetService<IOptions<Messaging.QueueWriting.SqlServiceBroker.SqlServiceBrokerWriterConfiguration>>().Value);
                    services.AddSingleton<
                        Messaging.QueueWriting.Interfaces.IQueueWriter,
                        Messaging.QueueWriting.SqlServiceBroker.SqlServiceBrokerQueueWriter>();
                    break;
            }

            //Direct data access
            services.AddSingleton<
                DataAccess.Direct.Interfaces.IOrderRepository>((a) => new DataAccess.Direct.Impl.OrderRepository(Configuration["TicketingDataStore"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Order}/{action=Create}/{id?}");
            });


        }
    }
}
