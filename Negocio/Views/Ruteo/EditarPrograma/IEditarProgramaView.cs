using Core.Interfaces;
using Shared.Models;

namespace GestionFlota.Views
{
    public interface IEditarProgramaView : IViewConMensajes
    {
        void MostrarDatos(Shared.Models.Ruteo ruteo, Programa? programa, List<ProgramaExtranjero> hitosExtranjero);

        void MostrarArchivos(List<ArchivoDocRuteo> archivos);

        void MostrarAlertas(List<AlertaDto> alertas);
        void MostrarMantenimientosyFrancos(string manyfrancos);
        void Close();
    }
}