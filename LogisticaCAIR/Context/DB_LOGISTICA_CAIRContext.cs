using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LogisticaCAIR.Models;

#nullable disable

namespace LogisticaCAIR.Context
{
    public partial class DB_LOGISTICA_CAIRContext : DbContext
    {
        public DB_LOGISTICA_CAIRContext()
        {
        }

        public DB_LOGISTICA_CAIRContext(DbContextOptions<DB_LOGISTICA_CAIRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CatCartasInstruccion> CatCartasInstruccions { get; set; }
        public virtual DbSet<CatCliente> CatClientes { get; set; }
        public virtual DbSet<CatProveedore> CatProveedores { get; set; }
        public virtual DbSet<CobroCliente> CobroClientes { get; set; }
        public virtual DbSet<Correo> Correos { get; set; }
        public virtual DbSet<FacturasCliente> FacturasClientes { get; set; }
        public virtual DbSet<FacturasProveedore> FacturasProveedores { get; set; }
        public virtual DbSet<HistoricoActividade> HistoricoActividades { get; set; }
        public virtual DbSet<LogsEvent> LogsEvents { get; set; }
        public virtual DbSet<PagoProveedore> PagoProveedores { get; set; }
        public virtual DbSet<Perfile> Perfiles { get; set; }
        public virtual DbSet<RutasAcceso> RutasAccesos { get; set; }
        public virtual DbSet<Tarifa> Tarifas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<ViInfoCobroCliente> ViInfoCobroClientes { get; set; }
        public virtual DbSet<ViInfoFacturaCliente> ViInfoFacturaClientes { get; set; }
        public virtual DbSet<ViInfoFacturaProveedore> ViInfoFacturaProveedores { get; set; }
        public virtual DbSet<ViInfoPagoProv> ViInfoPagoProvs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db_a7efae_dbcair;User ID=sa;Password=inginf98;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<CatCartasInstruccion>(entity =>
            {
                entity.HasKey(e => e.CatCiId)
                    .HasName("PK__CAT_CART__12FC9575ED63F248");

                entity.ToTable("CAT_CARTAS_INSTRUCCION");

                entity.Property(e => e.CatCiId).HasColumnName("CAT_CI_ID");

                entity.Property(e => e.CatCiAduextDireccion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_ADUEXT_DIRECCION");

                entity.Property(e => e.CatCiAduextNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_ADUEXT_NOMBRE");

                entity.Property(e => e.CatCiAduextRfc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_ADUEXT_RFC");

                entity.Property(e => e.CatCiAdunacDireccion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_ADUNAC_DIRECCION");

                entity.Property(e => e.CatCiAdunacNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_ADUNAC_NOMBRE");

                entity.Property(e => e.CatCiAdunacRfc)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_ADUNAC_RFC");

                entity.Property(e => e.CatCiCalveProd)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_CALVE_PROD");

                entity.Property(e => e.CatCiCantidad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_CANTIDAD");

                entity.Property(e => e.CatCiClaveUnidad)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_CLAVE_UNIDAD");

                entity.Property(e => e.CatCiCliente)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_CLIENTE");

                entity.Property(e => e.CatCiCodEmbalaje)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_COD_EMBALAJE");

                entity.Property(e => e.CatCiCodHazmat)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_COD_HAZMAT");

                entity.Property(e => e.CatCiDescripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_DESCRIPCION");

                entity.Property(e => e.CatCiDestinatario)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_DESTINATARIO");

                entity.Property(e => e.CatCiDteCarta)
                    .HasColumnType("date")
                    .HasColumnName("CAT_CI_DTE_CARTA")
                    .HasDefaultValueSql("(CONVERT([date],getdate()))");

                entity.Property(e => e.CatCiFacturaAlanceria)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_FACTURA_ALANCERIA");

                entity.Property(e => e.CatCiFolioUuid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_FOLIO_UUID");

                entity.Property(e => e.CatCiHazmat)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_HAZMAT");

                entity.Property(e => e.CatCiPesoBruto)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_PESO_BRUTO");

                entity.Property(e => e.CatCiPesoNeto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_PESO_NETO");

                entity.Property(e => e.CatCiPesoUnidad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_PESO_UNIDAD");

                entity.Property(e => e.CatCiPlacas)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_PLACAS");

                entity.Property(e => e.CatCiProveedor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_PROVEEDOR");

                entity.Property(e => e.CatCiRefprod)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_REFPROD");

                entity.Property(e => e.CatCiRemitente)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_REMITENTE");

                entity.Property(e => e.CatCiRemolque)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_REMOLQUE");

                entity.Property(e => e.CatCiSubtipo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_SUBTIPO");

                entity.Property(e => e.CatCiTipoMoneda)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_TIPO_MONEDA");

                entity.Property(e => e.CatCiTotalCantidad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_TOTAL_CANTIDAD");

                entity.Property(e => e.CatCiTotalPbruto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_TOTAL_PBRUTO");

                entity.Property(e => e.CatCiTotalPneto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_TOTAL_PNETO");

                entity.Property(e => e.CatCiTransportados)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_TRANSPORTADOS");

                entity.Property(e => e.CatCiUnidad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_UNIDAD");

                entity.Property(e => e.CatCiValor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_VALOR");

                entity.Property(e => e.CatCiValorMoney)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CAT_CI_VALOR_MONEY");

                entity.HasOne(d => d.CatCiClienteNavigation)
                    .WithMany(p => p.CatCartasInstruccions)
                    .HasForeignKey(d => d.CatCiCliente)
                    .HasConstraintName("FK_CLIE_CARTA");

                entity.HasOne(d => d.CatCiProveedorNavigation)
                    .WithMany(p => p.CatCartasInstruccions)
                    .HasForeignKey(d => d.CatCiProveedor)
                    .HasConstraintName("FK_PROV_CARTA");
            });

            modelBuilder.Entity<CatCliente>(entity =>
            {
                entity.HasKey(e => e.CardCode)
                    .HasName("PK_CL");

                entity.ToTable("CAT_CLIENTES");

                entity.Property(e => e.CardCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BancoPreferencia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CardName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DirAddressPatios)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DirAddressPatios2)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DirAddressPatios3)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Email2)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Email3)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Email4)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.NameContact)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NumCta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Num_Cta");

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Tel2)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Tel3)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Tel4)
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CatProveedore>(entity =>
            {
                entity.HasKey(e => e.CardCode)
                    .HasName("PK_PV");

                entity.ToTable("CAT_PROVEEDORES");

                entity.Property(e => e.CardCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BancoPreferencia)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CardName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DirAddressPatios)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DirAddressPatios2)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DirAddressPatios3)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Email2)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Email3)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Email4)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.NameContact)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NumCta)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Num_Cta");

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Tel2)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Tel3)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Tel4)
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CobroCliente>(entity =>
            {
                entity.HasKey(e => e.Docnum)
                    .HasName("PK__COBRO_CL__C27FD8DB38949135");

                entity.ToTable("COBRO_CLIENTES");

                entity.Property(e => e.Docnum).HasColumnName("DOCNUM");

                entity.Property(e => e.BancoEmisor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BANCO_EMISOR");

                entity.Property(e => e.BkClie)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("BK_CLIE");

                entity.Property(e => e.BkUsr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BK_USR");

                entity.Property(e => e.Cancelado)
                    .HasColumnName("CANCELADO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CLIENTE");

                entity.Property(e => e.ConceptoAPagar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO_A_PAGAR");

                entity.Property(e => e.DtePago)
                    .HasColumnType("date")
                    .HasColumnName("DTE_PAGO");

                entity.Property(e => e.DteRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("DTE_REGISTRO")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Iva)
                    .HasColumnType("money")
                    .HasColumnName("IVA");

                entity.Property(e => e.MontoTotal)
                    .HasColumnType("money")
                    .HasColumnName("MONTO_TOTAL");

                entity.Property(e => e.NumCta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NUM_CTA");

                entity.Property(e => e.Pagado).HasColumnName("PAGADO");

                entity.Property(e => e.Retencion)
                    .HasColumnType("money")
                    .HasColumnName("RETENCION");

                entity.Property(e => e.Subtotal)
                    .HasColumnType("money")
                    .HasColumnName("SUBTOTAL");

                entity.Property(e => e.TipoMoneda)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_MONEDA");

                entity.Property(e => e.UrlDocumento)
                    .IsUnicode(false)
                    .HasColumnName("URL_DOCUMENTO");

                entity.Property(e => e.Verificador).HasColumnName("VERIFICADOR");

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.CobroClientes)
                    .HasForeignKey(d => d.Cliente)
                    .HasConstraintName("FK_CCLIE");

                entity.HasOne(d => d.VerificadorNavigation)
                    .WithMany(p => p.CobroClientes)
                    .HasForeignKey(d => d.Verificador)
                    .HasConstraintName("FK_VERIFCLIE");
            });

            modelBuilder.Entity<Correo>(entity =>
            {
                entity.HasKey(e => e.IdCorreo)
                    .HasName("PK__CORREOS__480541B9C471D1DF");

                entity.ToTable("CORREOS");

                entity.Property(e => e.IdCorreo).HasColumnName("ID_CORREO");

                entity.Property(e => e.Activo).HasColumnName("ACTIVO");

                entity.Property(e => e.Correo1)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("CORREO");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TIPO");
            });

            modelBuilder.Entity<FacturasCliente>(entity =>
            {
                entity.HasKey(e => e.FcIdRegistro)
                    .HasName("PK_FCIDR");

                entity.ToTable("FACTURAS_CLIENTES");

                entity.Property(e => e.FcIdRegistro)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FC_ID_REGISTRO")
                    .HasDefaultValueSql("(concat('FCP',replace(replace(replace(CONVERT([varchar](100),format(getdate(),'dd/MM/yy hh:mm:ssss')),'/',''),' ',''),':',''),substring(CONVERT([varchar](50),newid()),(1),(4))))");

                entity.Property(e => e.FcCargoExtra).HasColumnName("FC_CARGO_EXTRA");

                entity.Property(e => e.FcCostoTotal).HasColumnName("FC_COSTO_TOTAL");

                entity.Property(e => e.FcEstatus)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FC_ESTATUS");

                entity.Property(e => e.FcFechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FC_FECHA_CREACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FcFechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("FC_FECHA_INICIO");

                entity.Property(e => e.FcFechaPago)
                    .HasColumnType("date")
                    .HasColumnName("FC_FECHA_PAGO");

                entity.Property(e => e.FcFechaVencimiento)
                    .HasColumnType("date")
                    .HasColumnName("FC_FECHA_VENCIMIENTO");

                entity.Property(e => e.FcFormaPago)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FC_FORMA_PAGO");

                entity.Property(e => e.FcIdclie)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FC_IDCLIE");

                entity.Property(e => e.FcIva).HasColumnName("FC_IVA");

                entity.Property(e => e.FcNumFactCair).HasColumnName("FC_NUM_FACT_CAIR");

                entity.Property(e => e.FcNumeroComplemento)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FC_NUMERO_COMPLEMENTO");

                entity.Property(e => e.FcRetencion).HasColumnName("FC_RETENCION");

                entity.Property(e => e.FcSubtotal).HasColumnName("FC_SUBTOTAL");

                entity.Property(e => e.FcTipoMoneda)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FC_TIPO_MONEDA");

                entity.HasOne(d => d.FcIdclieNavigation)
                    .WithMany(p => p.FacturasClientes)
                    .HasForeignKey(d => d.FcIdclie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IDCLIE1");
            });

            modelBuilder.Entity<FacturasProveedore>(entity =>
            {
                entity.HasKey(e => e.FpIdRegistro)
                    .HasName("PK_FPIDR");

                entity.ToTable("FACTURAS_PROVEEDORES");

                entity.Property(e => e.FpIdRegistro)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FP_ID_REGISTRO")
                    .HasDefaultValueSql("(concat('FP',replace(replace(replace(CONVERT([varchar](100),format(getdate(),'dd/MM/yy hh:mm:ssss')),'/',''),' ',''),':',''),substring(CONVERT([varchar](50),newid()),(1),(9))))");

                entity.Property(e => e.FpCargoExtra).HasColumnName("FP_CARGO_EXTRA");

                entity.Property(e => e.FpCostoTotal).HasColumnName("FP_COSTO_TOTAL");

                entity.Property(e => e.FpEstatus)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FP_ESTATUS");

                entity.Property(e => e.FpFechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FP_FECHA_CREACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FpFechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("FP_FECHA_INICIO");

                entity.Property(e => e.FpFechaPago)
                    .HasColumnType("date")
                    .HasColumnName("FP_FECHA_PAGO");

                entity.Property(e => e.FpFechaVencimiento)
                    .HasColumnType("date")
                    .HasColumnName("FP_FECHA_VENCIMIENTO");

                entity.Property(e => e.FpFormaPago)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FP_FORMA_PAGO");

                entity.Property(e => e.FpIdprov)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FP_IDPROV");

                entity.Property(e => e.FpIva).HasColumnName("FP_IVA");

                entity.Property(e => e.FpNumFactCair).HasColumnName("FP_NUM_FACT_CAIR");

                entity.Property(e => e.FpNumFactProv).HasColumnName("FP_NUM_FACT_PROV");

                entity.Property(e => e.FpNumeroCaja).HasColumnName("FP_NUMERO_CAJA");

                entity.Property(e => e.FpPedimento).HasColumnName("FP_PEDIMENTO");

                entity.Property(e => e.FpReferencia)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FP_REFERENCIA");

                entity.Property(e => e.FpRetencion).HasColumnName("FP_RETENCION");

                entity.Property(e => e.FpSubtotal).HasColumnName("FP_SUBTOTAL");

                entity.Property(e => e.FpTipoMoneda)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FP_TIPO_MONEDA");

                entity.HasOne(d => d.FpIdprovNavigation)
                    .WithMany(p => p.FacturasProveedores)
                    .HasForeignKey(d => d.FpIdprov)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID");
            });

            modelBuilder.Entity<HistoricoActividade>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("HISTORICO_ACTIVIDADES");

                entity.Property(e => e.Cuando)
                    .HasColumnType("datetime")
                    .HasColumnName("CUANDO");

                entity.Property(e => e.Donde)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DONDE");

                entity.Property(e => e.Que)
                    .IsUnicode(false)
                    .HasColumnName("QUE");

                entity.Property(e => e.Quien)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("QUIEN");
            });

            modelBuilder.Entity<LogsEvent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LOGS_EVENTS");

                entity.Property(e => e.Cuando)
                    .HasColumnType("datetime")
                    .HasColumnName("CUANDO")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Que)
                    .IsUnicode(false)
                    .HasColumnName("QUE");
            });

            modelBuilder.Entity<PagoProveedore>(entity =>
            {
                entity.HasKey(e => e.Docnum)
                    .HasName("PK__PAGO_PRO__C27FD8DB5C54F0BB");

                entity.ToTable("PAGO_PROVEEDORES");

                entity.Property(e => e.Docnum).HasColumnName("DOCNUM");

                entity.Property(e => e.BancoReceptor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BANCO_RECEPTOR");

                entity.Property(e => e.BkProv)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BK_PROV");

                entity.Property(e => e.BkUsr)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BK_USR");

                entity.Property(e => e.Cancelado)
                    .HasColumnName("CANCELADO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ConceptoAPagar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("CONCEPTO_A_PAGAR");

                entity.Property(e => e.DepositoCta).HasColumnName("DEPOSITO_CTA");

                entity.Property(e => e.DtePago)
                    .HasColumnType("date")
                    .HasColumnName("DTE_PAGO");

                entity.Property(e => e.DteRegistro)
                    .HasColumnType("date")
                    .HasColumnName("DTE_REGISTRO");

                entity.Property(e => e.EstadoCta).HasColumnName("ESTADO_CTA");

                entity.Property(e => e.Importe).HasColumnName("IMPORTE");

                entity.Property(e => e.Iva).HasColumnName("IVA");

                entity.Property(e => e.NumCta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NUM_CTA");

                entity.Property(e => e.Pagado).HasColumnName("PAGADO");

                entity.Property(e => e.Proveedor)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PROVEEDOR");

                entity.Property(e => e.Retencion).HasColumnName("RETENCION");

                entity.Property(e => e.Tarifa).HasColumnName("TARIFA");

                entity.Property(e => e.TipoMoneda)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_MONEDA");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                entity.Property(e => e.UrlDocumento)
                    .IsUnicode(false)
                    .HasColumnName("URL_DOCUMENTO");

                entity.Property(e => e.Verificador).HasColumnName("VERIFICADOR");

                entity.HasOne(d => d.ProveedorNavigation)
                    .WithMany(p => p.PagoProveedores)
                    .HasForeignKey(d => d.Proveedor)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PV1");

                entity.HasOne(d => d.TarifaNavigation)
                    .WithMany(p => p.PagoProveedores)
                    .HasForeignKey(d => d.Tarifa)
                    .HasConstraintName("FK_TF");

                entity.HasOne(d => d.VerificadorNavigation)
                    .WithMany(p => p.PagoProveedores)
                    .HasForeignKey(d => d.Verificador)
                    .HasConstraintName("FK_VERIF");
            });

            modelBuilder.Entity<Perfile>(entity =>
            {
                entity.HasKey(e => e.IdPerfil)
                    .HasName("PK__PERFILES__90BDE809BF1CF6EF");

                entity.ToTable("PERFILES");

                entity.Property(e => e.IdPerfil).HasColumnName("ID_PERFIL");

                entity.Property(e => e.PerfilName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PERFIL_NAME");

                entity.Property(e => e.PermisoCartas).HasColumnName("PERMISO_CARTAS");

                entity.Property(e => e.PermisoCobroCliente).HasColumnName("PERMISO_COBRO_CLIENTE");

                entity.Property(e => e.PermisoCobroProveedor).HasColumnName("PERMISO_COBRO_PROVEEDOR");

                entity.Property(e => e.PermisoConfiguracion).HasColumnName("PERMISO_CONFIGURACION");

                entity.Property(e => e.PermisoFacturas).HasColumnName("PERMISO_FACTURAS");

                entity.Property(e => e.PermisoTarifas).HasColumnName("PERMISO_TARIFAS");
            });

            modelBuilder.Entity<RutasAcceso>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RUTAS_ACCESO");

                entity.Property(e => e.Modulo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("MODULO");

                entity.Property(e => e.Ruta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("RUTA");
            });

            modelBuilder.Entity<Tarifa>(entity =>
            {
                entity.HasKey(e => e.IdTarifa)
                    .HasName("PK__TARIFAS__8285422CCE59169E");

                entity.ToTable("TARIFAS");

                entity.Property(e => e.IdTarifa).HasColumnName("ID_TARIFA");

                entity.Property(e => e.Cliente)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CLIENTE");

                entity.Property(e => e.TarifExportacion).HasColumnName("TARIF_EXPORTACION");

                entity.Property(e => e.TarifImportacion).HasColumnName("TARIF_IMPORTACION");

                entity.Property(e => e.TipoTarifa)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_TARIFA");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__USUARIOS__91136B9031D0128E");

                entity.ToTable("USUARIOS");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");

                entity.Property(e => e.KeyDrive)
                    .IsUnicode(false)
                    .HasColumnName("KEY_DRIVE");

                entity.Property(e => e.NombreEmpleado)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_EMPLEADO");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("PASS");

                entity.Property(e => e.Perfil).HasColumnName("PERFIL");

                entity.Property(e => e.Useremail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USEREMAIL");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.PerfilNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Perfil)
                    .HasConstraintName("FK_USERS");
            });

            modelBuilder.Entity<ViInfoCobroCliente>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VI_INFO_COBRO_CLIENTES");

                entity.Property(e => e.Banco)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BANCO");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CLIENTE");

                entity.Property(e => e.ConceptoApagar)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ConceptoAPagar");

                entity.Property(e => e.FechaPago)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Iva)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MontoTotal)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroCta)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Retencion)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("subTotal");

                entity.Property(e => e.TipoMoneda)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Verificador)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViInfoFacturaCliente>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VI_INFO_FACTURA_CLIENTES");

                entity.Property(e => e.Clentes)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Estatus)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.FechaFinal)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaInicio)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaPago)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FormaPago)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Iva)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MontoTotal)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Retencion)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("subTotal");

                entity.Property(e => e.TipoMoneda)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViInfoFacturaProveedore>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VI_INFO_FACTURA_PROVEEDORES");

                entity.Property(e => e.Estatus)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.FechaFinal)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaInicio)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaPago)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FormaPago)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Iva)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MontoTotal)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Proveedor)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Retencion)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("subTotal");

                entity.Property(e => e.TipoMoneda)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViInfoPagoProv>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VI_INFO_PAGO_PROV");

                entity.Property(e => e.Banco)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BANCO");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CLIENTE");

                entity.Property(e => e.ConceptoApagar)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ConceptoAPagar");

                entity.Property(e => e.FechaPago)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Iva)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MontoTotal)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroCta)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Retencion)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SubTotal)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("subTotal");

                entity.Property(e => e.TipoMoneda)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Verificador)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
