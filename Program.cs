using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using product_orders.Endpoints.Categories;
using product_orders.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(DotNetEnv.Env.GetString("DB_LOCAL"));
    options.EnableSensitiveDataLogging().LogTo(Console.WriteLine);
});

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);
app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Methods, CategoryGetAll.Handle);
app.MapMethods(CategoryPut.Template, CategoryPut.Methods, CategoryPut.Handle);

app.Run();
