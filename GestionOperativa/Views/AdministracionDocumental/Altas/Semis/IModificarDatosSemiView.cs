using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Semis
{
    public interface IModificarDatosSemiView : IViewConMensajes
    {
        int IdSemi { get; }
        string Patente { get; }
        DateTime Anio { get; }
        int IdMarca { get; }
        int IdModelo { get; }
        decimal Tara { get; }
        DateTime FechaAlta { get; }
        int IdTipoCarga { get; }
        int Compartimientos { get; }
        int IdMaterial { get; }

        void CargarDatosSemi(ModificarSemiDto semi, List<VehiculoMarca> marcas, List<VehiculoModelo> modelos,
                             List<SemiCisternaTipoCarga> tiposCarga, List<SemiCisternaMaterial> materiales);

        void CargarModelos(List<VehiculoModelo> modelos);

        void MostrarMensaje(string mensaje);
    }
}