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

        int Inv { get; }
        int LitroNominal { get; }
        int Cubicacion { get; }

        void CargarDatosSemi(Semi semi, List<VehiculoMarca> marcas, List<VehiculoModelo> modelos,
                             List<SemiCisternaTipoCarga> tiposCarga, List<SemiCisternaMaterial> materiales, string litros);

        void CargarModelos(List<VehiculoModelo> modelos);

        void ActualizarConfeccion(string confeccion);

        void MostrarMensaje(string mensaje);
    }
}