using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using blog.Filters;
using blog.Models;
using blog.Models.Advertisement;
using blog.Models.Article;
using blog.Models.Classification;
using blog.Models.Comment;
using blog.Models.Consumer;
using blog.Models.Tag;
using blog.Services;
using blog.Services.Impl;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace blog
{
    public class Startup
    {
        public static ILoggerRepository Repository { get; set; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            //To configure the log
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            string connection = Configuration.GetConnectionString("conn");

            //EF
            services.AddDbContext<ArticleContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<ClassificationContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<CommentContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<AdvertisementContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<TagContext>(options => options.UseSqlServer(connection));

            //DI
            services.AddTransient<IUserService, UserServiceImpl>();
            services.AddTransient<IArticleService, ArticleServiceImpl>();
            services.AddTransient<IManagementService, ManagementServiceImpl>();
            services.AddTransient<IAboutMeService, AboutMeServiceImpl>();


            //Token
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Tokens:ValidIssuer"],
                    ValidAudience = Configuration["Tokens:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:IssuerSigningKey"])),
                    RequireExpirationTime = true,
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();

                        context.Response.StatusCode = 401;
                        context.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = context.Exception.Message;
                        Debug.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " +
                            context.SecurityToken);
                        return Task.CompletedTask;
                    }

                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //解决跨域问题
            app.UseCors(builder => { builder.AllowAnyOrigin();builder.AllowAnyHeader();builder.AllowAnyMethod(); });

            //让每次进来的 HTTP Request 都会经过此验证机制
            app.UseAuthentication();
            //全局异常捕获
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
