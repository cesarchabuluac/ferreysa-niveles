using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niveles.Domain
{
    public sealed class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = "";
        public string NombreCompleto { get; set; } = "";
        public bool Activo { get; set; } = true;
    }
}
