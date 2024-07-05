using System;

public class PdfViewModel
{
    public string NombrePaciente { get; set; }
    public DateTime FechaActual { get; set; }
    public string MotivoConsulta { get; set; }
    public string AntecedentesPersonales { get; set; }
    public AntecedentesFamiliares AntecedentesFamiliares { get; set; }
    public string EnfermedadActual { get; set; }
    public RevisionOrganos RevisionOrganos { get; set; }
    public SignosVitales SignosVitales { get; set; }
    public ExamenFisico ExamenFisico { get; set; }
 
    public string PlanTratamiento { get; set; }
    public DateTime PlanTratamientoFecha { get; set; }
    public TimeSpan PlanTratamientoHora { get; set; }
    public string ProfesionalNombre { get; set; }
    public string ProfesionalCodigo { get; set; }
}

public class AntecedentesFamiliares
{
    public bool Cardiopatia { get; set; }
    public bool Diabetes { get; set; }
    public bool EnfVascular { get; set; }
    public bool Hipertension { get; set; }
    public bool Cancer { get; set; }
    public bool Tuberculosis { get; set; }
    public bool EnfMental { get; set; }
    public bool EnfInfecciosa { get; set; }
    public bool Malformacion { get; set; }
    public string Otro { get; set; }
}

public class RevisionOrganos
{
    public RevisionItem Oido { get; set; }
    public RevisionItem Cardiovascular { get; set; }
    public RevisionItem Genital { get; set; }
    public RevisionItem Respiratorio { get; set; }
    public RevisionItem Digestivo { get; set; }
    public RevisionItem Urinario { get; set; }
    public RevisionItem MusculoEsqueletico { get; set; }
    public RevisionItem Endocrino { get; set; }
    public RevisionItem HemoLinfatico { get; set; }
    public RevisionItem Nervioso { get; set; }
}

public class RevisionItem
{
    public bool CP { get; set; }
    public bool SP { get; set; }
}

public class SignosVitales
{
    public DateTime FechaMedicion { get; set; }
    public decimal Temperatura { get; set; }
    public string PresionArterial { get; set; }
    public int Pulso { get; set; }
    public int FrecuenciaRespiratoria { get; set; }
    public decimal Peso { get; set; }
    public decimal Talla { get; set; }
}

public class ExamenFisico
{
    public ExamenItem Cabeza { get; set; }
    public ExamenItem Cuello { get; set; }
    public ExamenItem Torax { get; set; }
    public ExamenItem Abdomen { get; set; }
    public ExamenItem Pelvis { get; set; }
    public ExamenItem Extremidades { get; set; }
}

public class ExamenItem
{
    public bool CP { get; set; }
    public bool SP { get; set; }
}

