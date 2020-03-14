using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    /// <summary>
    /// Genre Class
    /// </summary>
    public class Genre
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}