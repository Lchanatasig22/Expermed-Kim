using Expermed.Models;
using Microsoft.EntityFrameworkCore;

namespace Expermed.Servicios
{ /// <summary>
/// Aqui mandamos a listar los distintos tipo de select filtrando por su categoria
/// </summary>
    public class CatalogService
    {
        private readonly Base_ExpermedContext _context;

        public CatalogService(Base_ExpermedContext context)
        {
            _context = context;
        }

        public async Task<List<Catalogo>> ObtenerTiposDocumentosAsync()
        {
            // Supongamos que tienes una tabla 'Catalogo' con los tipos de documentos
            return await _context.Catalogos.Where(c => c.CategoriaCatalogo == "TIPO DOCUMENTO").ToListAsync();
        }

        public async Task<List<Catalogo>> ObtenerTiposDeSangreAsync()
        {
            // Supongamos que tienes una tabla 'Catalogo' con los tipos de documentos
            return await _context.Catalogos.Where(c => c.CategoriaCatalogo == "TIPO DE SANGRE").ToListAsync();
        }
        public async Task<List<Catalogo>> ObtenerTiposDeGeneroAsync()
        {
            // Supongamos que tienes una tabla 'Catalogo' con los tipos de documentos
            return await _context.Catalogos.Where(c => c.CategoriaCatalogo == "GENERO").ToListAsync();
        }
        public async Task<List<Catalogo>> ObtenerTiposDeEstadoCivilAsync()
        {
            // Supongamos que tienes una tabla 'Catalogo' con los tipos de documentos
            return await _context.Catalogos.Where(c => c.CategoriaCatalogo == "ESTADO CIVIL").ToListAsync();
        }
        public async Task<List<Catalogo>> ObtenerTiposDeFormacionPAsync()
        {
            // Supongamos que tienes una tabla 'Catalogo' con los tipos de documentos
            return await _context.Catalogos.Where(c => c.CategoriaCatalogo == "FORMACION PROFESIONAL").ToListAsync();
        }
        public async Task<List<Localidad>> ObtenerTiposDeNacionalidadPAsync()
        {
            // Supongamos que tienes una tabla 'Catalogo' con los tipos de documentos
            return await _context.Localidads.Where(c => c.ActivoLocalidad == 1).ToListAsync();
        }
        public async Task<List<Localidad>> ObtenerTiposDeProvinciaPAsync()
        {
            // Supongamos que tienes una tabla 'Catalogo' con los tipos de documentos
            return await _context.Localidads.Where(c => c.ActivoLocalidad == 1).ToListAsync();
        }
        public async Task<List<Catalogo>> ObtenerTiposDeSeguroAsync()
        {
            // Supongamos que tienes una tabla 'Catalogo' con los tipos de documentos
            return await _context.Catalogos.Where(c => c.CategoriaCatalogo == "SEGUROS DE SALUD").ToListAsync();
        }
        public async Task<List<Usuario>> ObtenerMedicoAsync()
        {
            // Supongamos que tienes una tabla 'Catalogo' con los tipos de documentos
            return await _context.Usuarios.Where(c => c.PerfilUsuarioP == 2).ToListAsync();
        }

        // obviamente se puede crear varios metodos que generemos para las listas aqui mismo no solo de la tabla catalogo

    }
}
