using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class PagoProveedore
    {
        public int Docnum { get; set; }
        public string Proveedor { get; set; }
        public DateTime? DteRegistro { get; set; }
        public DateTime? DtePago { get; set; }
        public double? EstadoCta { get; set; }
        public double? DepositoCta { get; set; }
        public string ConceptoAPagar { get; set; }
        public string TipoMoneda { get; set; }
        public double Importe { get; set; }
        public double Iva { get; set; }
        public double Retencion { get; set; }
        public double Total { get; set; }
        public string BancoReceptor { get; set; }
        public string NumCta { get; set; }
        public int? Pagado { get; set; }
        public string UrlDocumento { get; set; }
        public int? Verificador { get; set; }
        public int? Tarifa { get; set; }
        public int? Cancelado { get; set; }
        public string BkProv { get; set; }
        public string BkUsr { get; set; }

        public virtual CatProveedore ProveedorNavigation { get; set; }
        public virtual Tarifa TarifaNavigation { get; set; }
        public virtual Usuario VerificadorNavigation { get; set; }
    }
}
