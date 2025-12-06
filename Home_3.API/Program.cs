using Home_3.BLL.interfaces.Repositories;
using Home_3.BLL.interfaces.Services;
using Home_3.DAL;
using Home_3.DAL.Repositories;
using Home_3.Services;
using Microsoft.EntityFrameworkCore;

namespace Home_3;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<IAuthorsRepository, AuthorsRepository>();
        builder.Services.AddSingleton<IBooksRepository, BooksRepository>();
        
        builder.Services.AddScoped<IAuthorsService, AuthorsService>();
        builder.Services.AddScoped<IBooksService, BooksService>();
        
        builder.Services.AddTransient<IValidationService, ValidationService>();
        
        builder.Services.AddDbContext<HomeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}

//TODO
//1.Update readme
//2.add database logic

