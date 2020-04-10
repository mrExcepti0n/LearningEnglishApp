using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using LearningEnglishWeb.Configuration;
using LearningEnglishWeb.Infrastructure;
using LearningEnglishWeb.Infrastructure.Training;
using LearningEnglishWeb.Services;
using LearningEnglishWeb.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace LearningEnglishWeb
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
            services.AddControllersWithViews()
                .Services.AddCustomMvc(Configuration)
                .AddHttpClientServices();

            IdentityModelEventSource.ShowPII = true;       // Caution! Do NOT use in production: https://aka.ms/IdentityModel/PII

            services.AddControllers();

            services.AddCustomAuthentication(Configuration);

            services.AddScoped<TrainingFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            var pathBase = Configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultError", "{controller=Error}/{action=Error}");
                endpoints.MapControllers();
            });
        }
    }


    static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.AddSession();
            services.AddDistributedMemoryCache();
            return services;
        }
        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IVocabularyService, VocabularyService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<IWordSetService, WordSetService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<IWordImageService, WordImageService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<ITrainingService, TrainingService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<ISpeachService, SpeachService>();

            return services;
        }


        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var identityUrl = configuration.GetValue<string>("IdentityUrl");
            var callBackUrl = configuration.GetValue<string>("CallBackUrl");
            var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);


            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddCookie(setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
                .AddOpenIdConnect(options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.Authority = identityUrl.ToString();
                    options.SignedOutRedirectUri = callBackUrl.ToString();
                    options.ClientId = "mvc";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code id_token";
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.RequireHttpsMetadata = false;
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("learningenglish");
                });

            return services;
        }
    }
}
