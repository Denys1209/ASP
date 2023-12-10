using NovelCatalog.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddLoggingMiddleware();

builder.Services.AddControllers(x =>
{
    x.Filters.AddValidationFilter();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();


app.UseLoggingMiddleware();

app.MapControllers();


app.Run();