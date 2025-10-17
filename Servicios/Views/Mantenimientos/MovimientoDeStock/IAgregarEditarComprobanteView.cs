using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Views.Mantenimiento
{
    public interface IAgregarEditarComprobanteView : IViewConMensajes
    {
        int Id { get; set; }
        int? IdTipoComprobante { get; set; }
        string NroComprobante { get; set; }
        int IdProveedor { get; set; }
        bool Activo { get; set; }
        string RutaComprobante { get; set; }
        string Nombre { get; set; }
        void CargarTiposComprobante(List<TipoComprobante> tipos);
        void CargarProveedores(List<ArticuloProveedor> proveedores);

        void MostrarProveedores(bool mostrar);  
        void Cerrar();
    }
}
