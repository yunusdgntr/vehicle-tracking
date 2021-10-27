using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Vehicle.Tracking.Business.Services.Abstract;
using Vehicle.Tracking.Business.Services.Concrete;
using Vehicle.Tracking.DataAccess.Abstract;
using Vehicle.Tracking.DataAccess.Concrete.EntityFramework;
using Vehicle.Tracking.DataAccess.Concrete.EntityFramework.Contexts;
using Vehicle.Tracking.Entities.Models.Common;
using Vehicle.Tracking.WebApi.Helpers;

namespace Vehicle.Tracking.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<BearerTokensOptions>(Configuration.GetSection("BearerTokens"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Vehicle.Tracking.WebApi",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                     new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        }
                     },new string[] {
                     }
                    }
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<VehicleTrackDbContext, MsDbContext>(ServiceLifetime.Transient);
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRightRepository, RightRepository>();
            services.AddTransient<ILocationHistoryRepository, LocationHistoryRepository>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IRightManager, RightManager>();
            services.AddTransient<IAntiForgeryCookieManager, AntiForgeryCookieManager>();
            

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                var key = Encoding.ASCII.GetBytes(Configuration["BearerTokens:Key"]);

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, 
                    IssuerSigningKey = new SymmetricSecurityKey(key), 
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });


            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("", "");
                });
            });


            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicle.Tracking.WebApi v1"));
            }
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<VehicleTrackDbContext>();
                context.Database.EnsureCreated();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
