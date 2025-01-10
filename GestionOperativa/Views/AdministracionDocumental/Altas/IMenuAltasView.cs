using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public interface IMenuAltasView : IViewConMensajes
    {
        // Métodos para manipular la UI desde el Presenter
        void MostrarEntidades<T>(List<T> entidades);

        // Propiedades de entrada del usuario
        string TextoBusqueda { get; }
    }
}
