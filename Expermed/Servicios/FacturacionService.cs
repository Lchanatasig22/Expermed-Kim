using Expermed.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Expermed.Servicios
{
    public class FacturacionService
    {
        private readonly Base_ExpermedContext _context;
        private readonly PacienteService _pacienteService;
        private readonly ConsultaService _consultaService;
        private readonly HttpClient _httpClient;

        public FacturacionService(Base_ExpermedContext context, PacienteService pacienteService, ConsultaService consultaService, HttpClient httpClient)
        {
            _context = context;
            _pacienteService = pacienteService;
            _consultaService = consultaService;
            _httpClient = httpClient;
        }

        public async Task CrearDocumentoAsync(Documento documento)
        {
            await _context.Documentos.AddAsync(documento);
            await _context.SaveChangesAsync();
        }

        public async Task<Documento> ObtenerDocumentoAsync(int secuencial)
        {
            return await _context.Documentos
                .Include(d => d.Emisor)
                .ThenInclude(e => e.Establecimiento)
                .Include(d => d.Totales)
                .ThenInclude(t => t.Impuestos)
                .Include(d => d.Comprador)
                .Include(d => d.Items)
                .ThenInclude(i => i.Impuestos)
                .Include(d => d.Pagos)
                .FirstOrDefaultAsync(d => d.Secuencial == secuencial);
        }

        public async Task<Documento> GenerarDocumentoDesdeConsultaAsync(int consultaId)
        {
            var consulta = await _consultaService.ObtenerConsultaPorIdAsync(consultaId);
            if (consulta == null)
            {
                return null;
            }

            var paciente = await _pacienteService.GetPacienteByIdAsync(consulta.IdConsulta);
            if (paciente == null)
            {
                return null;
            }

            // Mapear datos de consulta y paciente a un nuevo documento
            var documento = new Documento
            {
                Ambiente = 1,
                TipoEmision = 1,
                Secuencial = 9, // Generar o obtener un secuencial adecuado
                FechaEmision = DateTime.Now,
                Emisor = new Emisor
                {
                    Ruc = "1793215241001",
                    ObligadoContabilidad = true,
                    ContribuyenteEspecial = "",
                    NombreComercial = "EXPERMED",
                    RazonSocial = "EXPERMED S.A.S.",
                    Direccion = "AVENIDA AMERICA 35-29 Y HERNANDEZ DE GIRON",
                    Establecimiento = new Establecimiento
                    {
                        PuntoEmision = "002",
                        Codigo = "001",
                        DireccionEstablecimiento = "AVENIDA AMERICA 35-29 Y HERNANDEZ DE GIRON"
                    }
                },
                Moneda = "USD",
                InformacionAdicional = new InformacionAdicional
                {
                    Referencia = "consulta Medica"
                },
                Totales = new Totales
                {
                    TotalSinImpuestos = 60.00,
                    Impuestos = new List<Impuesto>
                    {
                        new Impuesto
                        {
                            BaseImponible = 60.00,
                            Valor = 9.00,
                            Codigo = "2",
                            CodigoPorcentaje = "4"
                        }
                    },
                    ImporteTotal = 69.00,
                    Propina = 0.0,
                    Descuento = 0.0
                },
                Comprador = new Comprador
                {
                    Email = paciente.EmailPacientes,
                    Identificacion = paciente.CiPacientes,
                    TipoIdentificacion = paciente.TipodocumentoPacientesCNavigation.UuidCatalogo,
                    RazonSocial = paciente.PrimernombrePacientes,
                    Direccion = paciente.DireccionPacientes,
                    Telefono = paciente.TelefonocelularPacientes
                },
                Items = new List<Item>
                {
                    new Item
                    {
                        Cantidad = 1,
                        CodigoPrincipal = "1",
                        CodigoAuxiliar = "",
                        PrecioUnitario = 60,
                        Descuento = 0,
                        Descripcion = "Consulta Médica",
                        PrecioTotalSinImpuestos = 60,
                        Impuestos = new List<Impuesto>
                        {
                            new Impuesto
                            {
                                BaseImponible = 60,
                                Valor = 9.00,
                                Tarifa = 15.0,
                                Codigo = "2",
                                CodigoPorcentaje = "4"
                            }
                        },
                        UnidadMedida = ""
                    }
                }
            };

            return documento;
        }

        public async Task<string> EnviarDocumentoAApiExterna(Documento documento)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://link.datil.co/invoices/issue");
            request.Headers.Add("X-Key", "7278cc50a72640eea6384a075b8e8335");
            request.Headers.Add("X-Password", "EX1793215241");

            var json = JsonSerializer.Serialize(documento, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GenerarYEnviarDocumentoDesdeConsultaAsync(int consultaId, Documento documentoFormulario, string tipoPago)
        {
            var documento = await GenerarDocumentoDesdeConsultaAsync(consultaId);
            if (documento == null)
            {
                return null;
            }

            // Mapear los datos del formulario al documento generado
            documento.FechaEmision = documentoFormulario.FechaEmision;
            documento.Totales.TotalSinImpuestos = documentoFormulario.Totales.TotalSinImpuestos;
            documento.Totales.Impuestos = documentoFormulario.Totales.Impuestos;
            documento.Totales.ImporteTotal = documentoFormulario.Totales.ImporteTotal;
            documento.Pagos = new List<Pago>
            {
                new Pago
                {
                    Medio = tipoPago,
                    Total = documentoFormulario.Totales.ImporteTotal,
                    Notas = "Consulta Médica"
                }
            };

            return await EnviarDocumentoAApiExterna(documento);
        }
    }
}
