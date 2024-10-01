using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Interfaces
{
    public interface IAgregarEditarConceptoView
    {
        int Id { get; set; }
        string Codigo { get; }
        string Descripcion { get; }
        int IdTipoConsumo { get; }
        decimal PrecioActual { get; }
        decimal PrecioAnterior { get; }
        DateTime Vigencia { get; }

        int IdProveedorBahiaBlanca { get; }
        int IdProveedorPlazaHuincul { get; }

        void CargarTiposDeConsumo(List<ConceptoTipo> tiposDeConsumo);
        void CargarProveedoresBahiaBlanca(List<Proveedor> proveedores);
        void CargarProveedoresPlazaHuincul(List<Proveedor> proveedores);

        void MostrarDatosConcepto(Concepto concepto);
        void MostrarMensaje(string mensaje);
    }
}
