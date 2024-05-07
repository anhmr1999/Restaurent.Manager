using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurent.Manager.Models.Datas
{
    public class BillRecord
    {
        public int BillId { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }

        [ForeignKey(nameof(BillId))]
        public virtual Bill Bill { get; set; }
        [ForeignKey(nameof(FoodId))]
        public virtual Food Food { get; set; }
    }
}
