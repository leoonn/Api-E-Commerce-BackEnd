using System.ComponentModel.DataAnnotations;

namespace Login_Backend.Models
{
    public class Address
    {
        public long Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(9)]
        public string Cep { get; set; }
        [Required]
        public string Logradouro { get; set; }
        [Required]
        public long NumberHouse { get; set; }
        public string Complemento { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Localidade { get; set; }
        [Required]
        [MaxLength(2)]
        public string Uf { get; set; }


    }
}
