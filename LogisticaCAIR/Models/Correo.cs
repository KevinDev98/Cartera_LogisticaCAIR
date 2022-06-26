using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class Correo
    {
        public int IdCorreo { get; set; }
        public string Correo1 { get; set; }
        public string Tipo { get; set; }
        public int? Activo { get; set; }
    }
}
