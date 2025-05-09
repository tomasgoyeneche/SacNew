using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Tractores
{
    public interface IAgregarEditarTractorView
    {
        Task CargarDatos(int idTractor);

        void MostrarSeguros(List<EmpresaSeguroDto> seguros);

        void MostrarDatosTractor(TractorDto tractor);

        void ConfigurarFotoTractor(bool habilitar, string? rutaArchivo);

        void ConfigurarFotoManual(bool habilitar, string? rutaArchivo);

        void ConfigurarFotoConfiguracion(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonCedula(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonTitulo(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonRuta(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonVTV(bool habilitar, string? rutaArchivo);

        void MostrarVencimiento(string anioVencimiento);

        void MostrarMensaje(string mensaje);
    }
}