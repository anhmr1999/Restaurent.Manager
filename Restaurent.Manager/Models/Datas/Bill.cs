using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurent.Manager.Models.Datas
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public int TableId { get; set; }
        public double SubTotal { get; set; }
        public double ServiceFee { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }

        [ForeignKey(nameof(TableId))]
        public virtual Table Table { get; set; }
        public virtual IEnumerable<BillRecord> Records { get; set; }
    }
}
