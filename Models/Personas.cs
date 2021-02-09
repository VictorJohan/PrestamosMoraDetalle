using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroPrestamoBlazor.Models
{
    public class Personas
    {
        [Key]
        public int PersonaId { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir un nombre.")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir un número telefónico .")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir una cédula.")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir una dirección.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Es obligatorio introducir una fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        public double Balance { get; set; }

    }
}
