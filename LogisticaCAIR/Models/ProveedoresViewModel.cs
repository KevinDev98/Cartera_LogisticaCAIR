using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticaCAIR.Models
{
    public class ProveedoresViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameContact { get; set; }
        [Required]
        public string Tel { get; set; }
        public string Tel2 { get; set; }
        public string Tel3 { get; set; }
        public string Tel4 { get; set; }
        [Required]
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Email4 { get; set; }
        [Required]
        public string DirPatios { get; set; }
        public string DirPatios2 { get; set; }
        public string DirPatios3 { get; set; }
        [Required]
        public string NumCuenta { get; set; }
        [Required]
        public string Banco { get; set; }
    }
}
