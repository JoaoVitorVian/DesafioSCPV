using Application.DTO.PostoDeVacinacao;
using Application.DTO.Vacina;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using CadastroPostosVacinacao.Domain.Entities;
using Infra.Data.Context;
using Infra.Data.Repositories;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SCPV.Presentation.WebAPI.ViewModel.PostoViewModel;
using SCPV.Presentation.WebAPI.ViewModel.VacinaViewModel;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cadastro de Postos de Vacinação", Version = "v1" });
});

builder.Services.AddSingleton(d => configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

#region automapper
var autoMapperConfig = new MapperConfiguration(cfg =>
{
    
    cfg.CreateMap<PostoDeVacinacao, PostoDeVacinacaoDTO>().ReverseMap();
    cfg.CreateMap<Vacina, VacinaDTO>().ReverseMap();

    cfg.CreateMap<CreatePostoDeVacinacaoViewModel, PostoDeVacinacaoDTO>().ReverseMap();
    cfg.CreateMap<UpdatePostoDeVacinacaoViewModel, PostoDeVacinacaoDTO>().ReverseMap();

    cfg.CreateMap<CreateVacinaViewModel, VacinaDTO>().ReverseMap();
    cfg.CreateMap<UpdateVacinaViewModel, VacinaDTO>().ReverseMap();
});
#endregion

#region DI
builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

builder.Services.AddScoped<IPostoDeVacinacaoRepository, PostoDeVacinacaoRepository>();
builder.Services.AddScoped<IVacinaRepository, VacinaRepository>();

builder.Services.AddScoped<IPostoDeVacinacaoService, PostoDeVacinacaoService>();
builder.Services.AddScoped<IVacinaService, VacinaService>();
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
