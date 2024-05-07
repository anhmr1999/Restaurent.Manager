using System.ComponentModel.DataAnnotations;

namespace Restaurent.Manager.Models.Datas
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
