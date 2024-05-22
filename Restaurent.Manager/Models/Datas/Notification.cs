using System.ComponentModel.DataAnnotations;

namespace Restaurent.Manager.Models.Datas
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public int? RequiredPayment { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
