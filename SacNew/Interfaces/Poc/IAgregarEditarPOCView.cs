using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IAgregarEditarPOCView: IViewConMensajes
    {
        int IdNomina { get; }
        int IdPosta { get; }
        string NumeroPOC { get; }
        double Odometro { get; }
        string Comentario { get; }
        DateTime FechaCreacion { get; }  // La fecha y hora que se ingresa manualmente
        int IdUsuario { get; }

        void CargarNominas(List<Nomina> nominas);

        void CargarPostas(List<Posta> postas);

        void MostrarDatosPOC(POC poc);
    }
}