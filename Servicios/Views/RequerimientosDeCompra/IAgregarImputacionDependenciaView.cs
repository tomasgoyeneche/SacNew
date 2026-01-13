using Core.Interfaces;
using Shared.Models;

namespace Servicios.Views.RequerimientosDeCompra
{
    public interface IAgregarImputacionDependenciaView : IViewConMensajes
    {
        void CargarDependencias(List<Dependencia> dependencias);

        void CargarImputaciones(List<Imputacion> imputaciones);

        int Porcentaje { get; }
    }
}