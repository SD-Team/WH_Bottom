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
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<HPDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HPConnection")));
            services.AddControllers();
            //Auto Mapper
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IMapper>(sp =>
            {
                return new Mapper(AutoMapperConfig.RegisterMappings());
            });
            services.AddSingleton(AutoMapperConfig.RegisterMappings());
             // Repository
            services.AddScoped<IPackingListRepository, PackingListRepository>();
            services.AddScoped<ICodeIDDetailRepo, CodeIDDetailRepo>();
            services.AddScoped<IRackLocationRepo, RackLocationRepo>();
            services.AddScoped<IQRCodeMainRepository, QRCodeMainRepository>();
            services.AddScoped<IPackingListDetailRepository, PackingListDetailRepository>();
            services.AddScoped<IQRCodeDetailRepository, QRCodeDetailRepository>();
            services.AddScoped<IHPMaterialRepository, HPMaterialRepository>();
            services.AddScoped<IHPStyleRepository, HPStyleRepository>();
            services.AddScoped<IHPVendorRepository, HPVendorRepository>();
            services.AddScoped<IMaterialPurchaseRepository, MaterialPurchaseRepository>();
            services.AddScoped<IMaterialMissingRepository, MaterialMissingRepository>();

            // Service
            services.AddScoped<IPackingListService, PackingListService>();
            services.AddScoped<ICodeIDDetailService, CodeIDDetailService>();
            services.AddScoped<IRackLocationService, RackLocationService>();
            services.AddScoped<IQRCodeMainService, QRCodeMainService>();
            services.AddScoped<IPackingListDetailService, PackingListDetailService>();
            services.AddScoped<IQRCodeDetailService, QRCodeDetailService>();
            services.AddScoped<IHPMaterialService, HPMaterialService>();
            services.AddScoped<IHPStyleService, HPStyleService>();
            services.AddScoped<IHPVendorService, HPVendorService>();
            services.AddScoped<IReceivingService, ReceivingService>();
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
