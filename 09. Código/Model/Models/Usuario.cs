
using System;
using System.ComponentModel.DataAnnotations;
namespace Model.Models
{
    public class Usuario
    {
        [Key]
        public int? Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "O login é obrigatório.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Deve possuir uma quantidade de caracteres entre 5 e 20.")]
        [RegularExpression(@"^[a-zA-Z0-9_]{5,20}$", ErrorMessage = "Digite um login válido. Use letras, números e underscore ( _ ).")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha atual é obrigatória.")]
        [StringLength(100, ErrorMessage = "A senha deve possuir, pelo menos, 5 caracteres em seu tamanho.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[a-zA-Z0-9_@#$%&]{5,100}$", ErrorMessage = "Digite uma senha válida. Use letras, números e os caracteres especiais _ @ # $ % &.")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Emprego")]
        public string NomeEmp { get; set; }

        [Display(Name = "Disponibilidade")]
        public bool IsDisponibilidade { get; set; }

        [Display(Name = "Descrição do emprego")]
        public string DescricaoEmp { get; set; }

        [Display(Name = "Rua")]
        public string Rua { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public virtual TipoUsuario TipoUsuario { get; set; }

        //public bool Admin { get; set; }
    }
}