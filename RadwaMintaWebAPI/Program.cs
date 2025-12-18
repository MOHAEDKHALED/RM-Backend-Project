
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.Data.SeedingData;
using RadwaMintaWebAPI.Interfaces;
using RadwaMintaWebAPI.MappingProfiles;
using RadwaMintaWebAPI.Models.DbContexts;
using RadwaMintaWebAPI.Services;
using System.Text;
using System.Threading.Tasks;

namespace RadwaMintaWebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container.
            //builder.Services.AddControllers();
            builder.Services
                   .AddControllers()
                   .AddJsonOptions(options =>
                   {
                       options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                   });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RadwaMintaWebAPI", Version = "v1" });

                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
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
                        new string[] {}
                    }
                });
            });
            builder.Services.AddDbContext<MintaDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
           );
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>(); 
            builder.Services.AddScoped<IMediaService, MediaService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IQualityService, QualityService>();
            builder.Services.AddScoped<IExperienceService, ExperienceService>();
            builder.Services.AddScoped<ITokenService, TokenService>(); 
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            //builder.Services.AddAutoMapper(X => X.AddProfile(new ProductProfile()));
            builder.Services.AddAutoMapper(X => X.AddProfile(new MediaProfile()));
            //builder.Services.AddAutoMapper(X => X.AddProfile(new ReviewProfile()));
            //builder.Services.AddScoped<PictureUrlProducts>();
            //builder.Services.AddScoped<PictureUrlQuality>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IContactService, ContactService>();


            #endregion

            #region Authentication
            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
               .AddJwtBearer(options =>
               {
                   var jwtSettings = builder.Configuration.GetSection("Jwt");
                   options.SaveToken = true;
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,

                       ValidIssuer = builder.Configuration["Jwt:Issuer"],
                       ValidAudience = builder.Configuration["Jwt:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // <<<<<< ????? ?? ??? Key ?? 100%
                                                                                                                             // ClockSkew = TimeSpan.Zero // ???? ????? ?? ?? ????? ??? ????? ?? ??? ?????? ???????? (??? testing)
                   };

                   options.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           Console.WriteLine("Authentication failed: " + context.Exception.Message);
                           return Task.CompletedTask;
                       },
                       OnTokenValidated = context =>
                       {
                           Console.WriteLine("Token validated for: " + context.Principal.Identity.Name);
                           return Task.CompletedTask;
                       },
                       OnChallenge = context =>
                       {
                           Console.WriteLine("OnChallenge: " + context.ErrorDescription);
                           return Task.CompletedTask;
                       }
                   };
               });
            #endregion

            builder.Services.AddAuthorization();

            #region Add CORS policy for frontend development
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            }); 
            #endregion

            var app = builder.Build();



            #region Seeding Data
            using var Scope = app.Services.CreateScope();
            var DataSeedingObject = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await DataSeedingObject.DataSeedAsync();
            #endregion

            // MiddleWares
            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() | true)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("AllowAllOrigins");
            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
