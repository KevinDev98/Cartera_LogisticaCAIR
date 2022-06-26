using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticaCAIR.Models
{
    public class LoginViewModel
    {
        [BindProperty]//Enlazar las propiedades de tipo modelo
        public InputModel Input { get; set; }

        [TempData]//Mensajes temporales
        public string ErrorMessage { get; set; }
        public class InputModel
        {
            [Required(ErrorMessage ="Ingresar un usuario valido")]                        
            public string UserName { get; set; }
            [Required(ErrorMessage ="Ingresar una contraseña")]
            [DataType(DataType.Password)]
            public string Pass { get; set; }
        }
    }
}
