using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class CatCliente
    {
        public CatCliente()
        {
            CatCartasInstruccions = new HashSet<CatCartasInstruccion>();
            CobroClientes = new HashSet<CobroCliente>();
            FacturasClientes = new HashSet<FacturasCliente>();
        }

        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string NameContact { get; set; }
        public string Tel { get; set; }
        public string Tel2 { get; set; }
        public string Tel3 { get; set; }
        public string Tel4 { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Email4 { get; set; }
        public string DirAddressPatios { get; set; }
        public string NumCta { get; set; }
        public string DirAddressPatios2 { get; set; }
        public string DirAddressPatios3 { get; set; }
        public string BancoPreferencia { get; set; }

        public virtual ICollection<CatCartasInstruccion> CatCartasInstruccions { get; set; }
        public virtual ICollection<CobroCliente> CobroClientes { get; set; }
        public virtual ICollection<FacturasCliente> FacturasClientes { get; set; }
    }
}
