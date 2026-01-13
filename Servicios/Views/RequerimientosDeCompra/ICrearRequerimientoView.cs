using Core.Interfaces;
using Shared.Models.RequerimientoCompra;

namespace Servicios.Views.RequerimientosDeCompra
{
    public interface ICrearRequerimientoView : IViewConMensajes
    {
        // 🔹 Datos principales
        int NumeroRc { set; }

        DateTime Fecha { get; set; }

        int? IdProveedor { get; set; }
        int? IdEmitido { get; set; }
        int? IdAprobado { get; set; }

        string FuncionEmitido { set; }
        string FuncionAprobado { set; }

        string EntregaLugar { get; set; }
        string EntregaFecha { get; set; }
        string Importe { get; set; }
        string CondicionPago { get; set; }
        string Observaciones { get; set; }

        // 🔹 Cargas
        void CargarProveedores(List<ProveedorRcc> proveedores);

        void CargarUsuarios(List<UsuarioRcc> usuarios);

        string DetalleDescripcion { get; }
        decimal DetalleCantidad { get; }

        List<(int IdImputacion, int Porcentaje)> ObtenerImputaciones();

        void MostrarDetalles(List<RcDetalleRcc> detalles);

        void LimpiarDetalle();

        // 🔹 Estado UI
        void BloquearEmitido();

        void Cerrar();
    }
}