using System.ComponentModel.DataAnnotations;
using FornecedoresApi.Models;

namespace FornecedoresApi.Data.Dtos;

public class AlterarFornecedorDto 
{

    [Required(ErrorMessage = "A Razão social é obrigatória!")]
    public string RazaoSocial { get; set; }

    public string Endereco { get; set; }

    [Required(ErrorMessage = "Contato e obrigatorio")]  
    public ICollection<AlterarContatoDto> Contatos { get; set; }
}