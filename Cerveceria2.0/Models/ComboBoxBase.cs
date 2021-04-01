using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria2._0.Models
{
    public class ComboBoxBase
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Por favor, ingrese una descripción.")]
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
