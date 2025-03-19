using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Unidades
{
    public interface IAgregarEditarUnidadView : IViewConMensajes
    {
        void MostrarDatosUnidad(UnidadDto unidad);

        void ConfigurarFotoUnidad(bool habilitar, string? rutaArchivo);

        void ConfigurarFotoConfiguracionTractor(bool habilitar, string? rutaArchivo);

        void ConfigurarFotoConfiguracionSemi(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonTaraTotal(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonMasYPF(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonChecklist(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonCalibrado(bool habilitar, string? rutaArchivo);
    }
}