using Microsoft.EntityFrameworkCore;
using NovelistBlazor.Common.Model;
using NovelistBlazor.Common.DTO;
using NovelistBlazor.API.Data;
using Microsoft.AspNetCore.Localization;
using NovelistBlazor.Common.Service;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NovelistDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NovelistDbDevelop;Trusted_Connection=True;"));

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Novel, NovelDTO>();
    cfg.CreateMap<NovelDTO, Novel>();
    cfg.CreateMap<Character, CharacterDTO>();
    cfg.CreateMap<CharacterDTO, Character>();
    cfg.CreateMap<PlotUnit, PlotUnitDTO>();
    cfg.CreateMap<PlotUnitDTO, PlotUnit>();
    cfg.CreateMap<PlotUnitType, PlotUnitTypeDTO>();
    cfg.CreateMap<PlotUnitTypeDTO, PlotUnitType>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(_ => mapper);

builder.Services.AddSingleton<DataFactory>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowLocalhost",
            builder => builder.WithOrigins("https://localhost:7234") // Hier sollte die Adresse deiner PWA stehen
                              .AllowAnyHeader()
                              .AllowAnyMethod());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
