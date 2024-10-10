using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IAgregarEditarLocacionView
    {
        // Propiedades para obtener datos de los campos del formulario
        string Nombre { get; }

        bool Carga { get; }
        bool Descarga { get; }

        // Métodos para mostrar y cargar datos
        void MostrarDatosLocacion(Locacion locacion);

        void CargarProductos(List<LocacionProducto> productos);

        void CargarKilometros(List<LocacionKilometrosEntre> kilometrosEntre);

        void MostrarMensaje(string mensaje);
    }
}