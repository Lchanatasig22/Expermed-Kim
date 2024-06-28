using Expermed.Models;
using Microsoft.EntityFrameworkCore;

namespace Expermed.Servicios
{
    public class ConsultaService
    {
        private readonly Base_ExpermedContext _context;

        private readonly PacienteService _pacienteService;
        public ConsultaService(Base_ExpermedContext context, PacienteService pacienteService)
        {
            _context = context;
            _pacienteService = pacienteService;
        }

        public async Task<Paciente> BuscarPacientePorNombreAsync(string nombre)
        {
            return await _context.Pacientes
                .Where(p => p.PrimernombrePacientes.Contains(nombre) || p.SegundonombrePacientes.Contains(nombre) || p.PrimerapellidoPacientes.Contains(nombre) || p.SegundoapellidoPacientes.Contains(nombre))
                .FirstOrDefaultAsync();
        }

    }
}
