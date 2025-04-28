using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Semis
{
    public interface IAgregarEditarSemiView
    {
        Task CargarDatos(int idSemi);

        void MostrarDatosSemi(SemiDto semi);

        void MostrarSeguros(List<EmpresaSeguroDto> seguros);

        void ConfigurarFotoSemi(bool habilitar, string? rutaArchivo);

        void ConfigurarFotoConfiguracion(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonCedula(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonTitulo(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonRuta(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonVTV(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonEstanqueidad(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonLitrosNominales(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonCubicacion(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonInv(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonEspesor(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonVisualInt(bool habilitar, string? rutaArchivo);

        void ConfigurarBotonVisualExt(bool habilitar, string? rutaArchivo);

        void MostrarVencimiento(string anioVencimiento);

        void MostrarMensaje(string mensaje);
    }
}