using Expermed.Models;
using Microsoft.EntityFrameworkCore;

namespace Expermed.Servicios
{/// <summary>
/// esto pues solo lo hice en caso de que se necesite
/// </summary>
    public class PerfilesService
    {
        private readonly Base_ExpermedContext _context;

        public PerfilesService(Base_ExpermedContext context)
        {
            _context = context;
        }

        // Método para obtener todos los perfiles
        public async Task<List<Perfil>> GetAllPerfilesAsync()
        {
            return await _context.Perfils.ToListAsync();
        }
    }
}
