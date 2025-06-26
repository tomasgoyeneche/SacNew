using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Choferes
{
    public interface IAgregarEditarChoferView : IViewConMensajes
    {
        void MostrarDatosChofer(ChoferDto chofer);

        void MostrarSeguros(List<EmpresaSeguroDto> seguros);

        void ConfigurarFotoChofer(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonAltaTemprana(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonApto(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonCurso(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonDNI(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonLicencia(bool habilitar, string? rutaArchivo);
    }
}