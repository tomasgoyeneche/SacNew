using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    // Interface para la vista
    public interface ICambiarTransportistaView
    {
        void CargarEmpresas(List<EmpresaDto> empresas);
        void SeleccionarEmpresaActual(int idEmpresa);
        int IdEmpresaSeleccionada { get; }
        void MostrarMensaje(string mensaje);
        void Cerrar();
    }
}
