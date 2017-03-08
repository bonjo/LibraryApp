using System.ComponentModel.DataAnnotations;

namespace BookSvc.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Autor
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}