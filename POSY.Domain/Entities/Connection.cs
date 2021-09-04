using System;

namespace POSY.Domain.Entities
{
    public class Connection
    {
        public string ConnectionId { get; set; }
        public Guid UsuarioId { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
    }
}
