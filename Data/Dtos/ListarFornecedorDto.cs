using FornecedoresApi.Models;

namespace FornecedoresApi.Data.Dtos;

public class ListarFornecedorDto 
{
    public int Id { get; set; }
    public string RazaoSocial { get; set; }

    public string Endereco { get; set; }

    public ICollection<ListarContatoDto> Contatos { get; set; }
}