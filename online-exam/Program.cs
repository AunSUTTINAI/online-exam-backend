using class_room.Endpoint;
using class_room.UnitOfWork;
using class_room.UnitOfWork.Interface;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IUowClassRoom, UowClassRoom>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Online Exam API",
        Version = "v1",
        Description = "API for managing online exam.",
    });
});

var policyName = "_allowFrontend";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Exam API v1");
        opt.DocumentTitle = "Online Exam API Documentation";
        opt.InjectStylesheet("/custom-swagger/custom.css"); 
        opt.EnableFilter();
    });
}

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors(policyName);

ExamEndpoint.Registers(app);

app.Run();
