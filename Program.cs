using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using monstock.Components;
using monstock.Models;
using System.Reflection.Metadata;

namespace monstock
{
    public class Program
    {
        //DbContext
        public class BloggingContext(DbContextOptions<BloggingContext> options) : DbContext(options)
        {
            public DbSet<ColorRecord> Colors { get; set; }
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

            builder.Services.AddDbContextPool<BloggingContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add services to the container.
        builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
