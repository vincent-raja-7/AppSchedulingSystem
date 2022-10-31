using AppointmentSchedSys.Models;
using AppointmentSchedSys.Repository;
using AppointmentSchedSys.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSchedSys
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppointmentSchedSys", Version = "v1" });
            });


            services.AddCors(options =>
            {
                options.AddPolicy("MyCors", opts =>
                {
                    opts.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });



            services.AddDbContext<AppSysContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("dbCon"));
            });
            services.AddScoped<ILogin, LoginService>();
            services.AddScoped<IToken, TokenService>();
            services.AddScoped<IRepo<string, User>, UserService>();
            services.AddScoped<IRepo<DateTime, Bookings>, BookingService>();
            services.AddScoped<IRepo<int, AppointmentEntries>, AppEntryService>();
            services.AddScoped<IFindRepo, AppEntryService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppointmentSchedSys v1"));
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseCors("MyCors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
