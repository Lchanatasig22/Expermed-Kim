using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expermed.Models
{
    public partial class Consultum
    {
        public Consultum()
        {
            Cita = new HashSet<Citum>();
            ActivoConsulta = 1;
            SecuencialConsulta = "3";
            EspecialidadConsultaC = 2;
            EstadoConsultaC = 1;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdConsulta { get; set; }

        public DateTime? FechacreacionConsulta { get; set; }

        [StringLength(500)]
        public string? UsuariocreacionConsulta { get; set; }

        [StringLength(500)]
        public string? HistorialConsulta { get; set; }

        [StringLength(500)]
        public string? SecuencialConsulta { get; set; }

        public int? PacienteConsultaP { get; set; }

        [Column(TypeName = "text")]
        public string? MotivoConsulta { get; set; }

        [Column(TypeName = "text")]
        public string? EnfermedadConsulta { get; set; }

        [StringLength(500)]
        public string? NombreparienteConsulta { get; set; }

        public int? TipoparienteConsulta { get; set; }

        public int? TelefonoConsulta { get; set; }

        [StringLength(500)]
        public string? TemperaturaConsulta { get; set; }

        [StringLength(500)]
        public string? FrecuenciarespiratoriaConsulta { get; set; }

        [StringLength(500)]
        public string? PresionarterialsistolicaConsulta { get; set; }

        [StringLength(500)]
        public string? PresionarterialdiastolicaConsulta { get; set; }

        [StringLength(500)]
        public string? PulsoConsulta { get; set; }

        [StringLength(500)]
        public string? PesoConsulta { get; set; }

        [StringLength(500)]
        public string? TallaConsulta { get; set; }

        [Column(TypeName = "text")]
        public string? PlantratamientoConsulta { get; set; }

        [Column(TypeName = "text")]
        public string? ObservacionConsulta { get; set; }

        [Column(TypeName = "text")]
        public string? AntecedentespersonalesConsulta { get; set; }

        public int? DiasincapacidadConsulta { get; set; }

        public int? MedicoConsultaD { get; set; }

        public int? EspecialidadConsultaC { get; set; }

        public int? EstadoConsultaC { get; set; }

        public int? TipoConsultaC { get; set; }

        [Column(TypeName = "text")]
        public string? NotasevolucionConsulta { get; set; }

        [Column(TypeName = "text")]
        public string? ConsultaprincipalConsulta { get; set; }

        public int? MedicamentoConsultaM { get; set; }

        public int? DocumentoConsultaD { get; set; }

        public int? DetalleConsultaD { get; set; }

        [StringLength(500)]
        public string? Cardiopatia { get; set; }

        [Column(TypeName = "text")]
        public string? ObserCardiopatia { get; set; }

        [StringLength(500)]
        public string? Diabetes { get; set; }

        [Column(TypeName = "text")]
        public string? ObserDiabetes { get; set; }

        [StringLength(500)]
        public string? EnfCardiovascular { get; set; }

        [Column(TypeName = "text")]
        public string? ObserEnfCardiovascular { get; set; }

        [StringLength(500)]
        public string? Hipertension { get; set; }

        [Column(TypeName = "text")]
        public string? ObserHipertensión { get; set; }

        [StringLength(500)]
        public string? Cancer { get; set; }

        [Column(TypeName = "text")]
        public string? ObserCancer { get; set; }

        [StringLength(500)]
        public string? Tuberculosis { get; set; }

        [Column(TypeName = "text")]
        public string? ObserTuberculosis { get; set; }

        [StringLength(500)]
        public string? EnfMental { get; set; }

        [Column(TypeName = "text")]
        public string? ObserEnfMental { get; set; }

        [StringLength(500)]
        public string? EnfInfecciosa { get; set; }

        [Column(TypeName = "text")]
        public string? ObserEnfInfecciosa { get; set; }

        [StringLength(500)]
        public string? MalFormacion { get; set; }

        [Column(TypeName = "text")]
        public string? ObserMalFormacion { get; set; }

        [StringLength(500)]
        public string? Otro { get; set; }

        [Column(TypeName = "text")]
        public string? ObserOtro { get; set; }

        [StringLength(500)]
        public string? Alergias { get; set; }

        [Column(TypeName = "text")]
        public string? ObserAlergias { get; set; }

        [StringLength(500)]
        public string? Cirugias { get; set; }

        [Column(TypeName = "text")]
        public string? ObserCirugias { get; set; }

        [StringLength(500)]
        public string? OrgSentidos { get; set; }

        [Column(TypeName = "text")]
        public string? ObserOrgSentidos { get; set; }

        [StringLength(500)]
        public string? Respiratorio { get; set; }

        [Column(TypeName = "text")]
        public string? ObserRespiratorio { get; set; }

        [StringLength(500)]
        public string? CardioVascular { get; set; }

        [Column(TypeName = "text")]
        public string? ObserCardioVascular { get; set; }

        [StringLength(500)]
        public string? Digestivo { get; set; }

        [Column(TypeName = "text")]
        public string? ObserDigestivo { get; set; }

        [StringLength(500)]
        public string? Genital { get; set; }

        [Column(TypeName = "text")]
        public string? ObserGenital { get; set; }

        [StringLength(500)]
        public string? Urinario { get; set; }

        [Column(TypeName = "text")]
        public string? ObserUrinario { get; set; }

        [StringLength(500)]
        public string? MEsqueletico { get; set; }

        [Column(TypeName = "text")]
        public string? ObserMEsqueletico { get; set; }

        [StringLength(500)]
        public string? Endocrino { get; set; }

        [Column(TypeName = "text")]
        public string? ObserEndocrino { get; set; }

        [StringLength(500)]
        public string? Linfatico { get; set; }

        [Column(TypeName = "text")]
        public string? ObserLinfatico { get; set; }

        [StringLength(500)]
        public string? Nervioso { get; set; }

        [Column(TypeName = "text")]
        public string? ObserNervioso { get; set; }

        [StringLength(500)]
        public string? Cabeza { get; set; }

        [Column(TypeName = "text")]
        public string? ObserCabeza { get; set; }

        [StringLength(500)]
        public string? Cuello { get; set; }

        [Column(TypeName = "text")]
        public string? ObserCuello { get; set; }

        [StringLength(500)]
        public string? Torax { get; set; }

        [Column(TypeName = "text")]
        public string? ObserTorax { get; set; }

        [StringLength(500)]
        public string? Abdomen { get; set; }

        [Column(TypeName = "text")]
        public string? ObserAbdomen { get; set; }

        [StringLength(500)]
        public string? Pelvis { get; set; }

        [Column(TypeName = "text")]
        public string? ObserPelvis { get; set; }

        [StringLength(500)]
        public string? Extremidades { get; set; }

        [Column(TypeName = "text")]
        public string? ObserExtremidades { get; set; }

        public int? ImagenConsultaI { get; set; }

        public int? LaboratorioConsultaLa { get; set; }

        public int? DiagnosticoConsultaDi { get; set; }

        public int? ActivoConsulta { get; set; }

        public virtual CDetalle? DetalleConsultaDNavigation { get; set; }

        public virtual CDiagnostico? DiagnosticoConsultaDiNavigation { get; set; }

        public virtual CDocumento? DocumentoConsultaDNavigation { get; set; }

        public virtual CImagen? ImagenConsultaINavigation { get; set; }

        public virtual CLaboratorio? LaboratorioConsultaLaNavigation { get; set; }

        public virtual CMedicamento? MedicamentoConsultaMNavigation { get; set; }

        public virtual Paciente? PacienteConsultaPNavigation { get; set; }

        public virtual ICollection<Citum> Cita { get; set; }
    }
}
