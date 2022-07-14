using System;
using System.Collections.Generic;

#nullable disable

namespace WebDatabaseFirst.Models
{
    public partial class PrestamoLibro
    {
        public int PrestamoLibroId { get; set; }
        public int LibroId { get; set; }
        public int PrestamoId { get; set; }

        public virtual Libro PrestamoLibroNavigation { get; set; }
        public virtual Prestamo Prestamo { get; set; }
    }
}
