using Core.Interfaces;
using Shared.Models;

namespace GestionOperativa
{
    public interface IAgregarEditarTransitoEspecialView : IViewConMensajes
    {
        DateTime Fecha { set; }
        string Cuit { get; }
        string RazonSocial { get; }
        string Nombre { get; }
        string Apellido { get; }
        string Documento { get; }
        DateTime? Licencia { get; }
        DateTime? Art { get; }
        DateTime? Seguro { get; }
        string Tractor { get; set; }
        string Semi { get; }
        string Zona { get; }

        void CargarEmpresasTransitoEspecial(List<TransitoEspecialEmpresaDto> empresas);

        void CompletarEmpresa(string razonSocial, string cuit);

        void Close();
    }
}