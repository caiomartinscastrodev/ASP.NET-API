using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Pai
    {
        [Key]
        public int PaiId { get; set; }
        [Required(ErrorMessage = "É necessario informar o nome do filho")]
        [StringLength(100)]
        public string Nome { get; set; }
        public ICollection<Filho>? Filho { get; set; }
    }
}
