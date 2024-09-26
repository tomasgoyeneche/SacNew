using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Services
{
    public interface ISesionService
    {
        int IdUsuario { get; }
        string NombreCompleto { get; }
        List<int> Permisos { get; }

        void IniciarSesion(Usuario usuario, List<int> permisos);
        void CerrarSesion();
    }
}
