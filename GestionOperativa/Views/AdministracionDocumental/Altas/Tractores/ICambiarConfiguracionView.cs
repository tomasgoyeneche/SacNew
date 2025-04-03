using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Tractores
{
    public interface ICambiarConfiguracionView : IViewConMensajes
    {
        string ConfiguracionSeleccionada { get; }

        void CargarOpcionesConfiguracion(List<string> configuraciones);
        void SeleccionarConfiguracionActual(string configuracionActual);
        void ConfigurarVistaPorEntidad(string tipoEntidad);
    }
}
