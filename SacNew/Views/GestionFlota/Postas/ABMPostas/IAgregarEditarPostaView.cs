using SacNew.Interfaces;
using SacNew.Models;

namespace SacNew.Views.GestionFlota.Postas.ABMPostas
{
    public interface IAgregarEditarPostaView : IViewConMensajes
    {
        string Codigo { get; set; }
        string Descripcion { get; set; }
        string Direccion { get; set; }
        int ProvinciaId { get; set; }

        int Id { get; set; }

        // Métodos para mostrar mensajes y cargar provincias

        void CargarProvincias(List<Provincia> provincias);
    }
}