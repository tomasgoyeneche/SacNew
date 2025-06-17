using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views
{
    public interface ICambioChoferView : IViewConMensajes
    {
        void CargarChoferes(List<ChoferDto> choferes);
        void CargarFrancos(List<NovedadesChoferesDto> francos);
        int? IdChoferSeleccionado { get; }
        DateTime FechaCambio { get; }
        string NombreChoferSeleccionado { get; } // Para descripción de registro
        void Cerrar();
    }
}
