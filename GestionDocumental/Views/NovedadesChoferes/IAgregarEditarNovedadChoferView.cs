using Core.Interfaces;
using Shared.Models.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDocumental.Views
{
    public interface IAgregarEditarNovedadChoferView : IViewConMensajes
    {
        int IdChofer { get; }
        int IdEstado { get; }
        DateTime FechaInicio { get; }
        DateTime FechaFin { get; }
        string Observaciones { get; }
        bool Disponible { get; }


        void CargarEstados(List<ChoferTipoEstado> estados);

        void CargarChoferes(List<Chofer> choferes);

        void Close();

        void MostrarDatosNovedad(NovedadesChoferesDto novedadChofer);

        void MostrarDiasAusente(int dias);
        void MostrarFechaReincorporacion(DateTime fecha);
    }
}
