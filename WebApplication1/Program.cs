using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using DataBase;


namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<MyAppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyAppDbContext") ??
                                     throw new InvalidOperationException(
                                         "Connection string 'MyAppDbContext' not found.")));

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IImageRepository, ImageRepository>();
            builder.Services.AddTransient<IImageService, ImageService>();

            builder.Services.AddMemoryCache();

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

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}