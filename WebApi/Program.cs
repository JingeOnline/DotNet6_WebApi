using WebApi.MiddleWares;
using AzureSpeech;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton(typeof(AzureTextToSpeechService));
//builder.Services.AddSingleton<IAzureTextToSpeechService, AzureTextToSpeechService>();
//如果不使用接口，可以这样写
builder.Services.AddSingleton<AzureTextToSpeechService, AzureTextToSpeechService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//自定义的中间件，用来Logging，如果放在UseHttpsRedirection()上面，该中间件会被触发两次
app.UseMiddleware<LoggingMiddleWare>();
app.UseAuthorization();

app.MapControllers();

app.Run();
