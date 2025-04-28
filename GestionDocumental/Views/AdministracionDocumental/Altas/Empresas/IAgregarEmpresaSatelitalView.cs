using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Empresas
{
    public interface IAgregarEmpresaSatelitalView : IViewConMensajes
    {
        int IdEmpresa { get; }
        int IdSatelital { get; }
        string Usuario { get; }
        string Clave { get; }

        void CargarSatelitales(List<Satelital> satelitales);

        void Inicializar(int idEmpresa);
    }
}