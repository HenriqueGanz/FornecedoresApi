using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FornecedoresApi.Models;

public class Fornecedor 
{
    [Key]
    [Required]
    public int Id { get; set; }

    public string RazaoSocial { get; set; }

    public string Endereco { get; set; }

    public virtual ICollection<Contato> Contatos { get; set; }

}