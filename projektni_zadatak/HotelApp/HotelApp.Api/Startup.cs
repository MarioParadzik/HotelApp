using HotelApp.Api.DbContexts;
using HotelApp.Api.Entities;
using HotelApp.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;
using System.Reflection;
using Microsoft.OpenApi.Models;
using HotelApp.Api.Helpers;
using HotelApp.Api.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API
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
            if (Configuration["Provider"] == "MySQL")
            {
                services.AddDbContext<HotelDbContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("MySQL"), ServerVersion.AutoDetect(Configuration.GetConnectionString("MySQL")),
                    x => x.MigrationsAssembly("HotelAppMySQL.Migrations")));
            }
            else if (Configuration["Provider"] == "MSSQL")
            {
                services.AddDbContext<HotelDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MSSQL"), x => x.MigrationsAssembly("HotelAppMSSQL.Migrations")));
            }
            else
            {
                throw new ArgumentException("Not a valid database type");
            }

           services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
           services.AddCors(); 
           services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                })
                .AddJwtBearer("JwtBearer",jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AuthKey:key").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });
            services.AddIdentityCore<User>(options => options.User.RequireUniqueEmail = false)
                .AddRoles<Role>()
                .AddEntityFrameworkStores<HotelDbContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ISortHelper<Room>, SortHelper<Room>>();
            services.AddScoped<ISortHelper<Reservation>, SortHelper<Reservation>>();
            services.AddScoped<IExternalApiClient, ExternalApiClient>();
            services.AddHttpClient("ExternalApi", c => c.BaseAddress = new Uri(Configuration.GetSection("ExternalUri:uri").Value));
            services.AddScoped<ISyncReservationRepository, SyncReservationRepository>();
            services.AddHostedService<TimedHostedService>();

            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
                apiBehaviorOptions.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(new
                    {
                        Code = 400,
                        Error = actionContext.ModelState.Values.SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)
                    });
                });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware(typeof(ErrorHandlerMiddleware));
            }
            app.UseCors(app => app.WithOrigins(Configuration.GetSection("CorsAllowedOrigins")?.GetChildren()?.Select(x => x.Value)?.ToArray()).AllowAnyMethod().AllowAnyHeader());
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel.App v1");
            });


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware(typeof(ErrorHandlerMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
