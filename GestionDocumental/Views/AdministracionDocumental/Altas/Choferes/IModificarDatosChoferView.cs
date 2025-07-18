using Shared.Models;

namespace GestionOperativa.Views.AdministracionDocumental.Altas.Choferes
{
    public interface IModificarDatosChoferView
    {
        int IdChofer { get; }

        string Nombre { get; }
        string Apellido { get; }
        string Documento { get; }

        DateTime FechaNacimiento { get; }

        int idEmpresa { get; }
        bool ZonaFria { get; }
        DateTime FechaAlta { get; }
        int IdProvincia { get; }
        int IdLocalidad { get; }
        string Domicilio { get; }
        string Telefono { get; }
        string Celular { get; }

        void CargarDatosChofer(Chofer chofer, List<EmpresaDto> empresa, List<Provincia> provincias, int idProvincia);

        void CargarLocalidades(List<Localidad> localidades);

        void MostrarMensaje(string mensaje);
    }
}