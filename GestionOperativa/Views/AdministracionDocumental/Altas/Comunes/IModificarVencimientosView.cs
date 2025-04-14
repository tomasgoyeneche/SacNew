using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Views.AdministracionDocumental.Altas
{
    public interface IModificarVencimientosView
    {
        void MostrarVencimientos(Dictionary<int, (string etiqueta, DateTime? fecha)> vencimientos);
        void MostrarMensaje(string mensaje);
        Dictionary<int, DateTime?> ObtenerFechasActualizadas();
    }
}
