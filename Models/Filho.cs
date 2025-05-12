using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Filho
    {
        [Key]
        public int FilhoId { get; set; }
        [Required(ErrorMessage = "É necessario informar o nome do filho")]
        [StringLength(100)]
        public string Nome { get; set; }
        public int PaiId { get; set; }
        [JsonIgnore]
        public Pai? Pai { get; set; }
    }
}
