using Core.Interfaces;

namespace GestionOperativa.Views.AgregarGuardia
{
    public interface IAgregarIngresoOtrosView : IViewConMensajes
    {
        DateTime Fecha { set; }
        string Nombre { get; }
        string Apellido { get; }
        string Documento { get; }
        DateTime? Licencia { get; }
        DateTime? Art { get; }
        string TipoIngreso { get; }
        string Patente { get; set; }
        string Empresa { get; }
        string Observaciones { get; }

        void Close();
    }
}