using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IAgregarEditarLocacionView : IViewConMensajes
    {
        // Propiedades para obtener datos de los campos del formulario
        string Nombre { get; }

        string Direccion { get; }
        bool Carga { get; }
        bool Descarga { get; }

        // Métodos para mostrar y cargar datos
        void MostrarDatosLocacion(Locacion locacion);

        void CargarProductos(List<LocacionProducto> productos);

        void EstablecerModoEdicion(bool habilitar);

        void CargarKilometros(List<LocacionKilometrosEntre> kilometrosEntre);

        DialogResult ConfirmarEliminacion(string mensaje);
    }
}