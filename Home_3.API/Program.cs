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

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddSingleton<IAuthorsRepository, AuthorsRepository>();
            builder.Services.AddSingleton<IBooksRepository, BooksRepository>();
        }
        else
        {
            builder.Services.AddScoped<IAuthorsRepository, AuthorsDBRepository>();
            builder.Services.AddScoped<IBooksRepository, BooksDBRepository>();
        }
        
        builder.Services.AddScoped<IAuthorsService, AuthorsService>();
        builder.Services.AddScoped<IBooksService, BooksService>();
        
        builder.Services.AddTransient<IValidationService, ValidationService>();
        
        builder.Services.AddDbContext<HomeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        if (app.Environment.IsDevelopment())
        {
            
        }
        
        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}

