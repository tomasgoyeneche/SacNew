using SacNew.Models;
using SacNew.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Interfaces
{
    public interface ILoginView
    {
        string NombreUsuario { get; }
        string Contrasena { get; }
        void MostrarMensaje(string mensaje);

        void RedirigirAlMenu(Menu menuform);
    }
}
