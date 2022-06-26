using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class Perfile
    {
        public Perfile()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdPerfil { get; set; }
        public string PerfilName { get; set; }
        public int? PermisoConfiguracion { get; set; }
        public int? PermisoCobroCliente { get; set; }
        public int? PermisoCobroProveedor { get; set; }
        public int? PermisoFacturas { get; set; }
        public int? PermisoCartas { get; set; }
        public int? PermisoTarifas { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
