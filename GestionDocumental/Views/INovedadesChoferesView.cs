using Core.Interfaces;
using Shared.Models;
using Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDocumental.Views
{
    public interface INovedadesChoferesView : IViewConMensajes
    {
        void MostrarNovedades(List<NovedadesChoferesDto> listaNovedades);

        DialogResult ConfirmarEliminacion(string mensaje);

        void CargarMesesDisponibles(List<(int Mes, int Anio)> meses);

        (int Mes, int Anio) ObtenerMesSeleccionado();

    }
}
