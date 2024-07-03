using Expermed.Models;

namespace Expermed.Servicios
{
    public class FacturacionService
    {
        private readonly Base_ExpermedContext _context;

        private readonly PacienteService _pacienteService;
        public FacturacionService(Base_ExpermedContext context, PacienteService pacienteService)
        {
            _context = context;
            _pacienteService = pacienteService;
        }
    }
}
