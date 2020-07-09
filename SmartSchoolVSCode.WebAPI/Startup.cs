using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchoolVSCode.WebAPI.Data;

namespace SmartSchoolVSCode.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SmartContext>(context => context.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddControllers()
                    .AddNewtonsoftJson(
                        option => option.SerializerSettings.ReferenceLoopHandling 
                               = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());                   

            //services.AddSingleton<IRepository, Repository>(); 
            //Cria uma única instância do serviço qunado é solicitado pela primeira vez e reutiliza essa mesma instância
            //em todos os locais em que esse serviço é necessárioo

            //services.AddTransient<IRepository, Repository>();
            //Sempre gerará uma nova instância para cada  item encontrado que possua ta dependência, 
            //ou seja, se houver 5 dependências serão 5 intâncias diferentes 

            // services.AddScoped<IRepository, Repository>() -> Ele é diferente do Transsient que garante que em uma requisão seja criada um instância
            // de uma classe onde houver outras dependências, seja utilizada essa única intância pra todas, 
            //renovando somente nas requisioções subsequentes, mas mantendo essa obrigatoriedade

        
         services.AddScoped<IRepository, Repository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
