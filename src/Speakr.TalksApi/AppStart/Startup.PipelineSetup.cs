using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Speakr.TalksApi
{
    public partial class Startup
    {
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
    }
}
