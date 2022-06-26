using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            CobroClientes = new HashSet<CobroCliente>();
            PagoProveedores = new HashSet<PagoProveedore>();
        }

        public int IdUsuario { get; set; }
        public string NombreEmpleado { get; set; }
        public string Username { get; set; }
        public string Useremail { get; set; }
        public int? Perfil { get; set; }
        public string KeyDrive { get; set; }
        public string Pass { get; set; }

        public virtual Perfile PerfilNavigation { get; set; }
        public virtual ICollection<CobroCliente> CobroClientes { get; set; }
        public virtual ICollection<PagoProveedore> PagoProveedores { get; set; }
    }
}
