using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Empresas
{
    public interface IAgregarEditarEmpresaView
    {
        void MostrarDatosEmpresa(EmpresaDto empresa); // Muestra los datos de la empresa

        int IdEmpresa { get; } // ID de la empresa, útil para acciones futuras

        void MostrarMensaje(string mensaje); // Muestra mensajes informativos o de error

        void HabilitarBotonLegajoArt(bool habilitar);

        void ConfigurarRutaArchivoArt(string rutaArchivo);

        void HabilitarBotonLegajoCuit(bool habilitar);

        void ConfigurarRutaArchivoCuit(string rutaArchivo);

        void MostrarSatelitales(List<EmpresaSatelitalDto> satelitales);

        void MostrarPaises(List<EmpresaPaisDto> paises);
    }
}