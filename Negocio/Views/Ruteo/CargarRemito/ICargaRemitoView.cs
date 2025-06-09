using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFlota.Views.Ruteo
{
    public interface ICargaRemitoView : IViewConMensajes
    {
        void CargarLocaciones(List<Locacion> locaciones);
        void CargarProductos(List<Producto> productos);
        void CargarMedidas(List<Medida> medidas);
        void CargarDatosIniciales(Programa programa, int idDestino, string tipoRemito);
        string RemitoNumero { get; }
        DateTime? FechaRemito { get; }
        int? IdMedida { get; }
        int? Cantidad { get; }
        int? IdProducto { get; }
        int? IdOrigen { get; }
        void Close();
    }
}
