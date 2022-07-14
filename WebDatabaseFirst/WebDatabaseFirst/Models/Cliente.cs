using System;
using System.Collections.Generic;

#nullable disable

namespace WebDatabaseFirst.Models
{
    public partial class Cliente
    {
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; }
        public string CedulaCliente { get; set; }

        public virtual Prestamo Prestamo { get; set; }
    }
}
