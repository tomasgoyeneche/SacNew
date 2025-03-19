using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Tractores
{
    public interface IModificarDatosTractorView
    {
        int IdTractor { get; }
        string Patente { get; }
        DateTime Anio { get; }
        int IdMarca { get; }
        int IdModelo { get; }
        decimal Tara { get; }
        int Hp { get; }
        int Combustible { get; }
        int Cmt { get; }
        int IdEmpresa { get; } // 🔹 Se usa para buscar EmpresaSatelital
        string SatelitalSeleccionado { get; } // 🔹 Megatrans o Sitrack
        DateTime FechaAlta { get; }

        void CargarDatosTractor(Tractor tractor, List<VehiculoMarca> marcas, List<VehiculoModelo> modelos, string SatelitalNombre);
        void CargarModelos(List<VehiculoModelo> modelos);
        void MostrarMensaje(string mensaje);
    }

}
