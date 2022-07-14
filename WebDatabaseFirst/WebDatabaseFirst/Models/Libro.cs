using System;
using System.Collections.Generic;

#nullable disable

namespace WebDatabaseFirst.Models
{
    public partial class Libro
    {
        public int LibroId { get; set; }
        public string NombreLibro { get; set; }
        public string AutorLibro { get; set; }

        public virtual PrestamoLibro PrestamoLibro { get; set; }
    }
}
