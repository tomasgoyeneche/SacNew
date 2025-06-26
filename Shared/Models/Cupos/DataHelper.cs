using System.ComponentModel;

namespace Shared.Models
{
    public static class DataHelper
    {
        public static BindingList<PedidoImportacion> GetData(int count)
        {
            var pedidos = new BindingList<PedidoImportacion>();
            for (int i = 0; i < count; i++)
            {
                pedidos.Add(new PedidoImportacion
                {
                    Producto = $"Producto {i}",
                    AlbaranDespacho = $"Albaran {i:D5}",
                    PedidoOR = $"OR-{1000 + i}",
                    NombreCliente = $"Cliente {i}",
                    FechaEntrega = DateTime.Today.AddDays(i),
                    FechaCarga = DateTime.Today.AddDays(i - 1),
                    Cantidad = i * 10,
                    Transporte = $"Transporte {i % 5}",
                    CodTransporte = $"CT-{i % 10}",
                    Dni = (30000000 + i).ToString(),
                    Chofer = $"Chofer {i % 7}",
                    Tractor = $"AA{i:000}BB",
                    Semi = $"CC{i:000}DD",
                    Observaciones = i % 2 == 0 ? "Ninguna" : $"Obs {i}"
                });
            }
            return pedidos;
        }
    }
}