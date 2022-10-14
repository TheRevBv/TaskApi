using System.ComponentModel.DataAnnotations;

namespace TaskApi.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(255)]
        public string Descripcion { get; set; } = string.Empty;
        public bool Estatus { get; set; }
        public DateTime FechaProgramada { get; set; }
        public DateTime FechaTermino { get; set; }

    }
}
