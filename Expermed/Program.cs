using Expermed.Models; // Importa los modelos de datos del proyecto.
using Expermed.Servicios; // Importa los servicios del proyecto.
using Microsoft.EntityFrameworkCore; // Importa Entity Framework Core para trabajar con la base de datos.
using Rotativa.AspNetCore; // Importa Rotativa para la generaci�n de PDFs.

var builder = WebApplication.CreateBuilder(args); // Crea una instancia del constructor de la aplicaci�n web.

// Agrega servicios al contenedor de dependencias.
builder.Services.AddControllersWithViews(); // Agrega soporte para controladores con vistas (MVC).
builder.Services.AddSession(options =>
{
    // Configura las opciones de la sesi�n.
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Establece el tiempo de espera de la sesi�n en 30 minutos.
    options.Cookie.HttpOnly = true; // Configura la cookie de sesi�n como HttpOnly.
    options.Cookie.IsEssential = true; // Marca la cookie de sesi�n como esencial.
});
builder.Services.AddRazorPages().AddRazorRuntimeCompilation(); // Agrega soporte para Razor Pages con compilaci�n en tiempo de ejecuci�n.
builder.Services.AddDbContext<Base_ExpermedContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"))); // Configura el contexto de la base de datos utilizando SQL Server.

builder.Services.AddScoped<AutenticationService>(); // Registra el servicio de autenticaci�n.
builder.Services.AddScoped<UserService>(); // Registra el servicio de usuarios.
builder.Services.AddScoped<PerfilesService>(); // Registra el servicio de perfiles.
builder.Services.AddScoped<PacienteService>(); // Registra el servicio de pacientes.
builder.Services.AddScoped<CatalogService>(); // Registra el servicio de cat�logo.
builder.Services.AddScoped<CitasService>(); // Registra el servicio de citas.
builder.Services.AddScoped<ConsultaService>(); // Registra el servicio de consultas.
builder.Services.AddScoped<FacturacionService>(); // Registra el servicio de facturaci�n.

//Registra IHttpContextAccessor para acceder al contexto HTTP.
builder.Services.AddHttpContextAccessor();

var app = builder.Build(); // Construye la aplicaci�n.

// Configura el pipeline de manejo de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // Configura el manejo de errores para ambientes de producci�n.
    app.UseHsts(); // Habilita el uso de HSTS (HTTP Strict Transport Security).
}

app.UseHttpsRedirection(); // Redirecciona las solicitudes HTTP a HTTPS.
app.UseStaticFiles(); // Habilita el servicio de archivos est�ticos.
app.UseSession(); // Habilita el uso de sesiones.
app.UseRouting(); // Habilita el enrutamiento de solicitudes.
app.UseAuthorization(); // Habilita la autorizaci�n de solicitudes.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}"); // Configura la ruta predeterminada para los controladores.

// Configura Rotativa para la generaci�n de PDFs
IWebHostEnvironment env = app.Environment;
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa");

app.Run(); // Ejecuta la aplicaci�n.
