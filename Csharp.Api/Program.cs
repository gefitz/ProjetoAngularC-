using Csharp.Api.Data;
using Csharp.Api.Data.CriacaoBase;
using Csharp.Api.DTO.Mapper;
using Csharp.Api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Configuração do DbContext com banco em memória
builder.Services.AddScoped<CommandSql>(opc => new CommandSql(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddScoped<TpProdutoRepository>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<VendaRepository>();
builder.Services.AddAutoMapper(typeof(MapperProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowSpecificOrigin",
        build =>
        {
            build.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

    app.UseCors("AllowSpecificOrigin");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
new CriarBase(new CommandSql(builder.Configuration.GetConnectionString("ConnectionString")));

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
