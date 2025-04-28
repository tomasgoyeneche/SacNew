using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public interface IAgregarUnidadView
    {
        void MostrarEmpresa(string nombreEmpresa, int idEmpresa);
        void CargarTractores(List<Tractor> tractores);
        void CargarSemis(List<Semi> semis);
        int IdEmpresa { get; }
        int? IdTractorSeleccionado { get; }
        int? IdSemiSeleccionado { get; }
        bool UsaMetanol { get; }
        bool UsaGasoil { get; }
        bool UsaLujanCuyo { get; }
        bool UsaAptoBo { get; }
        void MostrarMensaje(string mensaje);
        void Cerrar();
    }
}
