namespace Expermed.Models
{
    public class PdfViewModel
    {
        public bool Receta { get; set; }
        public bool Justificacion { get; set; }
        public bool FormatoConsulta { get; set; }
        public string NombrePaciente { get; set; }
        public string FechaActual { get; set; }
        public string Medicamentos { get; set; }
        public string InstruccionesAdicionales { get; set; }
        public string MotivoJustificacion { get; set; }
        public string Recomendaciones { get; set; }
        public string MotivoConsulta { get; set; }
        public string Diagnostico { get; set; }
        public string PlanTratamiento { get; set; }
    }

}
