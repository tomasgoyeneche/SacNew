using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Views.AdministracionDocumental.Relevamientos
{
    public interface IFichaTecnicaUnidadesView
    {
        void CargarTransportistas(List<EmpresaDto> transportistas);
        void MostrarUnidades(List<UnidadDto> unidades);

        void MostrarTotales(int cantidadTractores, int cantidadSemis, int cantidadUnidades);
        void MostrarPromedios(double promedioTractor, double promedioSemi, double promedioUnidad);
        void MostrarMensaje(string mensaje);
    }
}
