
using Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


public class Administrador {

    [Key]
    public int? Id { get; set; }

    //[Required(AllowEmptyStrings = false, ErrorMessage = "O login � obrigat�rio.")]
    //[StringLength(20, MinimumLength = 5, ErrorMessage = "Deve possuir uma quantidade de caracteres entre 5 e 20.")]
    //[RegularExpression(@"^[a-zA-Z0-9_]{5,20}$", ErrorMessage = "Digite um login v�lido. Use letras, n�meros e underscore ( _ ).")]
    public string Login { get; set; }

    //[Required(AllowEmptyStrings = false, ErrorMessage = "Senha atual � obrigat�ria.")]
    //[StringLength(100, ErrorMessage = "A senha deve possuir, pelo menos, 5 caracteres em seu tamanho.", MinimumLength = 5)]
    [DataType(DataType.Password)]
    //[RegularExpression(@"^[a-zA-Z0-9_@#$%&]{5,100}$", ErrorMessage = "Digite uma senha v�lida. Use letras, n�meros e os caracteres especiais _ @ # $ % &.")]
    [Display(Name = "Senha")]
    public string Senha { get; set; }

    [Display(Name = "Nome")]
    public string Nome { get; set; }
    public virtual TipoUsuario TipoUsuario { get; set; }
}