using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Expermed.Models
{
    public partial class Base_ExpermedContext : DbContext
    {
        public Base_ExpermedContext()
        {
        }

        public Base_ExpermedContext(DbContextOptions<Base_ExpermedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CDetalle> CDetalles { get; set; } = null!;
        public virtual DbSet<CDocumento> CDocumentos { get; set; } = null!;
        public virtual DbSet<CMedicamento> CMedicamentos { get; set; } = null!;
        public virtual DbSet<Catalogo> Catalogos { get; set; } = null!;
        public virtual DbSet<Citum> Cita { get; set; } = null!;
        public virtual DbSet<Consultum> Consulta { get; set; } = null!;
        public virtual DbSet<Establecimiento> Establecimientos { get; set; } = null!;
        public virtual DbSet<Localidad> Localidads { get; set; } = null!;
        public virtual DbSet<Medicamento> Medicamentos { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;
        public virtual DbSet<Perfil> Perfils { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Base_Expermed;User Id=sa;Password=1717;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CDetalle>(entity =>
            {
                entity.HasKey(e => e.IdDetalle)
                    .HasName("PK__C_Detall__4F1332DE62B6953E");

                entity.ToTable("C_Detalle");

                entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");

                entity.Property(e => e.CantidadDetalle).HasColumnName("cantidad_detalle");

                entity.Property(e => e.FechacreacionDetalle)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_detalle");

                entity.Property(e => e.ObservacionDetalle)
                    .IsUnicode(false)
                    .HasColumnName("observacion_detalle");

                entity.Property(e => e.TipoDetalle).HasColumnName("tipo_detalle");

                entity.Property(e => e.UsuariocreacionDetalle)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariocreacion_detalle");
            });

            modelBuilder.Entity<CDocumento>(entity =>
            {
                entity.HasKey(e => e.IdDocumento)
                    .HasName("PK__C_Docume__5D2EE7E5E4366399");

                entity.ToTable("C_Documento");

                entity.Property(e => e.IdDocumento).HasColumnName("id_documento");

                entity.Property(e => e.FechacreacionDocumento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_documento");

                entity.Property(e => e.MedicoDocumento).HasColumnName("medico_documento");

                entity.Property(e => e.RecomendacionDocumento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("recomendacion_documento");

                entity.Property(e => e.SecuencialDocumento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("secuencial_documento");

                entity.Property(e => e.SignoalarmaDocumento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("signoalarma_documento");

                entity.Property(e => e.TipodocumentoDocumento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("tipodocumento_documento");

                entity.Property(e => e.UsuariocreacionDocumento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariocreacion_documento");
            });

            modelBuilder.Entity<CMedicamento>(entity =>
            {
                entity.HasKey(e => e.IdMedicamento)
                    .HasName("PK__C_Medica__2588C03297E8B52D");

                entity.ToTable("C_Medicamento");

                entity.Property(e => e.IdMedicamento)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_medicamento");

                entity.Property(e => e.CantidadMedicamentoC).HasColumnName("cantidad_medicamento_c");

                entity.Property(e => e.ConsultaMedicamentoC).HasColumnName("consulta_medicamento_c");

                entity.Property(e => e.FechacreacionMedicamento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_medicamento");

                entity.Property(e => e.IdMedicamentosMedicamentoM).HasColumnName("id_medicamentos_medicamento_m");

                entity.Property(e => e.ObservacionMedicamento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("observacion_medicamento");

                entity.Property(e => e.UsuariocreacionMedicamento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariocreacion_medicamento");

                entity.HasOne(d => d.IdMedicamentoNavigation)
                    .WithOne(p => p.CMedicamento)
                    .HasForeignKey<CMedicamento>(d => d.IdMedicamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__C_Medicam__id_me__1EA48E88");
            });

            modelBuilder.Entity<Catalogo>(entity =>
            {
                entity.HasKey(e => e.UuidCatalogo)
                    .HasName("PK__Catalogo__C58C4DEA50673140");

                entity.ToTable("Catalogo");

                entity.Property(e => e.UuidCatalogo)
                    .ValueGeneratedNever()
                    .HasColumnName("uuid_catalogo");

                entity.Property(e => e.ActivoCatalogo).HasColumnName("activo_catalogo");

                entity.Property(e => e.CategoriaCatalogo)
                    .IsUnicode(false)
                    .HasColumnName("categoria_catalogo");

                entity.Property(e => e.DescripcionCatalogo)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_catalogo");

                entity.Property(e => e.FechacreacionCatalogo)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_catalogo");

                entity.Property(e => e.FechamodificacionCatalogo)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_catalogo");

                entity.Property(e => e.IdCatalogo)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_catalogo");

                entity.Property(e => e.UsuariocreacionCatalogo)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariocreacion_catalogo");

                entity.Property(e => e.UsuariomodificacionCatalogo)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariomodificacion_catalogo");
            });

            modelBuilder.Entity<Citum>(entity =>
            {
                entity.HasKey(e => e.IdCitas)
                    .HasName("PK__Citas1__B0CEEC7C16BFF012");

                entity.Property(e => e.IdCitas).HasColumnName("id_citas");

                entity.Property(e => e.ConsultaCitaC).HasColumnName("consulta_cita_c");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('En Curso')");

                entity.Property(e => e.FechacreacionCitas)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_citas");

                entity.Property(e => e.FechadelacitaCitas)
                    .HasColumnType("date")
                    .HasColumnName("fechadelacita_citas");

                entity.Property(e => e.HoradelacitaCitas).HasColumnName("horadelacita_citas");

                entity.Property(e => e.MedicoCitasU).HasColumnName("medico_citas_u");

                entity.Property(e => e.PacienteCitasP).HasColumnName("paciente_citas_p");

                entity.Property(e => e.UsuariocreacionCitas)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariocreacion_citas");

                entity.HasOne(d => d.ConsultaCitaCNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.ConsultaCitaC)
                    .HasConstraintName("FK__Cita__consulta_c__282DF8C2");

                entity.HasOne(d => d.MedicoCitasUNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.MedicoCitasU)
                    .HasConstraintName("FK__Citas__medico_ci__7F2BE32F");

                entity.HasOne(d => d.PacienteCitasPNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.PacienteCitasP)
                    .HasConstraintName("FK__Citas__paciente___00200768");
            });

            modelBuilder.Entity<Consultum>(entity =>
            {
                entity.HasKey(e => e.IdConsulta)
                    .HasName("PK__Consulta__6F53588BA90B8A9D");

                entity.Property(e => e.IdConsulta).HasColumnName("id_consulta");

                entity.Property(e => e.Abdomen)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Alergias)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AntecedentespersonalesConsulta)
                    .IsUnicode(false)
                    .HasColumnName("antecedentespersonales_consulta");

                entity.Property(e => e.Cabeza)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Cancer)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CardioVascular)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Cardio_Vascular");

                entity.Property(e => e.Cardiopatia)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Cirugias)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ConsultaprincipalConsulta)
                    .IsUnicode(false)
                    .HasColumnName("consultaprincipal_consulta");

                entity.Property(e => e.Cuello)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DetalleConsultaD).HasColumnName("detalle_consulta_d");

                entity.Property(e => e.Diabetes)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DiasincapacidadConsulta).HasColumnName("diasincapacidad_consulta");

                entity.Property(e => e.Digestivo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentoConsultaD).HasColumnName("documento_consulta_d");

                entity.Property(e => e.Endocrino)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.EnfCardiovascular)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Enf_Cardiovascular");

                entity.Property(e => e.EnfInfecciosa)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Enf_Infecciosa");

                entity.Property(e => e.EnfMental)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Enf_Mental");

                entity.Property(e => e.EnfermedadConsulta)
                    .IsUnicode(false)
                    .HasColumnName("enfermedad_consulta");

                entity.Property(e => e.EspecialidadConsultaC).HasColumnName("especialidad_consulta_c");

                entity.Property(e => e.EstadoConsultaC).HasColumnName("estado_consulta_c");

                entity.Property(e => e.Extremidades)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FechacreacionConsulta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_consulta");

                entity.Property(e => e.FrecuenciarespiratoriaConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("frecuenciarespiratoria_consulta");

                entity.Property(e => e.Genital)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Hipertension)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.HistorialConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("historial_consulta");

                entity.Property(e => e.Linfatico)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.MEsqueletico)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("M_Esqueletico");

                entity.Property(e => e.MalFormacion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Mal_Formacion");

                entity.Property(e => e.MedicamentoConsultaM).HasColumnName("medicamento_consulta_m");

                entity.Property(e => e.MedicoConsultaD).HasColumnName("medico_consulta_d");

                entity.Property(e => e.MotivoConsulta)
                    .IsUnicode(false)
                    .HasColumnName("motivo_consulta");

                entity.Property(e => e.Nervioso)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NombreparienteConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombrepariente_consulta");

                entity.Property(e => e.NotasevolucionConsulta)
                    .IsUnicode(false)
                    .HasColumnName("notasevolucion_consulta");

                entity.Property(e => e.ObserAbdomen)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Abdomen");

                entity.Property(e => e.ObserAlergias)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Alergias");

                entity.Property(e => e.ObserCabeza)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Cabeza");

                entity.Property(e => e.ObserCancer)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Cancer");

                entity.Property(e => e.ObserCardioVascular)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Cardio_Vascular");

                entity.Property(e => e.ObserCardiopatia)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Cardiopatia");

                entity.Property(e => e.ObserCirugias)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Cirugias");

                entity.Property(e => e.ObserCuello)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Cuello");

                entity.Property(e => e.ObserDiabetes)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Diabetes");

                entity.Property(e => e.ObserDigestivo)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Digestivo");

                entity.Property(e => e.ObserEndocrino)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Endocrino");

                entity.Property(e => e.ObserEnfCardiovascular)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Enf_Cardiovascular");

                entity.Property(e => e.ObserEnfInfecciosa)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Enf_Infecciosa");

                entity.Property(e => e.ObserEnfMental)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Enf_Mental");

                entity.Property(e => e.ObserExtremidades)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Extremidades");

                entity.Property(e => e.ObserGenital)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Genital");

                entity.Property(e => e.ObserHipertensión)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Hipertensión");

                entity.Property(e => e.ObserLinfatico)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Linfatico");

                entity.Property(e => e.ObserMEsqueletico)
                    .IsUnicode(false)
                    .HasColumnName("Obser_M_Esqueletico");

                entity.Property(e => e.ObserMalFormacion)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Mal_Formacion");

                entity.Property(e => e.ObserNervioso)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Nervioso");

                entity.Property(e => e.ObserOrgSentidos)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Org_Sentidos");

                entity.Property(e => e.ObserOtro)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Otro");

                entity.Property(e => e.ObserPelvis)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Pelvis");

                entity.Property(e => e.ObserRespiratorio)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Respiratorio");

                entity.Property(e => e.ObserTorax)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Torax");

                entity.Property(e => e.ObserTuberculosis)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Tuberculosis");

                entity.Property(e => e.ObserUrinario)
                    .IsUnicode(false)
                    .HasColumnName("Obser_Urinario");

                entity.Property(e => e.ObservacionConsulta)
                    .IsUnicode(false)
                    .HasColumnName("observacion_consulta");

                entity.Property(e => e.OrgSentidos)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("Org_Sentidos");

                entity.Property(e => e.Otro)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PacienteConsultaP).HasColumnName("paciente_consulta_p");

                entity.Property(e => e.Pelvis)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PesoConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("peso_consulta");

                entity.Property(e => e.PlantratamientoConsulta)
                    .IsUnicode(false)
                    .HasColumnName("plantratamiento_consulta");

                entity.Property(e => e.PresionarterialdiastolicaConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("presionarterialdiastolica_consulta");

                entity.Property(e => e.PresionarterialsistolicaConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("presionarterialsistolica_consulta");

                entity.Property(e => e.PulsoConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pulso_consulta");

                entity.Property(e => e.Respiratorio)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SecuencialConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("secuencial_consulta");

                entity.Property(e => e.TallaConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("talla_consulta");

                entity.Property(e => e.TelefonoConsulta).HasColumnName("telefono_consulta");

                entity.Property(e => e.TemperaturaConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("temperatura_consulta");

                entity.Property(e => e.TipoConsultaC).HasColumnName("tipo_consulta_c");

                entity.Property(e => e.TipoparienteConsulta).HasColumnName("tipopariente_consulta");

                entity.Property(e => e.Torax)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Tuberculosis)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Urinario)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UsuariocreacionConsulta)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariocreacion_consulta");

                entity.HasOne(d => d.DetalleConsultaDNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.DetalleConsultaD)
                    .HasConstraintName("FK__Consulta__detall__52593CB8");

                entity.HasOne(d => d.DocumentoConsultaDNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.DocumentoConsultaD)
                    .HasConstraintName("FK__Consulta__detall__5070F446");

                entity.HasOne(d => d.MedicamentoConsultaMNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.MedicamentoConsultaM)
                    .HasConstraintName("FK__Consulta__medica__5165187F");

                entity.HasOne(d => d.PacienteConsultaPNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.PacienteConsultaP)
                    .HasConstraintName("FK__Consulta__pacien__534D60F1");
            });

            modelBuilder.Entity<Establecimiento>(entity =>
            {
                entity.HasKey(e => e.IdEstablecimiento)
                    .HasName("PK__Establec__AFEAEA20D414E1E1");

                entity.ToTable("Establecimiento");

                entity.Property(e => e.IdEstablecimiento).HasColumnName("id_establecimiento");

                entity.Property(e => e.ActivoEstablecimiento).HasColumnName("activo_establecimiento");

                entity.Property(e => e.CiudadEstablecimiento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ciudad_establecimiento");

                entity.Property(e => e.DescripcionEstablecimiento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_establecimiento");

                entity.Property(e => e.DireccionEstablecimiento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("direccion_establecimiento");

                entity.Property(e => e.FechacreacionEstablecimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_establecimiento");

                entity.Property(e => e.FechamodificacionEstablecimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_establecimiento");

                entity.Property(e => e.ProvinciaEstablecimiento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("provincia_establecimiento");

                entity.Property(e => e.UsuariocreacionEstablecimiento)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariocreacion_establecimiento");
            });

            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.HasKey(e => e.IdLocalidad)
                    .HasName("PK__Localida__9A5E82AA984BBA1B");

                entity.ToTable("Localidad");

                entity.Property(e => e.IdLocalidad).HasColumnName("id_localidad");

                entity.Property(e => e.ActivoLocalidad).HasColumnName("activo_localidad");

                entity.Property(e => e.CiaLocalidad)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("cia_localidad");

                entity.Property(e => e.CodigoLocalidad)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("codigo_localidad");

                entity.Property(e => e.FechacreacionLocalidad)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_localidad");

                entity.Property(e => e.FechamodificacionLocalidad)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_localidad");

                entity.Property(e => e.GentilicioLocalidad)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("gentilicio_localidad");

                entity.Property(e => e.IsoLocalidad)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("iso_localidad");

                entity.Property(e => e.IsoadLocalidad)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("isoad_localidad");

                entity.Property(e => e.NombreLocalidad)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombre_localidad");

                entity.Property(e => e.PrefijoLocalidad)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("prefijo_localidad");

                entity.Property(e => e.UsuariocreacionLocalidad)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariocreacion_localidad");

                entity.Property(e => e.UsuariomodificacionLocalidad)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariomodificacion_localidad");
            });

            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.HasKey(e => e.IdMedicamentos)
                    .HasName("PK__Medicame__25F30EEDE93DF93C");

                entity.Property(e => e.IdMedicamentos).HasColumnName("id_medicamentos");

                entity.Property(e => e.ActivoMedicamentos).HasColumnName("activo_medicamentos");

                entity.Property(e => e.CategoriaMedicamentos)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("categoria_medicamentos");

                entity.Property(e => e.ConcentracionMedicamentos)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("concentracion_medicamentos");

                entity.Property(e => e.DescripcionMedicamentos)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_medicamentos");

                entity.Property(e => e.FechacreacionMedicamentos)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_medicamentos");

                entity.Property(e => e.FechamodificacionMedicamentos)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_medicamentos");

                entity.Property(e => e.UsuariocreacionMedicamentos)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariocreacion_medicamentos");

                entity.Property(e => e.UsuariomodificacionMedicamentos)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariomodificacion_medicamentos");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPacientes)
                    .HasName("PK__Paciente__D80336DAF791E618");

                entity.Property(e => e.IdPacientes).HasColumnName("id_pacientes");

                entity.Property(e => e.ActivoPacientes).HasColumnName("activo_pacientes");

                entity.Property(e => e.CiPacientes).HasColumnName("ci_pacientes");

                entity.Property(e => e.DireccionPacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("direccion_pacientes");

                entity.Property(e => e.DonantePacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("donante_pacientes");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.EmailPacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("email_pacientes");

                entity.Property(e => e.EmpresaPacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("empresa_pacientes");

                entity.Property(e => e.EstadocivilPacientesC).HasColumnName("estadocivil_pacientes_c");

                entity.Property(e => e.FechacreacionPacientes)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_pacientes");

                entity.Property(e => e.FechamodificacionPacientes)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_pacientes");

                entity.Property(e => e.FechanacimientoPacientes)
                    .HasColumnType("datetime")
                    .HasColumnName("fechanacimiento_pacientes");

                entity.Property(e => e.FormacionprofesionalPacientesC).HasColumnName("formacionprofesional_pacientes_c");

                entity.Property(e => e.NacionalidadPacientesL).HasColumnName("nacionalidad_pacientes_l");

                entity.Property(e => e.OcupacionPacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ocupacion_pacientes");

                entity.Property(e => e.PrimerapellidoPacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("primerapellido_pacientes");

                entity.Property(e => e.PrimernombrePacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("primernombre_pacientes");

                entity.Property(e => e.ProvinciaPacientesL).HasColumnName("provincia_pacientes_l");

                entity.Property(e => e.SegundoapellidoPacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("segundoapellido_pacientes");

                entity.Property(e => e.SegundonombrePacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("segundonombre_pacientes");

                entity.Property(e => e.SegurosaludPacientesC)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("segurosalud_pacientes_c");

                entity.Property(e => e.SexoPacientesC).HasColumnName("sexo_pacientes_c");

                entity.Property(e => e.TelefonocelularPacientes).HasColumnName("telefonocelular_pacientes");

                entity.Property(e => e.TelefonofijoPacientes).HasColumnName("telefonofijo_pacientes");

                entity.Property(e => e.TipodocumentoPacientesC).HasColumnName("tipodocumento_pacientes_c");

                entity.Property(e => e.TiposangrePacientesC).HasColumnName("tiposangre_pacientes_c");

                entity.Property(e => e.UsuariocreacionPacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariocreacion_pacientes");

                entity.Property(e => e.UsuariomodificacionPacientes)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuariomodificacion_pacientes");

                entity.HasOne(d => d.EstadocivilPacientesCNavigation)
                    .WithMany(p => p.PacienteEstadocivilPacientesCNavigations)
                    .HasForeignKey(d => d.EstadocivilPacientesC)
                    .HasConstraintName("FK__Pacientes__estad__44FF419A");

                entity.HasOne(d => d.FormacionprofesionalPacientesCNavigation)
                    .WithMany(p => p.PacienteFormacionprofesionalPacientesCNavigations)
                    .HasForeignKey(d => d.FormacionprofesionalPacientesC)
                    .HasConstraintName("FK__Pacientes__forma__45F365D3");

                entity.HasOne(d => d.NacionalidadPacientesLNavigation)
                    .WithMany(p => p.PacienteNacionalidadPacientesLNavigations)
                    .HasForeignKey(d => d.NacionalidadPacientesL)
                    .HasConstraintName("FK__Pacientes__nacio__46E78A0C");

                entity.HasOne(d => d.ProvinciaPacientesLNavigation)
                    .WithMany(p => p.PacienteProvinciaPacientesLNavigations)
                    .HasForeignKey(d => d.ProvinciaPacientesL)
                    .HasConstraintName("FK__Pacientes__provi__47DBAE45");

                entity.HasOne(d => d.SexoPacientesCNavigation)
                    .WithMany(p => p.PacienteSexoPacientesCNavigations)
                    .HasForeignKey(d => d.SexoPacientesC)
                    .HasConstraintName("FK__Pacientes__sexo___4316F928");

                entity.HasOne(d => d.TipodocumentoPacientesCNavigation)
                    .WithMany(p => p.PacienteTipodocumentoPacientesCNavigations)
                    .HasForeignKey(d => d.TipodocumentoPacientesC)
                    .HasConstraintName("FK__Pacientes__tipod__4222D4EF");

                entity.HasOne(d => d.TiposangrePacientesCNavigation)
                    .WithMany(p => p.PacienteTiposangrePacientesCNavigations)
                    .HasForeignKey(d => d.TiposangrePacientesC)
                    .HasConstraintName("FK__Pacientes__tipos__440B1D61");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil)
                    .HasName("PK__Perfil__1D1C876886368FEF");

                entity.ToTable("Perfil");

                entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");

                entity.Property(e => e.DescripcionPerfil)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_perfil");

                entity.Property(e => e.EspecialidadPerfil)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("especialidad_perfil");

                entity.Property(e => e.FechacreacionPerfil)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_perfil");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__4E3E04AD702575EE");

                entity.ToTable("Usuario");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.ActivoUsuario).HasColumnName("activo_usuario");

                entity.Property(e => e.ApellidosUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("apellidos_usuario");

                entity.Property(e => e.CiUsuario).HasColumnName("ci_usuario");

                entity.Property(e => e.CiudadUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ciudad_usuario");

                entity.Property(e => e.ClaveUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("clave_usuario");

                entity.Property(e => e.CodigoUsuario)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("codigo_usuario");

                entity.Property(e => e.DescripcionUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_usuario");

                entity.Property(e => e.DirecccionestableUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("direcccionestable_usuario");

                entity.Property(e => e.EmailUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("email_usuario");

                entity.Property(e => e.EstablecimientoUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("establecimiento_usuario");

                entity.Property(e => e.FechacreacionUsuario)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_usuario");

                entity.Property(e => e.FechamodificacionUsuario)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_usuario");

                entity.Property(e => e.LoginUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("login_usuario");

                entity.Property(e => e.NombresUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombres_usuario");

                entity.Property(e => e.PerfilUsuarioP).HasColumnName("perfil_usuario_p");

                entity.Property(e => e.ProvinciaUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("provincia_usuario");

                entity.Property(e => e.TelefonoUsuario)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("telefono_usuario");

                entity.HasOne(d => d.PerfilUsuarioPNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.PerfilUsuarioP)
                    .HasConstraintName("FK__Usuario__perfil___3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
