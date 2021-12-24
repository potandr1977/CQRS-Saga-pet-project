using MassTransit;
using MessageApprover.Commands.DataAccess;
using MessageApprover.Commands.DataAccess.Abstractions;
using MessageApprover.Commands.Services;
using MessageApprover.Commands.Services.Abstractions;
using MessageApprover.Commands.Api.Consumers;
using MessageApprover.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Reflection;
using MediatR;
using MessageApprover.Commands.handlers;

namespace MessageApprover.Commands.Api
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
            var handlersAssembly = typeof(CreateAuthorCommandHandler).GetTypeInfo().Assembly;
            services.AddMediatR(Assembly.GetExecutingAssembly(),handlersAssembly);

            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Durable = true;
                    cfg.PrefetchCount = 1;
                    cfg.PurgeOnStartup = true;
                    cfg.Host(RabbitSettings.RabbitMqUri, hst =>
                    {
                        hst.Username(RabbitSettings.UserName);
                        hst.Password(RabbitSettings.Password);
                    });

                    cfg.ReceiveEndpoint(RabbitSettings.SagaQueue, e =>
                    {
                        e.Consumer(() => new WaitForApproveConsumer(services));
                    });
                }));
            });

            services.AddHostedService<MassTransitConsoleHostedService>();

            services.AddSingleton<IMongoClient>(s => 
                new MongoClient(MongoSettings.ConnectionString)
            );
            
            services.AddSingleton<IAuthorDao, AuthorDao>();
            services.AddSingleton<IEnteredMessageDao, EnteredMessageDao>();
            services.AddSingleton<IAuthorCommandService, AuthorCommandService>();
            services.AddSingleton<IEnteredMessagesCommandService, EnteredMessagesCommandService>();
           
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MessageApprover.Commands.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MessageApprover.Commands.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
