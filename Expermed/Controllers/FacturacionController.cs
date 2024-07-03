using Expermed.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Expermed.Controllers
{
    public class FacturacionController : Controller
    {
        private readonly FacturacionService _facturacionService;
        private readonly CatalogService _catalogoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PacienteService _pacienteService;
        private readonly ConsultaService _consultaService;

        [HttpGet]
        public async Task<IActionResult> Facturacion()
        {

            var tiposBancos = await _catalogoService.ObtenerBancosAsync();
            // Puedes crear una lista de SelectListItem para usar en tu select
            var tiposBancoSelectList = tiposBancos.Select(d => new SelectListItem
            {
                Value = d.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = d.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();
            return View();
        }



    }
}
