using DinkToPdf;
using DinkToPdf.Contracts;
using Expermed.Models;
using Expermed.Servicios;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Loader;

var builder = WebApplication.CreateBuilder(args);

// Ruta específica a la biblioteca wkhtmltopdf
var wkHtmlToPdfPath = @"C:\Users\SAFERISK\source\repos\ExpermedGit\Expermed\libs\libwkhtmltox.dll";

// Agregar la ruta de la DLL al PATH del sistema
var envPath = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
var newPath = $"{envPath};{Path.GetDirectoryName(wkHtmlToPdfPath)}";
Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.Process);

// Cargar la biblioteca no administrada
var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(wkHtmlToPdfPath);

// Agregar servicios al contenedor de dependencias
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<Base_ExpermedContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

builder.Services.AddScoped<AutenticationService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PerfilesService>();
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<CitasService>();
builder.Services.AddScoped<ConsultaService>();
builder.Services.AddScoped<FacturacionService>();
builder.Services.AddScoped<PdfService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();

public class CustomAssemblyLoadContext : AssemblyLoadContext
{
    public IntPtr LoadUnmanagedLibrary(string absolutePath)
    {
        return LoadUnmanagedDll(absolutePath);
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        return LoadUnmanagedDllFromPath(unmanagedDllName);
    }
}
