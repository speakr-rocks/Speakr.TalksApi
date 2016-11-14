using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Speakr.TalksApi.DataAccess;
using Speakr.TalksApi.DataAccess.DbAccess;
using Speakr.TalksApi.Swagger;
using Swashbuckle.Swagger.Model;

namespace Speakr.TalksApi.AppStart
{
    public class Startup
    {
        public Startup(IHostingEnvironment env) { }

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
            var configuration = Configuration.Configure();

            IoCRegistry.RegisterDependencies(services, configuration);
            SwaggerBootstrap.SetupSwagger(services);

            services.AddMvc();
        }
    }
}
