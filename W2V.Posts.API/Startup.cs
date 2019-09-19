using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using W2V.Posts.API.Configurations;
using W2V.Posts.API.Domain.DAL;
using W2V.Posts.API.Domain.Repositories;
using W2V.Posts.API.Domain.Services;

namespace W2V.Posts.API
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
            InitConfigurations(services);
            services.AddSingleton(typeof(IPostService), typeof(PostService));
            services.AddSingleton(typeof(IPostsRepository), typeof(PostsRedisRepository));
            services.AddSingleton(typeof(IRedisDataBaseService), typeof(RedisDataBaseService));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
        }

        private void InitConfigurations(IServiceCollection services)
        {
            services.Configure<RedisDataBaseConfiguration>(config =>
            {
                config.DbConnectionString = Configuration.GetSection("RedisDataBaseConfiguration:DbConnectionString").Value;

                config.KeyExpirationTime = TimeSpan.TryParse(Configuration.GetSection("RedisDataBaseConfiguration:KeyExpirationTime").Value,
                    out var keyExpirationTimeValue) ? keyExpirationTimeValue : TimeSpan.FromMinutes(1);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
