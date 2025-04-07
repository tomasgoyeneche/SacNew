using Core.Interfaces;
using Shared.Models;
using Shared.Models.DTOs;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc
{
    public interface IAgregarEditarPOCView : IViewConMensajes
    {
        int IdUnidad { get; }
        int IdChofer { get; }
        int IdPeriodo { get; }
        string NumeroPOC { get; }
        double Odometro { get; }
        string Comentario { get; }
        DateTime FechaCreacion { get; }  // La fecha y hora que se ingresa manualmente
        int IdUsuario { get; }

        void CargarNominas(List<UnidadPatenteDto> unidades);

        void CargarChoferes(List<Chofer> choferes);

        void CargarPeriodo(List<Periodo> periodos);

        void Close();

        void MostrarDatosPOC(POC poc);
    }
}