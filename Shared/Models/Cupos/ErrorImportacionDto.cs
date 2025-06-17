using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ErrorImportacionDto
    {
        public int Registro { get; set; }
        public string Columna { get; set; }
        public string Detalle { get; set; }
    }
}
