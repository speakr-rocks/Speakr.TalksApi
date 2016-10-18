using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.Swagger.Model;

namespace Speakr.TalksApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            MakeDbConnectionString();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
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
            });
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

        private void MakeDbConnectionString()
        {
            var test = Configuration["SSSSSSS"];
            var dbServer = Configuration["SPEAKR_DB_SERVER"];
            var dbName = Configuration["SPEAKR_DB_NAME"];
            var dbUserName = Configuration["SPEAKR_DB_USER"];
            var dbPassword = Configuration["SPEAKR_DB_PASSWORD"];

            Configuration["ConnectionString"] =
                $"Server={dbServer};Database={dbName};Uid={dbUserName};Pwd={dbPassword};";
        }
    }
}
