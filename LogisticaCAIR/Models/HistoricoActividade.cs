using System;
using System.Collections.Generic;

#nullable disable

namespace LogisticaCAIR.Models
{
    public partial class HistoricoActividade
    {
        public string Quien { get; set; }
        public string Que { get; set; }
        public string Donde { get; set; }
        public DateTime? Cuando { get; set; }
    }
}
