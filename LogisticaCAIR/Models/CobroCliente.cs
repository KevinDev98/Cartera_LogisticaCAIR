using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class CobroCliente
    {
        public int Docnum { get; set; }
        public string Cliente { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Retencion { get; set; }
        public decimal? MontoTotal { get; set; }
        public string TipoMoneda { get; set; }
        public string BancoEmisor { get; set; }
        public string NumCta { get; set; }
        public DateTime? DteRegistro { get; set; }
        public DateTime? DtePago { get; set; }
        public int? Pagado { get; set; }
        public string UrlDocumento { get; set; }
        public int? Verificador { get; set; }
        public int? Cancelado { get; set; }
        public string ConceptoAPagar { get; set; }
        public string BkClie { get; set; }
        public string BkUsr { get; set; }

        public virtual CatCliente ClienteNavigation { get; set; }
        public virtual Usuario VerificadorNavigation { get; set; }
    }
}
