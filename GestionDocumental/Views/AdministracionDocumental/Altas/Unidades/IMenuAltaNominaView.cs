using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public interface IMenuAltaNominaView : IViewConMensajes
    {
        void CargarEmpresas(List<EmpresaDto> empresas);

        void MostrarUnidades(List<UnidadDto> unidades);

        void MostrarTransportista(string transportista);

        void MostrarSubTransportista(string subTransportista);

        UnidadDto? ObtenerUnidadSeleccionada();
    }
}