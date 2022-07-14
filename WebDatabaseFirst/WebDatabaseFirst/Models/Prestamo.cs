using System;
using System.Collections.Generic;

#nullable disable

namespace WebDatabaseFirst.Models
{
    public partial class Prestamo
    {
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public int PrestamoLibroId { get; set; }

        public virtual PrestamoLibro Prestamo1 { get; set; }
        public virtual Cliente PrestamoNavigation { get; set; }
    }
}
