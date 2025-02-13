using Core.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Empresas
{
    public interface IAgregarEmpresaSatelitalView : IViewConMensajes
    {
        int IdEmpresa { get; }
        int IdSatelital { get; }
        string Usuario { get; }
        string Clave { get; }

        void CargarSatelitales(List<Satelital> satelitales);
        void Inicializar(int idEmpresa);
    }
}
