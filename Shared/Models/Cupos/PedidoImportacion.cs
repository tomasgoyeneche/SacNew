using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class PedidoImportacion : INotifyPropertyChanged
    {
        private string producto;
        private string albaranDespacho;
        private string pedidoOR;
        private string nombreCliente;
        private DateTime? fechaEntrega;
        private DateTime? fechaCarga;
        private int? cantidad;
        private string transporte;
        private string codTransporte;
        private string dni;
        private string chofer;
        private string tractor;
        private string semi;
        private string observaciones;

        public string Producto
        {
            get => producto;
            set { if (producto != value) { producto = value; OnPropertyChanged(); } }
        }
        public string AlbaranDespacho
        {
            get => albaranDespacho;
            set { if (albaranDespacho != value) { albaranDespacho = value; OnPropertyChanged(); } }
        }
        public string PedidoOR
        {
            get => pedidoOR;
            set { if (pedidoOR != value) { pedidoOR = value; OnPropertyChanged(); } }
        }
        public string NombreCliente
        {
            get => nombreCliente;
            set { if (nombreCliente != value) { nombreCliente = value; OnPropertyChanged(); } }
        }
        public DateTime? FechaEntrega
        {
            get => fechaEntrega;
            set { if (fechaEntrega != value) { fechaEntrega = value; OnPropertyChanged(); } }
        }
        public DateTime? FechaCarga
        {
            get => fechaCarga;
            set { if (fechaCarga != value) { fechaCarga = value; OnPropertyChanged(); } }
        }
        public int? Cantidad
        {
            get => cantidad;
            set { if (cantidad != value) { cantidad = value; OnPropertyChanged(); } }
        }
        public string Transporte
        {
            get => transporte;
            set { if (transporte != value) { transporte = value; OnPropertyChanged(); } }
        }
        public string CodTransporte
        {
            get => codTransporte;
            set { if (codTransporte != value) { codTransporte = value; OnPropertyChanged(); } }
        }
        public string Dni
        {
            get => dni;
            set { if (dni != value) { dni = value; OnPropertyChanged(); } }
        }
        public string Chofer
        {
            get => chofer;
            set { if (chofer != value) { chofer = value; OnPropertyChanged(); } }
        }
        public string Tractor
        {
            get => tractor;
            set { if (tractor != value) { tractor = value; OnPropertyChanged(); } }
        }
        public string Semi
        {
            get => semi;
            set { if (semi != value) { semi = value; OnPropertyChanged(); } }
        }
        public string Observaciones
        {
            get => observaciones;
            set { if (observaciones != value) { observaciones = value; OnPropertyChanged(); } }
        }

        public override string ToString()
        {
            return $"{Producto}, {AlbaranDespacho}, {PedidoOR}, {NombreCliente}, {FechaEntrega}, {FechaCarga}, {Cantidad}, {Transporte}, {CodTransporte}, {Dni}, {Chofer}, {Tractor}, {Semi}, {Observaciones}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
