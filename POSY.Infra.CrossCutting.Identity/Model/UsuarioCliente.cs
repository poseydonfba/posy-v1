using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POSY.Infra.CrossCutting.Identity.Model
{
    [Table("UsuarioCliente")]
    public class UsuarioCliente
    {
        [Key]
        public int UsuarioClienteId { get; set; }
        public string ClientKey { get; set; }
        public Guid? UsuarioId { get; set; }

        public virtual ApplicationUser Usuario { get; set; }
    }
}
