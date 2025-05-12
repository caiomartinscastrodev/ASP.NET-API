using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Nome de usuario obrigatório!")]
        [Column(name: "username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Senha obrigatória!")]
        [Column(name: "password")]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "E-mail obrigatório")]
        [EmailAddress]
        [Column(name: "email")]
        public string Email { get; set; }
    }
}
