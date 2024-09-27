using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Interfaces
{
    public interface IMenuAbmPostasView
    {
        // Métodos para manipular la UI desde el Presenter
        void MostrarPostas(List<Posta> postas);
        void MostrarMensaje(string mensaje);

        // Propiedades de entrada del usuario
        string TextoBusqueda { get; }
    }
}
