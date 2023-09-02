using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FornecedoresApi.Models;
public class Contato 
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")]
        public string Email { get; set; }

        public int FornecedorId { get; set; }

        public virtual Fornecedor Fornecedor{ get; set; }
    }

