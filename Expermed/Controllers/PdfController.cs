using Expermed.Models;
using Expermed.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using System.Threading.Tasks;

public class PdfController : Controller
{
    private readonly CatalogService _catalogoService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PacienteService _pacienteService;
    private readonly ConsultaService _consultaService;

    
   
}
