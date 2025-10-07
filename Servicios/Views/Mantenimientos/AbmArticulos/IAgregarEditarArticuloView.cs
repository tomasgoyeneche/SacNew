using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Views.Mantenimiento
{
    public interface IAgregarEditarArticuloView : IViewConMensajes
    {
        int? IdArticulo { get; }
        string Codigo { get; }
        string Nombre { get; }
        string? Descripcion { get; }
        int IdMedida { get; }
        int IdArticuloFamilia { get; }
        int? IdArticuloMarca { get; }
        int? IdArticuloModelo { get; }
        decimal PrecioUnitario { get; }
        decimal? PedidoMinimo { get; }
        decimal? PedidoMaximo { get; }

        void CargarMedidas(List<Medida> medidas);
        void CargarFamilias(List<ArticuloFamilia> familias);
        void CargarMarcas(List<ArticuloMarca> marcas);
        void CargarModelos(List<ArticuloModelo> modelos);

        void MostrarDatosArticulo(Articulo articulo);
        void Cerrar();
    }

}
