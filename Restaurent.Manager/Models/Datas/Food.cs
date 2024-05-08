using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurent.Manager.Models.Datas
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; } = true;
        public int TypeId { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
