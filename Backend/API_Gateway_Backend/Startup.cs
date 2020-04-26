using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using API.Models.gateway;
using API.Managers;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using API.AppConstants;
using API.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace API_Gateway_Controllers
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

            // Add Db context in a scoped lifetime. One new up per request and prevents concurrency issues.
            services.AddDbContext<ApiGatewayContext>();
            services.AddHttpContextAccessor();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                                                             .AllowAnyHeader()
                                                             .AllowAnyMethod());
            });


            // Register Authentication middleware.
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(requirements =>
            {
                requirements.TokenValidationParameters = new TokenValidationParameters
                {
                    // Signing key to test.
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Constants.SigningKey)),

                    ValidateIssuerSigningKey = true,        // Test that the signing key is valid.
                    RequireSignedTokens = true,             // Test that tokens are signed.
                    ValidateIssuer = true,                  // Test that the issuer matches the server.
                    ValidateLifetime = true,                // Test that the expiration time has not passed.
                    ValidateAudience = false,               // HACK: Temporary making audience not checked
                    

                    ValidIssuer = Constants.Issuer          // Issuer to compare token against.
                    
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsOwner", policy => policy.Requirements.Add(new ManageServiceRequirement()));
            });

            // Add authorization handlers.
            services.AddTransient<IAuthorizationHandler, ManageServiceAuthorizationHandler>();



            // Register Services.
            services.AddTransient<TeamRegistrationService>();
            services.AddTransient<TeamLoginService>();
            services.AddTransient<JWTService>();
            services.AddTransient<ServiceManagementService>();
            services.AddTransient<UrlValidationService>();

            //services.AddTransient<HttpContext>();

            // Register Managers.
            services.AddTransient<TeamRegistrationManager>();
            services.AddTransient<TeamLoginManager>();
            services.AddTransient<ServiceManagementManager>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");

            app.UseHttpsRedirection();

            app.UseRouting();

            // Enable the use of the JWT authentication middleware.
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
