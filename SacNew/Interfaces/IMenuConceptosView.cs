using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IMenuConceptosView
    {
        void MostrarConceptos(List<Concepto> conceptos);

        string TextoBusqueda { get; }

        void MostrarMensaje(string mensaje);
    }
}