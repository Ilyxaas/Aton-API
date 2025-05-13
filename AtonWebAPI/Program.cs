using System.Reflection;
using AtonWebAPI.MappingProfile;
using AtonWebAPI.Middleware;
using BusinessLogic;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Repository;
using Microsoft.OpenApi.Models;

using AppContext = System.AppContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(UserMappingProfile));

builder.Services.AddSingleton<IPasswordService, PasswordService>();

builder.Services.AddScoped<IDataBaseInitializeService, DataBaseInitializeService>();

// True - работаем с базой данных. False - локальное хранилище
// Локальное хранилище было для тестирования.

var useDataBase = true;

if (useDataBase)
{
    builder.Services.AddScoped<IUserRepository, DatabaseUserRepository>();
    builder.Services.AddDataBaseService();
}
else
{
    builder.Services.AddScoped<IUserRepository, LocalStorageUserRepository>();
    builder.Services.AddSingleton(new LocalUserStorage());
}

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Aton User API by Илья Есин",
        Version = "v1",
        Description = $"По умолчанию есть 2 пользователя. " +
                      $"\n Admin с Guid = {DataBaseInitializeService.AdminGuid} " +
                      $"\n Пользователь с Guid = {DataBaseInitializeService.UserGuid}"
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

//Заполняем хранилище если вдруг оно пустое.
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDataBaseInitializeService>();
    await dbInitializer.InitializeAsync(); 
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger/index.html")).ExcludeFromDescription(); ;





app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

