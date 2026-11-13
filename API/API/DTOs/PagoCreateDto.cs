
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PagoCreateDto
    {
        [Range(1, int.MaxValue)]
        public int SesionId { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Monto { get; set; }

        [Required]
        [StringLength(50)]
        public string MetodoPago { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Observacion { get; set; }
    }
}
