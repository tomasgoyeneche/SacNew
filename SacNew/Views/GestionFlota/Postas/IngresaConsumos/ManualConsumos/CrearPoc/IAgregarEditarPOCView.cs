using SacNew.Interfaces;
using SacNew.Models;
using SacNew.Models.DTOs;

namespace SacNew.Views.GestionFlota.Postas.IngresaConsumos.CrearPoc
{
    public interface IAgregarEditarPOCView : IViewConMensajes
    {
        int IdUnidad { get; }
        int IdChofer { get; }
        int IdPosta { get; }
        string NumeroPOC { get; }
        double Odometro { get; }
        string Comentario { get; }
        DateTime FechaCreacion { get; }  // La fecha y hora que se ingresa manualmente
        int IdUsuario { get; }

        void CargarNominas(List<UnidadPatenteDto> unidades);
        void CargarChoferes(List<chofer> choferes);
        void CargarPostas(List<Posta> postas);

        void MostrarDatosPOC(POC poc);
    }
}