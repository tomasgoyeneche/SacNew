using Core.Interfaces;

namespace GestionDocumental.Views
{
    public interface INovedadesView : IViewConMensajes
    {
        void MostrarNovedades(object listaNovedades);

        DialogResult ConfirmarEliminacion(string mensaje);

        void CargarMesesDisponibles(List<(int Mes, int Anio)> meses);

        (int Mes, int Anio) ObtenerMesSeleccionado();

        bool activoChecked { get; }
    }
}