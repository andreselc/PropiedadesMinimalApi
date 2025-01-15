using System.Net;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropiedadesMinimalApi.Datos;
using PropiedadesMinimalApi.Mappers;
using PropiedadesMinimalApi.Modelos;
using PropiedadesMinimalApi.Modelos.Dtos;

var builder = WebApplication.CreateBuilder(args);

//Configurar conexión a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Añadir AutoMapper a través de Inyección de Dependencias

builder.Services.AddAutoMapper(typeof(ConfiguracionMapas));

// Añadir Validaciones
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Endpoints

//Obtener todas las propiedades - GET - MapGet
app.MapGet("/api/propiedades", async (ApplicationDbContext _bd, ILogger<Program> logger) =>
{
    RespuestasAPI respuesta = new RespuestasAPI();
    //Usar el _logger que ya está como inyección de dependencias    
    logger.Log(LogLevel.Information, "Carga todas las propiedades");

    //respuesta.Resultado = DatosPropiedad.listaPropiedades;
    respuesta.Resultado = _bd.Propiedad;
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.OK;
    return Results.Ok(respuesta);

}).WithName("ObtenerPropiedades").Produces<RespuestasAPI>(200);

//Obtener propiedad individual - GET - MapGet
app.MapGet("/api/propiedades/{id:int}", async (ApplicationDbContext _bd, int id) =>
{
    RespuestasAPI respuesta = new RespuestasAPI() { Success = false, codigoEstado= HttpStatusCode.BadRequest };

    respuesta.Resultado = await _bd.Propiedad.FirstOrDefaultAsync(p => p.id == id);
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.OK;
    return Results.Ok(respuesta);

}).WithName("ObtenerPropiedad").Produces<RespuestasAPI>(200);

//Crear propiedad - POST - MapPost
app.MapPost("/api/propiedades", async (ApplicationDbContext _bd, 
    IMapper _mapper, 
    IValidator<CrearPropiedadDto> _validacion,
    [FromBody] CrearPropiedadDto crearPropiedadDto) =>
{
    RespuestasAPI respuesta = new RespuestasAPI();

    var resultadoValidaciones = await _validacion.ValidateAsync(crearPropiedadDto);
    
    //Validar id de la propiedad y que el nombre esté vacío
    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuesta);
    }

    //validar si el nombre de la propiedad ya existe
    if (await _bd.Propiedad.FirstOrDefaultAsync(p => p.nombre.ToLower() == crearPropiedadDto.nombre.ToLower()) != null)
    {
        respuesta.Errores.Add("El nombre de la propiedad ya existe");
        return Results.BadRequest(respuesta);
    }

    Propiedad propiedad = _mapper.Map<Propiedad>(crearPropiedadDto);

    await _bd.Propiedad.AddAsync(propiedad);
    await _bd.SaveChangesAsync();

    PropiedadDto propiedadDto = new PropiedadDto();

     _mapper.Map<PropiedadDto>(propiedad);

    //return Results.CreatedAtRoute("ObtenerPropiedad", new {id= propiedad.id}, propiedadDto);

    respuesta.Resultado = propiedadDto;
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.Created;
    return Results.Ok(respuesta);

}).WithName("CrearPropiedad").Accepts<CrearPropiedadDto>("application/json").Produces<RespuestasAPI>(201).Produces(400);


//Actualizar propiedad - PUT - MapPut
app.MapPut("/api/propiedades", async (ApplicationDbContext _bd, 
    IMapper _mapper,
    IValidator<ActualizarPropiedadDto> _validacion,
    [FromBody] ActualizarPropiedadDto actualizarPropiedadDto) =>
{
    RespuestasAPI respuesta = new RespuestasAPI();

    var resultadoValidaciones = await _validacion.ValidateAsync(actualizarPropiedadDto);

    //Validar id de la propiedad y que el nombre esté vacío
    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuesta);
    }

    Propiedad propiedadDesdeBD = await _bd.Propiedad.FirstOrDefaultAsync
    (p => p.id == actualizarPropiedadDto.id);

    propiedadDesdeBD.nombre = actualizarPropiedadDto.nombre;
    propiedadDesdeBD.descripcion = actualizarPropiedadDto.descripcion;
    propiedadDesdeBD.ubicacion = actualizarPropiedadDto.ubicacion;
    propiedadDesdeBD.activa = actualizarPropiedadDto.activa;

    await _bd.SaveChangesAsync();

    respuesta.Resultado = _mapper.Map<PropiedadDto>(propiedadDesdeBD); 
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.Created;
    return Results.Ok(respuesta);

}).WithName("ActualizarPropiedad").Accepts<ActualizarPropiedadDto>("application/json").Produces<RespuestasAPI>(200).Produces(400);

//Borrar propiedad
app.MapDelete("/api/propiedades/{id:int}", async (ApplicationDbContext _bd,int id) =>
{
    RespuestasAPI respuesta = new RespuestasAPI() { Success = false, codigoEstado = HttpStatusCode.BadRequest };

    //Obtener el id de la propiedad a eliminar
    Propiedad propiedadDesdeBD = await _bd.Propiedad.FirstOrDefaultAsync
   (p => p.id == id);

    if (propiedadDesdeBD != null)
    {
        _bd.Propiedad.Remove(propiedadDesdeBD);
        await _bd.SaveChangesAsync();

        respuesta.Success = true;
        respuesta.codigoEstado = HttpStatusCode.NoContent;
        return Results.Ok(respuesta);
    }
    else
    {
        respuesta.Errores.Add("El ID de la propiedad es inválido");
        return Results.BadRequest(respuesta);
    }

});


app.UseHttpsRedirection();
app.Run();
