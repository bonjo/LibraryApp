namespace BookSvc.Models
{
    public class LibroDetalleDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Anio { get; set; }
        public decimal Precio { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
    }
}