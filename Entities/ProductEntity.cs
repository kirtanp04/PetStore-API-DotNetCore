using System.ComponentModel.DataAnnotations;

namespace Server.Entities
{
    public class ProductEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Brand { get; set; }
        public string Title { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;

    }
}
