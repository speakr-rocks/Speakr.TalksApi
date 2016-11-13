using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;
using Speakr.TalksApi.Swagger;
using Swashbuckle.Swagger.Model;

namespace Speakr.TalksApi.AppStart
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            GenerateConfigs();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterDependencies(services);

            SetupSwagger(services);

            services.AddMvc();
        }

        private void GenerateConfigs()
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            MakeDbConnectionString();
        }

        private void MakeDbConnectionString()
        {
            var dbServer = Configuration["SPEAKR_DB_SERVER"];
            var dbName = Configuration["SPEAKR_DB_NAME"];
            var dbUserName = Configuration["SPEAKR_DB_USER"];
            var dbPassword = Configuration["SPEAKR_DB_PASSWORD"];
            const string allowUserVariables = "Allow User Variables=True";

            Configuration["DbConnectionString"] =
                $"Server={dbServer};Database={dbName};Uid={dbUserName};Pwd={dbPassword};{allowUserVariables}";
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton(provider => Configuration);
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IDapper, DataAccess.DbAccess.Dapper>();
        }

        private static void SetupSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen();

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Speakr.TalksApi",
                    Description = "API for the Speakr.App",
                    TermsOfService = "None"
                });
                options.OperationFilter<ApplySwaggerDescriptionFilterAttributes>();
            });
        }
    }
}
