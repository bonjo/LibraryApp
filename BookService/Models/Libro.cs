using System.ComponentModel.DataAnnotations;

namespace BookSvc.Models
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public int Anio { get; set; }
        public decimal Precio { get; set; }
        public string Genero { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}