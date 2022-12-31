using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReservationApi.Contracts.Interfaces;
using ReservationApi.Contracts.Services;
using ReservationApi.Data;
using ReservationApi.Data.Seeding;
using System.Reflection;
using System.Text;

namespace ReservationApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            WebHostEnvironment = environment;
        }

        public void ConfigureDatabase(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            if (WebHostEnvironment.IsProduction())
            {
                services.AddDbContext<ReservationApiDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }
            else if(WebHostEnvironment.IsDevelopment())
            {
                services.AddDbContext<ReservationApiDbContext>(options =>
                    options.UseInMemoryDatabase(connectionString));
                services.AddScoped<DbInitialiser>();
            }
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityApiUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ReservationApiDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddScoped<IUserManagerService, UserManagerService>();
            services.AddScoped<ISittingsService, SittingService>();
        }

        public void ConfigureMapster(IServiceCollection services)
        {
            //Maybe? services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetEntryAssembly()!);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
        }

        public void SeedDatabase(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitialiser>();
                dbInitializer.Initialise();
            }
        }

        public void ConfigureControllers(IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddControllers(config =>
            {
                config.CacheProfiles.Add("30SecondsCaching", new CacheProfile
                {
                    Duration = 30
                });
            });
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new MediaTypeApiVersionReader("version"),
                    new HeaderApiVersionReader("X-Version"),
                    new UrlSegmentApiVersionReader()
                    );
                options.ReportApiVersions= true;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Reservation API",
                    Version = "v1",
                    Description = "Reservation API Services.",
                    Contact = new OpenApiContact
                    {
                        Name = "Michal Maciejewski."
                    },
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI. using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public void ConfigureAuthentication(IServiceCollection services)
        {
            var jwtConfig = Configuration.GetSection("Jwt");
            var secretKey = jwtConfig["Key"];
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["Issuer"],
                    ValidAudience = jwtConfig["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });
        }
    }
}
