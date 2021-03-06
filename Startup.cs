using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_widepay.Entities;
using api_widepay.Interfaces;
using api_widepay.Repository;
using api_widepay.Services;
using api_widepay.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace api_widepay {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc (options => options.EnableEndpointRouting = false);
            //OR
            services.AddControllers (options => options.EnableEndpointRouting = false);
            services.AddDbContext<BancoContext> (options => options.UseLazyLoadingProxies ().UseMySql (Configuration.GetConnectionString ("DefaultConnection")));
            services.AddTransient<IWidePay, widepayService> ();
            services.AddTransient<IMysql, MysqlRepository> ();
            services.AddTransient<IFinanceiro, FinanceiroService> ();
            services.AddTransient<IGraph, GraphService> ();
            services.AddTransient<IParcelas, ParcelasRepository> ();
            services.AddTransient<IBoletoStorage, BoletoStorage> ();
            services.AddTransient<IVelocidadeService, VelocidadeService> ();
            services.AddTransient<IClienteService, ClienteService> ();
            services.AddCors (options => {
                options.AddPolicy ("CorsPolicy",
                    builder => builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ());
            });
           // services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            //app.UseHttpsRedirection ();
            app.UseCors ("CorsPolicy");
            app.UseMvc ();
            app.UseFileServer (new FileServerOptions () {
                RequestPath = new PathString ("/boletos"),
                    EnableDirectoryBrowsing = true
            });
        }
    }
}