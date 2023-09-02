using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FornecedoresApi.Data.Dtos;
using FornecedoresApi.Models;

namespace FornecedoresApi.Data.Dtos;

public class CriarFornecedorDto 
{
    [Required(ErrorMessage = "A Razão social é obrigatória!")]
    public string RazaoSocial { get; set; }

    public string Endereco { get; set; }

    [Required(ErrorMessage = "Contato e obrigatorio")]  
    public ICollection<Contato> Contatos { get; set; } 
    public class Contato 
    {

        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")]
        public string Email { get; set; }

    }
}