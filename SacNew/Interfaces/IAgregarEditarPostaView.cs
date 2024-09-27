using SacNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacNew.Interfaces
{
    public interface IAgregarEditarPostaView
    {
        string Codigo { get; set; }
        string Descripcion { get; set; }
        string Direccion { get; set; }
        int ProvinciaId { get; set; }
        
        int Id { get; set; }
        // Métodos para mostrar mensajes y cargar provincias
        void MostrarMensaje(string mensaje);
        void CargarProvincias(List<Provincia> provincias);
    }
}
