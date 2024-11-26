using SacNew.Interfaces;
using SacNew.Models;

namespace SacNew.Views.GestionFlota.Postas.ConceptoConsumos
{
    public interface IMenuConceptosView : IViewConMensajes
    {
        Task MostrarConceptosAsync(List<Concepto> conceptos);

        string TextoBusqueda { get; }
    }
}