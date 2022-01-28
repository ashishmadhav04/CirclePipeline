using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CirclePipeline.BusinessLayer;
using CirclePipeline.BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CirclePipeline
{
    public class Startup
    {

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssembly(Assembly.Load("CirclePipeline.Model"));
                s.ImplicitlyValidateRootCollectionElements = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Circle CI Pipeline", Version = "v1" });
            });

            services.AddHealthChecks();
            services.AddHttpClient();
            services.AddTransient<ICirclePipelineManagementService, CirclePipelineManagementService>();
            services.AddTransient<IGitPipelineManagementService, GitPipelineManagementService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Circle CI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
