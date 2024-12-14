using bks.domain.Data.Appsettings;
using bks.domain.Interfaces;
using bks.domain.Interfaces.Repository;
using bks.domain.Interfaces.Service;
using bks.inftasturcture.Repository;
using bks.inftasturcture.Service;
using BKS.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;

namespace bks.api.Utilities
{
    public static class ServiceExtensions
    {
        public static void ConfigureServiceResolver(this IServiceCollection services)
        {
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IPackageRepository, PackageRepository>();
			services.AddScoped<IClassScheduleRepository, ClassScheduleRepository>();
			services.AddScoped<IClassScheduleService, ClassScheduleService>();
			services.AddScoped<IPackageService, PackageService>();
			services.AddScoped<ICacheService, RedisCacheService>();
			//services.AddScoped<IBookingService, BookingService>();
			services.AddScoped<IUserService, UserService>();

			services.AddScoped<UserService>();
			services.AddScoped<JwtTokenHelper>();
		}
		public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
			services.Configure<JwtSettings>(configuration.GetSection("RedisSetting"));
		}
		public static void ConfigureLoggerService(this IServiceCollection services)
        {
            //services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking System (BKS)", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Pagination"));
            });
        }
        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
			// Add authentication
			var jwtSettings = new JwtSettings();
			configuration.Bind("JwtSettings", jwtSettings);
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtSettings.Issuer,
						ValidAudience = jwtSettings.Audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
					};
				});

			// Add authorization
			services.AddAuthorization();
		}

		public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
		{
			var redisSettings = new RedisSetting();
			configuration.Bind("RedisSetting", redisSettings);
			var redisConnectionString = configuration.GetConnectionString("Redis");

			services.AddStackExchangeRedisCache(options =>
			{
                options.Configuration = redisSettings.Configuration;
                options.InstanceName = redisSettings.InstanceName;
			});

			var redis = ConnectionMultiplexer.Connect(redisSettings.Configuration);
			services.AddSingleton<IConnectionMultiplexer>(redis);
		}

	}
}
