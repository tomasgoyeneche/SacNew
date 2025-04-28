using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Empresas
{
    public interface IModificarDatosEmpresaView : IViewConMensajes
    {
        int IdEmpresa { get; }
        string Cuit { get; }
        int IdEmpresaTipo { get; }
        string RazonSocial { get; }
        string NombreFantasia { get; }
        int IdProvincia { get; }
        int IdLocalidad { get; }
        string Domicilio { get; }
        string Telefono { get; }
        string Email { get; }

        void CargarDatosEmpresa(Empresa empresa, List<EmpresaTipo> tipos, List<Provincia> provincias);

        void CargarLocalidades(List<Localidad> localidades);
    }
}