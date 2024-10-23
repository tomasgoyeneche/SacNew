using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Models
{
    public class LocacionPool
    {
        public int IdLocacionPool { get; set; }  // Identificador único del pool-locación
        public int IdPool { get; set; }          // Identificador del pool (locación principal)
        public int IdLocacion { get; set; }      // Identificador de una locación individual asociada al pool
        public bool Activo { get; set; }         // Estado (activo/inactivo)
    }
}
