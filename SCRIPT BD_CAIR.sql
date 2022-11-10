USE [db_a7efae_dbcair]
GO
/****** Object:  Table [dbo].[USUARIOS]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOS](
	[ID_USUARIO] [int] IDENTITY(1,1) NOT NULL,
	[NOMBRE_EMPLEADO] [varchar](100) NOT NULL,
	[USERNAME] [varchar](100) NOT NULL,
	[USEREMAIL] [varchar](50) NOT NULL,
	[PERFIL] [int] NULL,
	[KEY_DRIVE] [varchar](max) NULL,
	[PASS] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_USUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COBRO_CLIENTES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COBRO_CLIENTES](
	[DOCNUM] [int] IDENTITY(1,1) NOT NULL,
	[CLIENTE] [varchar](100) NULL,
	[SUBTOTAL] [money] NULL,
	[IVA] [money] NULL,
	[RETENCION] [money] NULL,
	[MONTO_TOTAL] [money] NULL,
	[TIPO_MONEDA] [varchar](50) NULL,
	[BANCO_EMISOR] [varchar](100) NULL,
	[NUM_CTA] [varchar](100) NULL,
	[DTE_REGISTRO] [datetime] NULL,
	[DTE_PAGO] [date] NULL,
	[PAGADO] [int] NULL,
	[URL_DOCUMENTO] [varchar](max) NULL,
	[VERIFICADOR] [int] NULL,
	[CANCELADO] [int] NULL,
	[CONCEPTO_A_PAGAR] [varchar](500) NULL,
	[BK_CLIE] [varchar](150) NULL,
	[BK_USR] [varchar](50) NULL,
 CONSTRAINT [PK__COBRO_CL__C27FD8DB38949135] PRIMARY KEY CLUSTERED 
(
	[DOCNUM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_CLIENTES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_CLIENTES](
	[CardCode] [varchar](100) NOT NULL,
	[CardName] [varchar](200) NOT NULL,
	[NameContact] [varchar](200) NOT NULL,
	[Tel] [varchar](13) NOT NULL,
	[Tel2] [varchar](13) NULL,
	[Tel3] [varchar](13) NULL,
	[Tel4] [varchar](13) NULL,
	[Email] [varchar](35) NOT NULL,
	[Email2] [varchar](35) NULL,
	[Email3] [varchar](35) NULL,
	[Email4] [varchar](35) NULL,
	[DirAddressPatios] [varchar](1000) NOT NULL,
	[Num_Cta] [varchar](100) NULL,
	[DirAddressPatios2] [varchar](1000) NULL,
	[DirAddressPatios3] [varchar](1000) NULL,
	[BancoPreferencia] [varchar](50) NULL,
 CONSTRAINT [PK_CL] PRIMARY KEY CLUSTERED 
(
	[CardCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VI_INFO_COBRO_CLIENTES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/****** Object:  Table [dbo].[PAGO_PROVEEDORES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PAGO_PROVEEDORES](
	[DOCNUM] [int] IDENTITY(1,1) NOT NULL,
	[PROVEEDOR] [varchar](100) NULL,
	[DTE_REGISTRO] [date] NULL,
	[DTE_PAGO] [date] NULL,
	[ESTADO_CTA] [float] NULL,
	[DEPOSITO_CTA] [float] NULL,
	[CONCEPTO_A_PAGAR] [varchar](500) NULL,
	[TIPO_MONEDA] [varchar](100) NULL,
	[IMPORTE] [float] NOT NULL,
	[IVA] [float] NOT NULL,
	[RETENCION] [float] NOT NULL,
	[TOTAL] [float] NOT NULL,
	[BANCO_RECEPTOR] [varchar](100) NULL,
	[NUM_CTA] [varchar](100) NULL,
	[PAGADO] [int] NULL,
	[URL_DOCUMENTO] [varchar](max) NULL,
	[VERIFICADOR] [int] NULL,
	[TARIFA] [int] NULL,
	[CANCELADO] [int] NULL,
	[BK_PROV] [varchar](100) NULL,
	[BK_USR] [varchar](100) NULL,
 CONSTRAINT [PK__PAGO_PRO__C27FD8DB5C54F0BB] PRIMARY KEY CLUSTERED 
(
	[DOCNUM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_PROVEEDORES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_PROVEEDORES](
	[CardCode] [varchar](100) NOT NULL,
	[CardName] [varchar](200) NOT NULL,
	[NameContact] [varchar](200) NOT NULL,
	[Tel] [varchar](13) NOT NULL,
	[Tel2] [varchar](13) NULL,
	[Tel3] [varchar](13) NULL,
	[Tel4] [varchar](13) NULL,
	[Email] [varchar](35) NOT NULL,
	[Email2] [varchar](35) NULL,
	[Email3] [varchar](35) NULL,
	[Email4] [varchar](35) NULL,
	[DirAddressPatios] [varchar](1000) NOT NULL,
	[Num_Cta] [varchar](100) NOT NULL,
	[BancoPreferencia] [varchar](100) NULL,
	[DirAddressPatios2] [varchar](1000) NULL,
	[DirAddressPatios3] [varchar](1000) NULL,
 CONSTRAINT [PK_PV] PRIMARY KEY CLUSTERED 
(
	[CardCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VI_INFO_PAGO_PROV]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/****** Object:  Table [dbo].[FACTURAS_PROVEEDORES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACTURAS_PROVEEDORES](
	[FP_ID_REGISTRO] [varchar](100) NOT NULL,
	[FP_FECHA_CREACION] [datetime] NULL,
	[FP_FECHA_INICIO] [date] NOT NULL,
	[FP_FECHA_VENCIMIENTO] [date] NOT NULL,
	[FP_NUMERO_CAJA] [int] NOT NULL,
	[FP_PEDIMENTO] [int] NOT NULL,
	[FP_NUM_FACT_PROV] [int] NULL,
	[FP_NUM_FACT_CAIR] [int] NOT NULL,
	[FP_IDPROV] [varchar](100) NOT NULL,
	[FP_TIPO_MONEDA] [varchar](100) NOT NULL,
	[FP_SUBTOTAL] [float] NULL,
	[FP_IVA] [float] NULL,
	[FP_CARGO_EXTRA] [float] NULL,
	[FP_RETENCION] [float] NULL,
	[FP_COSTO_TOTAL] [float] NOT NULL,
	[FP_REFERENCIA] [varchar](100) NOT NULL,
	[FP_FORMA_PAGO] [varchar](100) NOT NULL,
	[FP_ESTATUS] [varchar](10) NOT NULL,
	[FP_FECHA_PAGO] [date] NULL,
 CONSTRAINT [PK_FPIDR] PRIMARY KEY CLUSTERED 
(
	[FP_ID_REGISTRO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VI_INFO_FACTURA_PROVEEDORES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VI_INFO_FACTURA_PROVEEDORES]
AS
SELECT FP_NUM_FACT_CAIR NumDoc, ISNULL(T2.CardName,'Proveedor eliminado') Proveedor 
,T1.FP_TIPO_MONEDA TipoMoneda
,CONVERT(varchar,FORMAT(FP_SUBTOTAL,'C','mx-MX')) subTotal, CONVERT(varchar,FORMAT(FP_IVA,'C','mx-MX')) Iva, CONVERT(varchar,FORMAT(FP_RETENCION,'C','mx-MX')) Retencion
,CONVERT(varchar,FORMAT(FP_COSTO_TOTAL,'C','mx-MX')) MontoTotal
,ISNULL(FP_FORMA_PAGO,'NO PAGADO') FormaPago
,CONVERT(VARCHAR,T1.FP_FECHA_CREACION,103) FechaRegistro
,ISNULL(CONVERT(VARCHAR,FP_FECHA_INICIO,103),'FECHA NO REGISTRADA') FechaInicio
,ISNULL(CONVERT(VARCHAR,FP_FECHA_VENCIMIENTO,103),'FECHA NO REGISTRADA') FechaFinal
,CASE WHEN FP_ESTATUS='NO' THEN 'NO PAGADO' WHEN FP_ESTATUS='C' THEN 'CANCELADO' WHEN FP_ESTATUS='V' THEN 'VENCIDA' ELSE CONVERT(VARCHAR,FP_FECHA_PAGO,103) END FechaPago
,CASE WHEN FP_ESTATUS='NO' THEN 'NO PAGADO' WHEN FP_ESTATUS='SI' THEN 'PAGADO' WHEN FP_ESTATUS='C' THEN 'CANCELADO' WHEN FP_ESTATUS='V' THEN 'VENCIDA' ELSE 'DESCONOCIDO' END Estatus
FROM FACTURAS_PROVEEDORES T1 LEFT JOIN CAT_PROVEEDORES T2 ON FP_IDPROV=CardCode or FP_IDPROV=CardName

GO
/****** Object:  Table [dbo].[FACTURAS_CLIENTES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FACTURAS_CLIENTES](
	[FC_ID_REGISTRO] [varchar](100) NOT NULL,
	[FC_FECHA_CREACION] [datetime] NULL,
	[FC_FECHA_INICIO] [date] NOT NULL,
	[FC_FECHA_VENCIMIENTO] [date] NOT NULL,
	[FC_NUM_FACT_CAIR] [int] NOT NULL,
	[FC_IDCLIE] [varchar](100) NOT NULL,
	[FC_TIPO_MONEDA] [varchar](100) NOT NULL,
	[FC_SUBTOTAL] [float] NULL,
	[FC_IVA] [float] NULL,
	[FC_CARGO_EXTRA] [float] NULL,
	[FC_RETENCION] [float] NULL,
	[FC_COSTO_TOTAL] [float] NOT NULL,
	[FC_NUMERO_COMPLEMENTO] [varchar](100) NULL,
	[FC_FORMA_PAGO] [varchar](100) NOT NULL,
	[FC_ESTATUS] [varchar](10) NOT NULL,
	[FC_FECHA_PAGO] [date] NULL,
 CONSTRAINT [PK_FCIDR] PRIMARY KEY CLUSTERED 
(
	[FC_ID_REGISTRO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VI_INFO_FACTURA_CLIENTES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE VIEW [dbo].[VI_INFO_FACTURA_CLIENTES]
AS
SELECT FC_NUM_FACT_CAIR NumDoc, ISNULL(T2.CardName,'Cliente eliminado') Clentes 
,T1.FC_TIPO_MONEDA TipoMoneda
,CONVERT(varchar,FORMAT(FC_SUBTOTAL,'C','mx-MX')) subTotal, CONVERT(varchar,FORMAT(FC_IVA,'C','mx-MX')) Iva, CONVERT(varchar,FORMAT(FC_RETENCION,'C','mx-MX')) Retencion
,CONVERT(varchar,FORMAT(FC_COSTO_TOTAL,'C','mx-MX')) MontoTotal
,ISNULL(FC_FORMA_PAGO,'NO PAGADO') FormaPago
,CONVERT(VARCHAR,T1.FC_FECHA_CREACION,103) FechaRegistro
,ISNULL(CONVERT(VARCHAR,FC_FECHA_INICIO,103),'FECHA NO REGISTRADA') FechaInicio
,ISNULL(CONVERT(VARCHAR,FC_FECHA_VENCIMIENTO,103),'FECHA NO REGISTRADA') FechaFinal
,CASE WHEN FC_ESTATUS='NO' THEN 'NO PAGADO' WHEN FC_ESTATUS='C' THEN 'CANCELADO' WHEN FC_ESTATUS='V' THEN 'VENCIDA' ELSE CONVERT(VARCHAR,FC_FECHA_PAGO,103) END FechaPago
,CASE WHEN FC_ESTATUS='NO' THEN 'NO PAGADO' WHEN FC_ESTATUS='SI' THEN 'PAGADO' WHEN FC_ESTATUS='C' THEN 'CANCELADO' WHEN FC_ESTATUS='V' THEN 'VENCIDA' ELSE 'DESCONOCIDO' END Estatus
FROM FACTURAS_CLIENTES T1 LEFT JOIN CAT_CLIENTES T2 ON FC_IDCLIE=CardCode or FC_IDCLIE=CardName

GO
/****** Object:  Table [dbo].[CAT_CARTAS_INSTRUCCION]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_CARTAS_INSTRUCCION](
	[CAT_CI_ID] [int] IDENTITY(1,1) NOT NULL,
	[CAT_CI_REMITENTE] [varchar](100) NULL,
	[CAT_CI_DESTINATARIO] [varchar](100) NULL,
	[CAT_CI_DTE_CARTA] [date] NULL,
	[CAT_CI_REMOLQUE] [varchar](20) NULL,
	[CAT_CI_SUBTIPO] [varchar](20) NULL,
	[CAT_CI_PLACAS] [varchar](20) NULL,
	[CAT_CI_REFPROD] [varchar](15) NULL,
	[CAT_CI_CALVE_PROD] [varchar](15) NULL,
	[CAT_CI_DESCRIPCION] [varchar](100) NULL,
	[CAT_CI_TRANSPORTADOS] [varchar](100) NULL,
	[CAT_CI_CANTIDAD] [varchar](50) NULL,
	[CAT_CI_CLAVE_UNIDAD] [varchar](15) NULL,
	[CAT_CI_UNIDAD] [varchar](20) NULL,
	[CAT_CI_HAZMAT] [varchar](5) NULL,
	[CAT_CI_COD_HAZMAT] [varchar](15) NULL,
	[CAT_CI_COD_EMBALAJE] [varchar](15) NULL,
	[CAT_CI_PESO_BRUTO] [varchar](15) NULL,
	[CAT_CI_PESO_NETO] [varchar](20) NULL,
	[CAT_CI_PESO_UNIDAD] [varchar](20) NULL,
	[CAT_CI_VALOR_MONEY] [varchar](20) NULL,
	[CAT_CI_TIPO_MONEDA] [varchar](10) NULL,
	[CAT_CI_FACTURA_ALANCERIA] [varchar](10) NULL,
	[CAT_CI_FOLIO_UUID] [varchar](50) NULL,
	[CAT_CI_TOTAL_CANTIDAD] [varchar](20) NULL,
	[CAT_CI_TOTAL_PBRUTO] [varchar](20) NULL,
	[CAT_CI_TOTAL_PNETO] [varchar](20) NULL,
	[CAT_CI_VALOR] [varchar](20) NULL,
	[CAT_CI_ADUNAC_RFC] [varchar](20) NULL,
	[CAT_CI_ADUNAC_NOMBRE] [varchar](50) NULL,
	[CAT_CI_ADUNAC_DIRECCION] [varchar](150) NULL,
	[CAT_CI_ADUEXT_RFC] [varchar](20) NULL,
	[CAT_CI_ADUEXT_NOMBRE] [varchar](50) NULL,
	[CAT_CI_ADUEXT_DIRECCION] [varchar](150) NULL,
	[CAT_CI_CLIENTE] [varchar](100) NULL,
	[CAT_CI_PROVEEDOR] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CAT_CI_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CORREOS]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CORREOS](
	[ID_CORREO] [int] IDENTITY(1,1) NOT NULL,
	[CORREO] [varchar](70) NOT NULL,
	[TIPO] [varchar](50) NOT NULL,
	[ACTIVO] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_CORREO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HISTORICO_ACTIVIDADES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HISTORICO_ACTIVIDADES](
	[QUIEN] [varchar](100) NULL,
	[QUE] [varchar](max) NULL,
	[DONDE] [varchar](100) NULL,
	[CUANDO] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOGS_EVENTS]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOGS_EVENTS](
	[QUE] [varchar](max) NULL,
	[CUANDO] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PERFILES]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERFILES](
	[ID_PERFIL] [int] IDENTITY(1,1) NOT NULL,
	[PERFIL_NAME] [varchar](100) NOT NULL,
	[PERMISO_CONFIGURACION] [int] NULL,
	[PERMISO_COBRO_CLIENTE] [int] NULL,
	[PERMISO_COBRO_PROVEEDOR] [int] NULL,
	[PERMISO_FACTURAS] [int] NULL,
	[PERMISO_CARTAS] [int] NULL,
	[PERMISO_TARIFAS] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_PERFIL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RUTAS_ACCESO]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RUTAS_ACCESO](
	[RUTA] [varchar](100) NULL,
	[MODULO] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TARIFAS]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TARIFAS](
	[ID_TARIFA] [int] IDENTITY(1,1) NOT NULL,
	[TIPO_TARIFA] [varchar](100) NOT NULL,
	[CLIENTE] [varchar](100) NOT NULL,
	[TARIF_EXPORTACION] [float] NULL,
	[TARIF_IMPORTACION] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_TARIFA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CAT_CARTAS_INSTRUCCION] ADD  DEFAULT (CONVERT([date],getdate())) FOR [CAT_CI_DTE_CARTA]
GO
ALTER TABLE [dbo].[COBRO_CLIENTES] ADD  CONSTRAINT [DF_COBRO_CLIENTES_DTE_REGISTRO]  DEFAULT (getdate()) FOR [DTE_REGISTRO]
GO
ALTER TABLE [dbo].[COBRO_CLIENTES] ADD  CONSTRAINT [DF_COBRO_CLIENTES_CANCELADO]  DEFAULT ((0)) FOR [CANCELADO]
GO
ALTER TABLE [dbo].[FACTURAS_CLIENTES] ADD  CONSTRAINT [DF__FACTURAS___FC_ID__671F4F74]  DEFAULT (concat('FCP',replace(replace(replace(CONVERT([varchar](100),format(getdate(),'dd/MM/yy hh:mm:ssss')),'/',''),' ',''),':',''),substring(CONVERT([varchar](50),newid()),(1),(4)))) FOR [FC_ID_REGISTRO]
GO
ALTER TABLE [dbo].[FACTURAS_CLIENTES] ADD  CONSTRAINT [DF__FACTURAS___FC_FE__681373AD]  DEFAULT (getdate()) FOR [FC_FECHA_CREACION]
GO
ALTER TABLE [dbo].[FACTURAS_PROVEEDORES] ADD  CONSTRAINT [DF__FACTURAS___FP_ID__625A9A57]  DEFAULT (concat('FP',replace(replace(replace(CONVERT([varchar](100),format(getdate(),'dd/MM/yy hh:mm:ssss')),'/',''),' ',''),':',''),substring(CONVERT([varchar](50),newid()),(1),(9)))) FOR [FP_ID_REGISTRO]
GO
ALTER TABLE [dbo].[FACTURAS_PROVEEDORES] ADD  CONSTRAINT [DF__FACTURAS___FP_FE__634EBE90]  DEFAULT (getdate()) FOR [FP_FECHA_CREACION]
GO
ALTER TABLE [dbo].[LOGS_EVENTS] ADD  DEFAULT (getdate()) FOR [CUANDO]
GO
ALTER TABLE [dbo].[PAGO_PROVEEDORES] ADD  CONSTRAINT [DF_PAGO_PROVEEDORES_CANCELADO]  DEFAULT ((0)) FOR [CANCELADO]
GO
ALTER TABLE [dbo].[CAT_CARTAS_INSTRUCCION]  WITH CHECK ADD  CONSTRAINT [FK_CLIE_CARTA] FOREIGN KEY([CAT_CI_CLIENTE])
REFERENCES [dbo].[CAT_CLIENTES] ([CardCode])
GO
ALTER TABLE [dbo].[CAT_CARTAS_INSTRUCCION] CHECK CONSTRAINT [FK_CLIE_CARTA]
GO
ALTER TABLE [dbo].[CAT_CARTAS_INSTRUCCION]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CARTA] FOREIGN KEY([CAT_CI_PROVEEDOR])
REFERENCES [dbo].[CAT_PROVEEDORES] ([CardCode])
GO
ALTER TABLE [dbo].[CAT_CARTAS_INSTRUCCION] CHECK CONSTRAINT [FK_PROV_CARTA]
GO
ALTER TABLE [dbo].[COBRO_CLIENTES]  WITH CHECK ADD  CONSTRAINT [FK_CCLIE] FOREIGN KEY([CLIENTE])
REFERENCES [dbo].[CAT_CLIENTES] ([CardCode])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[COBRO_CLIENTES] CHECK CONSTRAINT [FK_CCLIE]
GO
ALTER TABLE [dbo].[COBRO_CLIENTES]  WITH CHECK ADD  CONSTRAINT [FK_VERIFCLIE] FOREIGN KEY([VERIFICADOR])
REFERENCES [dbo].[USUARIOS] ([ID_USUARIO])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[COBRO_CLIENTES] CHECK CONSTRAINT [FK_VERIFCLIE]
GO
ALTER TABLE [dbo].[FACTURAS_CLIENTES]  WITH NOCHECK ADD  CONSTRAINT [FK_IDCLIE1] FOREIGN KEY([FC_IDCLIE])
REFERENCES [dbo].[CAT_CLIENTES] ([CardCode])
GO
ALTER TABLE [dbo].[FACTURAS_CLIENTES] CHECK CONSTRAINT [FK_IDCLIE1]
GO
ALTER TABLE [dbo].[FACTURAS_PROVEEDORES]  WITH NOCHECK ADD  CONSTRAINT [FK_ID] FOREIGN KEY([FP_IDPROV])
REFERENCES [dbo].[CAT_PROVEEDORES] ([CardCode])
GO
ALTER TABLE [dbo].[FACTURAS_PROVEEDORES] CHECK CONSTRAINT [FK_ID]
GO
ALTER TABLE [dbo].[PAGO_PROVEEDORES]  WITH CHECK ADD  CONSTRAINT [FK_PV1] FOREIGN KEY([PROVEEDOR])
REFERENCES [dbo].[CAT_PROVEEDORES] ([CardCode])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[PAGO_PROVEEDORES] CHECK CONSTRAINT [FK_PV1]
GO
ALTER TABLE [dbo].[PAGO_PROVEEDORES]  WITH CHECK ADD  CONSTRAINT [FK_TF] FOREIGN KEY([TARIFA])
REFERENCES [dbo].[TARIFAS] ([ID_TARIFA])
GO
ALTER TABLE [dbo].[PAGO_PROVEEDORES] CHECK CONSTRAINT [FK_TF]
GO
ALTER TABLE [dbo].[PAGO_PROVEEDORES]  WITH CHECK ADD  CONSTRAINT [FK_VERIF] FOREIGN KEY([VERIFICADOR])
REFERENCES [dbo].[USUARIOS] ([ID_USUARIO])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[PAGO_PROVEEDORES] CHECK CONSTRAINT [FK_VERIF]
GO
ALTER TABLE [dbo].[USUARIOS]  WITH CHECK ADD  CONSTRAINT [FK_USERS] FOREIGN KEY([PERFIL])
REFERENCES [dbo].[PERFILES] ([ID_PERFIL])
GO
ALTER TABLE [dbo].[USUARIOS] CHECK CONSTRAINT [FK_USERS]
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZA_BK]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ACTUALIZA_BK]
AS
BEGIN
	BEGIN TRY
		UPDATE COBRO_CLIENTES SET BK_CLIE=CLIENTE, BK_USR=VERIFICADOR WHERE BK_CLIE IS NULL OR BK_USR IS NULL
		UPDATE PAGO_PROVEEDORES SET BK_PROV=PROVEEDOR , BK_USR=VERIFICADOR WHERE BK_PROV IS NULL OR BK_USR IS NULL
	END TRY
	BEGIN CATCH

	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZA_ESTATUS_FACTURAS]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ACTUALIZA_ESTATUS_FACTURAS]
@BANDERA AS INT,
@TIPO_FACTURA AS VARCHAR(10)=NULL,
@ID_FACTURA AS INT=NULL,
@MONTO AS FLOAT=NULL,
@SC AS VARCHAR(50)=NULL,
@ESTATUS AS VARCHAR(50)=NULL,
@FECHA AS DATE=NULL
AS
BEGIN
	BEGIN TRY
		IF @BANDERA=0 BEGIN
		UPDATE FACTURAS_CLIENTES SET FC_ESTATUS='V' WHERE FORMAT(FC_FECHA_VENCIMIENTO,'dd/MM/yyyy')<FORMAT(GETDATE(),'dd/MM/yyyy') AND FC_ESTATUS<>'SI'
		--UPDATE FACTURAS_CLIENTES SET FC_ESTATUS='SI' WHERE FC_FECHA_PAGO IS NOT NULL AND FC_ESTATUS='V'
		UPDATE FACTURAS_PROVEEDORES SET FP_ESTATUS='V' WHERE FORMAT(FP_FECHA_VENCIMIENTO,'dd/MM/yyyy')<FORMAT(GETDATE(),'dd/MM/yyyy') AND FP_ESTATUS<>'SI'
		SELECT 'OK' OK
	END
	IF @BANDERA=1 BEGIN
		IF @TIPO_FACTURA='C' BEGIN
			UPDATE FACTURAS_CLIENTES SET FC_ESTATUS=@ESTATUS WHERE FC_NUM_FACT_CAIR=@ID_FACTURA AND (FC_IDCLIE=@SC OR FC_IDCLIE=(SELECT CardCode FROM CAT_CLIENTES WHERE CardName=@SC)) AND FC_COSTO_TOTAL=@MONTO
			IF @ESTATUS='SI' BEGIN
				UPDATE FACTURAS_CLIENTES SET FC_FECHA_PAGO=@FECHA WHERE FC_NUM_FACT_CAIR=@ID_FACTURA AND (FC_IDCLIE=@SC OR FC_IDCLIE=(SELECT CardCode FROM CAT_CLIENTES WHERE CardName=@SC)) AND FC_COSTO_TOTAL=@MONTO
			END
			SELECT 'OK' OK,* FROM FACTURAS_CLIENTES WHERE FC_NUM_FACT_CAIR=@ID_FACTURA AND (FC_IDCLIE=@SC OR FC_IDCLIE=(SELECT CardCode FROM CAT_CLIENTES WHERE CardName=@SC)) AND FC_COSTO_TOTAL=@MONTO
			--SELECT 'OK' OK
		END
		ELSE IF @TIPO_FACTURA='P' BEGIN
			UPDATE FACTURAS_PROVEEDORES SET FP_ESTATUS=@ESTATUS WHERE FP_NUM_FACT_CAIR=@ID_FACTURA AND (FP_IDPROV=@SC OR FP_IDPROV=(SELECT CardCode FROM CAT_PROVEEDORES WHERE CardName=@SC)) AND FP_COSTO_TOTAL=@MONTO
			IF @ESTATUS='SI' BEGIN
				UPDATE FACTURAS_PROVEEDORES SET FP_FECHA_PAGO=@FECHA WHERE FP_NUM_FACT_CAIR=@ID_FACTURA AND (FP_IDPROV=@SC OR FP_IDPROV=(SELECT CardCode FROM CAT_PROVEEDORES WHERE CardName=@SC)) AND FP_COSTO_TOTAL=@MONTO
			END
			SELECT 'OK' OK, * FROM FACTURAS_PROVEEDORES WHERE FP_NUM_FACT_CAIR=@ID_FACTURA AND (FP_IDPROV=@SC OR FP_IDPROV=(SELECT CardCode FROM CAT_PROVEEDORES WHERE CardName=@SC)) AND FP_COSTO_TOTAL=@MONTO
		END
	END
	END TRY
	BEGIN CATCH
		SELECT CONCAT('ERROR PROCESO: ', ERROR_MESSAGE())
	END CATCH
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BUSCA_FACTURAS]    Script Date: 10/11/2022 01:02:21 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_BUSCA_FACTURAS]
@BANDERA AS INT=NULL,
@FECHA1 AS DATE=NULL,
@FECHA2 AS DATE=NULL,
@PARAMETRO AS VARCHAR(50)=NULL
AS
BEGIN
	BEGIN TRY
	INSERT INTO LOGS_EVENTS(QUE)VALUES(CONCAT('1000 BANDERA: ', @BANDERA, '- FECHA 1 ',@FECHA1,'- FECHA2 ',@FECHA2,'- PARAMETROS: ',@PARAMETRO))
		IF @BANDERA=0 BEGIN
			IF ISNULL(@PARAMETRO,'')<>'' AND ISNULL(@FECHA1,'')<>'' AND ISNULL(@FECHA2,'')<>'' BEGIN
				INSERT INTO LOGS_EVENTS(QUE)VALUES(CONCAT('1 BANDERA: ', @BANDERA, '- FECHA 1 ',@FECHA1,'- FECHA2 ',@FECHA2,'- PARAMETROS: ',@PARAMETRO))
				SELECT NumDoc,Clentes,TipoMoneda,subTotal,Iva,Retencion,MontoTotal,FechaRegistro,FechaInicio,FechaFinal,FechaPago,Estatus,FormaPago
				FROM VI_INFO_FACTURA_CLIENTES WHERE FORMAT(CONVERT(DATE,FechaFinal,103),'dd/MM/yyyy') BETWEEN Format(CONVERT(DATE,@FECHA1,103),'dd/MM/yyyy') AND Format(CONVERT(DATE,@FECHA2,103),'dd/MM/yyy')
				AND Clentes like CONCAT('%',@PARAMETRO,'%')				
			END
			ELSE IF ISNULL(@PARAMETRO,'')='' AND ISNULL(@FECHA1,'')<>'' AND ISNULL(@FECHA2,'')<>'' BEGIN
			INSERT INTO LOGS_EVENTS(QUE)VALUES(CONCAT('2 BANDERA: ', @BANDERA, '- FECHA 1 ',@FECHA1,'- FECHA2 ',@FECHA2,'- PARAMETROS: ',@PARAMETRO))
				SELECT NumDoc,Clentes,TipoMoneda,subTotal,Iva,Retencion,MontoTotal,FechaRegistro,FechaInicio,FechaFinal,FechaPago,Estatus,FormaPago
				FROM VI_INFO_FACTURA_CLIENTES WHERE FORMAT(CONVERT(DATE,FechaFinal,103),'dd/MM/yyyy') BETWEEN Format(CONVERT(DATE,@FECHA1,103),'dd/MM/yyyy') AND Format(CONVERT(DATE,@FECHA2,103),'dd/MM/yyy')				
			END
			ELSE IF ISNULL(@PARAMETRO,'')!='' AND ISNULL(@FECHA1,'')='' AND ISNULL(@FECHA2,'')='' BEGIN
			INSERT INTO LOGS_EVENTS(QUE)VALUES(CONCAT('3 BANDERA: ', @BANDERA, '- FECHA 1 ',@FECHA1,'- FECHA2 ',@FECHA2,'- PARAMETROS: ',@PARAMETRO))
				SELECT NumDoc,Clentes,TipoMoneda,subTotal,Iva,Retencion,MontoTotal,FechaRegistro,FechaInicio,FechaFinal,FechaPago,Estatus,FormaPago 
				FROM VI_INFO_FACTURA_CLIENTES WHERE Clentes like CONCAT('%',@PARAMETRO,'%')	 or CONCAT(CONVERT(VARCHAR(20),NumDoc),'-',Clentes)=@PARAMETRO
			END
			ELSE BEGIN
			INSERT INTO LOGS_EVENTS(QUE)VALUES(CONCAT('4', @BANDERA, '- FECHA 1 ',@FECHA1,'- FECHA2 ',@FECHA2,'- PARAMETROS',@PARAMETRO))
				SELECT NumDoc,Clentes,TipoMoneda,subTotal,Iva,Retencion,MontoTotal,FechaRegistro,FechaInicio,FechaFinal,FechaPago,Estatus,FormaPago
				FROM VI_INFO_FACTURA_CLIENTES				
			END
		END
		IF @BANDERA=1 BEGIN
			SELECT NumDoc,Proveedor,TipoMoneda,subTotal,Iva,Retencion,MontoTotal,FechaRegistro,FechaInicio,FechaFinal,FechaPago,Estatus,FormaPago
			FROM VI_INFO_FACTURA_PROVEEDORES WHERE FORMAT(CONVERT(DATE,FechaFinal,103),'dd/MM/yyyy') BETWEEN Format(CONVERT(DATE,@FECHA1,103),'dd/MM/yyyy') AND Format(CONVERT(DATE,@FECHA2,103),'dd/MM/yyy')
		END
	END TRY
	BEGIN CATCH
		SELECT ERROR_MESSAGE() clentes
	END CATCH
END
GO
