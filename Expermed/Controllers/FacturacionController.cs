using Expermed.Models;
using Expermed.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Expermed.Controllers
{
    public class FacturacionController : Controller
    {
        private readonly FacturacionService _facturacionService;
        private readonly CatalogService _catalogoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PacienteService _pacienteService;
        private readonly ConsultaService _consultaService;

        // Constructor único con inyección de dependencias
        public FacturacionController(FacturacionService facturacionService, CatalogService catalogoService,
            IHttpContextAccessor httpContextAccessor, PacienteService pacienteService, ConsultaService consultaService)
        {
            _facturacionService = facturacionService;
            _catalogoService = catalogoService;
            _httpContextAccessor = httpContextAccessor;
            _pacienteService = pacienteService;
            _consultaService = consultaService;
        }

        [HttpGet]
        public async Task<IActionResult> Facturacion()
        {
            var tiposBancos = await _catalogoService.ObtenerBancosAsync();
            var tiposBancosSelectList = tiposBancos.Select(d => new SelectListItem
            {
                Value = d.UuidCatalogo.ToString(), // Aquí debes asignar el valor correcto
                Text = d.DescripcionCatalogo // Aquí debes asignar el texto que se mostrará en la opción
            }).ToList();

            ViewBag.TiposBancos = tiposBancosSelectList;

            return View();
        }

        [HttpPost("enviar-desde-consulta")]
        public async Task<IActionResult> EnviarDocumentoDesdeConsulta([FromForm] FormularioPago formularioPago)
        {
            if (formularioPago == null)
            {
                return BadRequest("El formulario no puede estar vacío.");
            }

            var documentoFormulario = new Documento
            {
                FechaEmision = formularioPago.FechaEmision,
                Totales = new Totales
                {
                    TotalSinImpuestos = formularioPago.TotalSinImpuestos,
                    ImporteTotal = formularioPago.ImporteTotal,
                    Impuestos = formularioPago.Impuestos
                },
                Pagos = formularioPago.Pagos
            };

            var resultado = await _facturacionService.GenerarYEnviarDocumentoDesdeConsultaAsync(formularioPago.ConsultaId, documentoFormulario);
            if (resultado == null)
            {
                return NotFound("No se pudo generar o enviar el documento desde la consulta.");
            }

            return Ok(resultado);
        }
    }
}
