using InvestmentsCodeAssessment.DB;
using InvestmentsCodeAssessment.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//fake db context as singleton for the purpose of mocking the db
builder.Services.AddSingleton<InvestmentsFakeDbContext>();
builder.Services.AddScoped<IInvestmentsService, InvestmentsService>();
builder.Services.AddScoped<InvestmentsRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();