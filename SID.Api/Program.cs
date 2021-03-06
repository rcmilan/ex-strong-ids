using SID.Api.Swagger;
using SID.Domain.Converters.Factories;
using SID.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connString = "server=localhost;port=3306;userid=mysqlusr;password=password;database=typedids;";

builder.Services.AddDatabaseModule(connString);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverterFactory());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SchemaFilter<StronglyTypedIdSchemaFilter>());

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