using Shared.Models;

namespace Core.Repositories
{
    public interface IAlertaRepositorio
    {
        Task<List<AlertaDto?>> ObtenerTodasLasAlertasDtoAsync();

        Task<Alerta?> ObtenerPorIdAsync(int idAlerta);

        Task<List<AlertaDto>> ObtenerAlertasPorIdNominaAsync(int idNomina);

        Task AgregarAlertaAsync(Alerta nuevaAlerta, int idUsuario);

        Task ActualizarAlertaAsync(Alerta alertaActualizada, int idUsuario);

        Task EliminarAlertaAsync(int idPosta);
    }
}