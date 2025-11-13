using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vales.Domain
{
    public sealed class Empresa
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = "";
        public string DataSource { get; set; } = "";
        public string Database { get; set; } = "";
        public short Port { get; set; } = 3050;
        public string UserId { get; set; } = "";
        public string Password { get; set; } = "";
        public int Dialect { get; set; } = 3;
        public short? IdCia { get; set; }
        public string Alias { get; set; } = "";
        public string Direccion { get; set; } = "";
        public string Telefono { get; set; } = "";
        public string RutaLogo { get; set; } = "";
        public short Status { get; set; } = 1;
        public int? IdPadre { get; set; }
        public bool ControlTotal { get; set; }
        public bool EsRemoto { get; set; }
        public short Login { get; set; } = 1;
        public short Sync { get; set; } = 0;

        public override string ToString() => string.IsNullOrWhiteSpace(Alias) ? Nombre : $"{Nombre} ({Alias})";
    }
}
