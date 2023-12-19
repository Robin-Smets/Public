using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NovelistBlazor.Common.DTO;
using NovelistBlazor.Common.Service;
using NovelistBlazor.PWA;
using NovelistBlazor.Common;
using AutoMapper;
using NovelistBlazor.Common.Interface;
using NovelistBlazor.Common.Model;
using Microsoft.Extensions.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7173/") });

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

builder.Services.AddHttpClient();

builder.Services.AddSingleton<AppState>();
builder.Services.AddSingleton<PageAnalyzer>();
builder.Services.AddSingleton<EventMediator>();
builder.Services.AddSingleton<DataFactory>();
builder.Services.AddSingleton<ResponseDeserializer>();

builder.Services.AddSingleton<Repository>();
builder.Services.AddSingleton<NovelRepository>();
builder.Services.AddSingleton<CharacterRepository>();
builder.Services.AddSingleton<PlotUnitRepository>();

await builder.Build().RunAsync();
