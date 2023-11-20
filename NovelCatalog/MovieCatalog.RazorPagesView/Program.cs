using NovelCatalog.Application.Novelists;
using NovelCatalog.Application.Categories;
using NovelCatalog.Application.Novel;
using NovelCatalog.MemoryPersistense.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<INovelistRepository, NovelistRepository>();
builder.Services.AddTransient<INovelRepository, NovelRepository>();

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<INovelistService, NovelistService>();
builder.Services.AddTransient<INovelService, NovelService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
