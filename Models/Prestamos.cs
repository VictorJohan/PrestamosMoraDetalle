using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroPrestamoBlazor.Models
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        [Required(ErrorMessage ="Es obligatorio introducir un concepto.")]
        public string Concepto { get; set; }
        [Range(minimum:100, maximum:1000000)]
        public double Monto { get; set; }
        public double Balance { get; set; }
        [Required(ErrorMessage ="Debe seleccionar un persona para asignar el prestamo.")]
        public int PersonaId { get; set; }

        [ForeignKey("PersonaId")]
        public virtual Personas Persona { get; set; }
    }
}
