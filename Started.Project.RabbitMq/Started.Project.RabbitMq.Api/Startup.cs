using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Started.Project.RabbitMq.Domain.Handler.Commands.Demonstration;
using Started.Project.RabbitMq.Domain.Interface;
using Started.Project.RabbitMq.Domain.Repositories.Demonstration;
using Started.Project.RabbitMq.Domain.Service;
using Started.Project.RabbitMq.Infra.DataContexts;
using Started.Project.RabbitMq.Infra.Respositories.Demonstration;
using Started.Project.RabbitMq.Shared;
using VueCliMiddleware;

namespace Started.Project.RabbitMq.Api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Configura e define as origens permitidas para comunicao (CORS)
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("www.google.com");
                                  });
            });

            services.AddControllers();

            //Conectar ao projeto client-app - VUEJS
            services.AddSpaStaticFiles(options => options.RootPath = "client-app/dist");

            //Injeção de dependencias
            services.AddScoped<DataContext, DataContext>();

            services.AddTransient<IDemoRepository, DemoRepository>();

            services.AddTransient<DemoHandler, DemoHandler>();

            services.AddTransient<IDocumentService, DocumentService>();

            //Configuração do Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Started.Project.RabbitMq", Version = "v1" });
            });

            //Carrega classe Settings que é estatica pelo sistema
            Settings.VirtualDirectory = Configuration["VirtualDirectory"];
            Settings.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Habilita o CORS
            app.UseCors(MyAllowSpecificOrigins);

            //Configura interface do Swagger que é responsável por documentar a API
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "My API");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "client-app";
                }
                else
                {
                    spa.Options.SourcePath = "dist";
                }

                if (env.IsDevelopment())
                    spa.UseVueCli(npmScript: "serve");
            });

        }
    }
}
