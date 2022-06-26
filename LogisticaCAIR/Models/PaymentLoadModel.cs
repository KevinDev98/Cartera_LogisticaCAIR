using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticaCAIR.Models
{
    public class PaymentLoadModel
    {
        [Display(Name = "Files2")]
        public IFormFile[] Files { get; set; }
    }
}
