using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistruCentras.Web.Models
{
    public class FaqViewModel
    {
        [Required]
        [StringLength(30)]
        public string Question { get; set; }
        [Required]
        [StringLength(100)]
        public string Answer { get; set; }

    }
}
