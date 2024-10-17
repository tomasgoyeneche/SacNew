using SacNew.Models;

namespace SacNew.Interfaces
{
    public interface IMenuConceptosView: IViewConMensajes
    {
        void MostrarConceptos(List<Concepto> conceptos);

        string TextoBusqueda { get; }


    }
}