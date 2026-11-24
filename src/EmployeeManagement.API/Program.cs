using EmployeeManagement.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
var employees = new List<Employee>
{
    new Employee { Id = 1, Department = "HR", Name = "Dolly" },
    new Employee { Id = 2, Department = "IT", Name = "Eme" },
};
app.MapGet("/api/employees", () => employees);
app.MapGet("/hello", () => "Welcome to employee management system");
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();