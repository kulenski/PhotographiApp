﻿namespace PhotographiApp.Web
{
    using System.Reflection;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using PhotographiApp.Data;
    using PhotographiApp.Data.Common;
    using PhotographiApp.Data.Common.Repositories;
    using PhotographiApp.Data.Models.Application;
    using PhotographiApp.Data.Repositories;
    using PhotographiApp.Data.Seeding;
    using PhotographiApp.Services;
    using PhotographiApp.Services.Data;
    using PhotographiApp.Services.Data.Interfaces;
    using PhotographiApp.Services.Interfaces;
    using PhotographiApp.Services.Mapping;
    using PhotographiApp.Services.Messaging;
    using PhotographiApp.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var account = new CloudinaryDotNet.Account(
                this.configuration["Cloudinary:CloudName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            var cloudUtility = new Cloudinary(account);
            services.AddSingleton(cloudUtility);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<Role>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<IPhotoStorageService, PhotoStorageService>();
            services.AddTransient<IPhotoMetadataService, PhotoMetadataService>();
            services.AddTransient<IAlbumsService, AlbumsService>();
            services.AddTransient<ILicenseService, LicenseService>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IPhotoAlbumService, PhotoAlbumService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IFavoritesService, FavoritesService>();
            services.AddTransient<ITopicReplyService, TopicReplyService>();
            services.AddTransient<ITopicService, TopicService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
