using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public interface IAltaEntidadView
    {
        string Campo1 { get; }
        string Campo2 { get; }
        string Campo3 { get; }

        void ConfigurarCampos(string entidad);
        void MostrarMensaje(string mensaje);
        void Cerrar();
    }
}
