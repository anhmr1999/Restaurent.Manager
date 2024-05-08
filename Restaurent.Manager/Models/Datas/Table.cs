using System.ComponentModel.DataAnnotations;

namespace Restaurent.Manager.Models.Datas
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; } = true;

        public virtual IEnumerable<Bill> Bills { get; set; }
    }
}
