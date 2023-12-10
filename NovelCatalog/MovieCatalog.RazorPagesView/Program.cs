using NovelCatalog.Application.Novelists;
using NovelCatalog.Application.Categories;
using NovelCatalog.Application.Novel;
using NovelCatalog.MemoryPersistense.Repositories;
using NovelCatalog.MVCView.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
