using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bottom_API._Repositories.Interfaces;
using Bottom_API._Repositories.Repositories;
using Bottom_API._Services.Interfaces;
using Bottom_API._Services.Services;
using Bottom_API.Data;
using Bottom_API.Helpers.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bottom_API
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
            services.AddCors();
            services.AddDbContext<WMS_DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SHC_WMS_Connection")));
            services.AddDbContext<HP_DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HP_Basis_Connection")));
            services.AddControllers();
            //Auto Mapper
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMapper>(sp =>
            {
                return new Mapper(AutoMapperConfig.RegisterMappings());
            });
            services.AddSingleton(AutoMapperConfig.RegisterMappings());

<<<<<<< HEAD

            // Repository
            services.AddScoped<IHPVendorU01Repository, HPVendorU01Repository>();
            services.AddScoped<IQRCodeMainRepository, QRCodeMainRepository>();
            services.AddScoped<IPackingListRepository, PackingListRepository>();

            // Service
            services.AddScoped<IHPVendorU01Service, HPVendorU01Service>();
            services.AddScoped<IQRCodeMainService, QRCodeMainService>();
            services.AddScoped<IPackingListService, PackingListService>();
=======
            services.AddScoped<ICodeIDDetailRepo, CodeIDDetailRepo>();
            services.AddScoped<IRackLocationRepo, RackLocationRepo>();

            services.AddScoped<ICodeIDDetailService, CodeIDDetailService>();
<<<<<<< HEAD
            services.AddScoped<IRackLocationService, RackLocationService>();
=======
>>>>>>> 0033a102b758854207a06eb4a4c64f8f840c216f
>>>>>>> 405c6313f5cb067789bc6d1b39ca9f798af529e0
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
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
