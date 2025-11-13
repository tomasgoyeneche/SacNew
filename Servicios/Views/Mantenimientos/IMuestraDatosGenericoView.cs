using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Servicios.Views.Mantenimientos
{
    public interface IMuestraDatosGenericoView : IViewConMensajes
    {
        void CargarDatos<T>(List<T> datos);
        void MostrarTitulo(string titulo);
    }
}
