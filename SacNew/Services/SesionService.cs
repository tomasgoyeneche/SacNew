using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Services
{
    public class SesionService : ISesionService
    {
        public int IdUsuario { get; private set; }
        public string NombreCompleto { get; private set; }
        public List<int> Permisos { get; private set; }

        public void IniciarSesion(Usuario usuario, List<int> permisos)
        {
            IdUsuario = usuario.IdUsuario;
            NombreCompleto = usuario.NombreCompleto;
            Permisos = permisos;
        }

        public void CerrarSesion()
        {
            IdUsuario = 0;
            NombreCompleto = string.Empty;
            Permisos = new List<int>();
        }
    }
}
