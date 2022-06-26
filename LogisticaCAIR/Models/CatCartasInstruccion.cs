using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class CatCartasInstruccion
    {
        public int CatCiId { get; set; }
        public string CatCiRemitente { get; set; }
        public string CatCiDestinatario { get; set; }
        public DateTime? CatCiDteCarta { get; set; }
        public string CatCiRemolque { get; set; }
        public string CatCiSubtipo { get; set; }
        public string CatCiPlacas { get; set; }
        public string CatCiRefprod { get; set; }
        public string CatCiCalveProd { get; set; }
        public string CatCiDescripcion { get; set; }
        public string CatCiTransportados { get; set; }
        public string CatCiCantidad { get; set; }
        public string CatCiClaveUnidad { get; set; }
        public string CatCiUnidad { get; set; }
        public string CatCiHazmat { get; set; }
        public string CatCiCodHazmat { get; set; }
        public string CatCiCodEmbalaje { get; set; }
        public string CatCiPesoBruto { get; set; }
        public string CatCiPesoNeto { get; set; }
        public string CatCiPesoUnidad { get; set; }
        public string CatCiValorMoney { get; set; }
        public string CatCiTipoMoneda { get; set; }
        public string CatCiFacturaAlanceria { get; set; }
        public string CatCiFolioUuid { get; set; }
        public string CatCiTotalCantidad { get; set; }
        public string CatCiTotalPbruto { get; set; }
        public string CatCiTotalPneto { get; set; }
        public string CatCiValor { get; set; }
        public string CatCiAdunacRfc { get; set; }
        public string CatCiAdunacNombre { get; set; }
        public string CatCiAdunacDireccion { get; set; }
        public string CatCiAduextRfc { get; set; }
        public string CatCiAduextNombre { get; set; }
        public string CatCiAduextDireccion { get; set; }
        public string CatCiCliente { get; set; }
        public string CatCiProveedor { get; set; }

        public virtual CatCliente CatCiClienteNavigation { get; set; }
        public virtual CatProveedore CatCiProveedorNavigation { get; set; }
    }
}
