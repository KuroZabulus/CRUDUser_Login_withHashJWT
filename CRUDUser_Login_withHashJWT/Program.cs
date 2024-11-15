using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.AutoMapper;
using Repository.Data;
using Repository.DTO.ViewModel.MailSender;
using Repository.TokenHandler;
using Service;
using System.Text;

namespace CRUDUser_Login_withHashJWT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Email service
            builder.Services.Configure<SmtpEmail>(builder.Configuration.GetSection("SmtpEmail"));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Swagger generator
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Load appsettings.json configuration
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Add CursusDbContext with SQL Server configuration
            builder.Services.AddDbContext<TestDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add AutoMapper for mapping between objects
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            //JWT
            builder.Services.AddSingleton<JWTTokenProvider>();
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
                        ValidIssuer = builder.Configuration["Jwt:Issuers"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });

            // Add repositories and services
            builder.Services
                    .AddServices() // Assuming AddRepository is an extension method to add repositories
                    .AddRepositories(); // Assuming AddServices is an extension method to add your services   

            // Add CORS policy to allow specific origins (e.g., localhost for development)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Use Swagger if app in Developement
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestAPI V1");
                    c.RoutePrefix = string.Empty;
                    c.InjectJavascript("/swagger/custom-swagger.js");
                });
            }

            //app.UseCors("AllowSpecificOrigins");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestAPI V1");
                //c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
