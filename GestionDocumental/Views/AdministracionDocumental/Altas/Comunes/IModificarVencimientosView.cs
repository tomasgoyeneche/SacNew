namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public interface IModificarVencimientosView
    {
        void MostrarVencimientos(Dictionary<int, (string etiqueta, DateTime? fecha)> vencimientos);

        void MostrarMensaje(string mensaje);

        Dictionary<int, DateTime?> ObtenerFechasActualizadas();

        void MostrarTara(bool Visible, int? Tara);

        int Tara { get; }
    }
}