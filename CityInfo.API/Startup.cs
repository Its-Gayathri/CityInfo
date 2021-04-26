﻿using CityInfo.API.Contexts;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CityInfo.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());//to configure output format
                });

            //services.AddMvc().AddJsonOptions(o =>   // to serialize to /deserialize from Json by default
            //{
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }
            //});
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            //var connectionString = @".\SQLExpress;Database=CityInfoDB;Trusted_Connection=True;";
            var connectionString = @"data source=SGZ-IN01191\SQLEXPRESS;initial catalog=CityInfoDB;trusted_connection=true;";
           // var connectionString = @"Data Source=.\sqlexpress;InitialCatalog=AddressDb;IntegratedSecurity=True";
            services.AddDbContext<CityInfoContext>(o=>
                {
                    o.UseSqlServer(connectionString);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages(); //StatusCodePages middleware --> text-based handler for common status code will be added

            app.UseMvc(); //MVC middleware will handle Http Requests in req pipeline
            
        }
    }
}
