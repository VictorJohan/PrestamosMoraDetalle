using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrestamosMoraDetalle.Models
{
    public class Moras
    {
        [Key]
        public int MoraId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public double Total { get; set; }

        [ForeignKey("MoraId")]
        public virtual List<MorasDetalle> MorasDetalles { get; set; } = new List<MorasDetalle>();
    }
}
