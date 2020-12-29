using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using BlogEngineApi.Shared.Infrastructure.Persistence.EntityFramework;

using BlogEngineApi.User.Application;
using BlogEngineApi.Posts.Application;

using BlogEngineApi.Posts.Domain;
using BlogEngineApi.Posts.Infrastructure.Persistence;

namespace BlogEngineApi
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
            string connectionString = Configuration["BlogEngine:ConnectionString"];
            services.AddControllers();
            services.AddDbContextPool<BlogEngineDbContext>(x =>
            {
                x.UseMySql(connectionString);
                x.UseLazyLoadingProxies();
                x.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                   ValidateIssuer = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidateAudience = false,
                   ValidateLifetime = true,
               };
           });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.Writer, Policies.WriterPolicy());
                config.AddPolicy(Policies.Editor, Policies.EditorPolicy());
            });

            // services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostCreateService, PostCreateService>();
            services.AddScoped<IPostGetService, PostGetService>();
            services.AddScoped<IPostUpdateService, PostUpdateService>();
            services.AddScoped<IPostDeleteService, PostDeleteService>();

            // repositories
            services.AddScoped<IPostRepository, MySqlPostRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
