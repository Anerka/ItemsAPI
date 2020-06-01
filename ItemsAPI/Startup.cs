using ItemsAPI.Context;
using ItemsAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace ItemsAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o=>
                {
                    o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    o.EnableEndpointRouting = false;
                });
            services.AddControllersWithViews().AddNewtonsoftJson();
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();

#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            var connectionString = _configuration["connectionString:itemInfoDBConnectionString"];

            services.AddDbContext<ItemInfoContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });

            services.AddScoped<IItemInfoRepository, ItemInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseHttpsRedirection();
            app.UseRouting();
        }
    }
}