using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Interfaces
{
    public interface IAgregarKilometrosView
    {
        void CargarLocaciones(List<Locacion> locaciones);
        int IdLocacionDestino { get; }
        decimal Kilometros { get; }
        void MostrarMensaje(string mensaje);
    }
}
