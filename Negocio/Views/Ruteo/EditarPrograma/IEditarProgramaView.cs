using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views
{
    public interface IEditarProgramaView : IViewConMensajes
    {
        void MostrarDatos(Shared.Models.Ruteo ruteo, Programa? programa);
        void MostrarArchivos(List<ArchivoDocRuteo> archivos);
        void MostrarAlertas(List<AlertaDto> alertas);
    }
}
