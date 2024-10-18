using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Models.DTOs
{
    public class TicketInfo
    {
        public string Numero { get; }
        public decimal Litros { get; }
        public string Aclaracion { get; }

        public TicketInfo(string numero, string litros, string aclaracion)
        {
            Numero = numero;
            Litros = decimal.TryParse(litros, out var result) ? result : 0;
            Aclaracion = aclaracion;
        }

        public bool EsValido() => !string.IsNullOrWhiteSpace(Numero) && Litros > 0;
    }
}
