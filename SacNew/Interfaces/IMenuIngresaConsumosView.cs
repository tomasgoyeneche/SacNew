using SacNew.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Interfaces
{
    public interface IMenuIngresaConsumosView
    {
        void MostrarPOC(List<POCDto> listaPOC);
        void MostrarMensaje(string mensaje);
        void MostrarNombreUsuario(string nombre);
        DialogResult ConfirmarEliminacion(string mensaje);
    }
}
