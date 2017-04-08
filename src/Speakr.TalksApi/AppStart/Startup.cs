using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Speakr.TalksApi.AppStart
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; set; }
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Environment = env;
            Configuration = AppConfiguration.Configure(Environment);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            var options = new JwtBearerOptions
            {
                Audience = Configuration["Auth0:TalksApiIdentifier"],
                Authority = $"https://{Configuration["Auth0:Domain"]}/"
            };
            app.UseJwtBearerAuthentication(options);

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IoCRegistry.RegisterDependencies(services, Configuration);
            SwaggerBootstrap.SetupSwagger(services);

            services.AddMvc();
        }
    }
}
