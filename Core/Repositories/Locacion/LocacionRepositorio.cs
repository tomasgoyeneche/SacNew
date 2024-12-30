using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class LocacionRepositorio : BaseRepositorio, ILocacionRepositorio
    {
        public LocacionRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
        : base(connectionStrings, sesionService)
        {
        }

        public Task<List<Locacion>> ObtenerTodasAsync()
        {
            var query = "SELECT * FROM Locacion WHERE Activo = 1";

            return ConectarAsync(connection =>
            {
                return connection.QueryAsync<Locacion>(query)
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public Task<Locacion?> ObtenerPorIdAsync(int idLocacion)
        {
            var query = "SELECT * FROM Locacion WHERE IdLocacion = @IdLocacion";

            return ConectarAsync(connection =>
            {
                return connection.QueryFirstOrDefaultAsync<Locacion?>(query, new { IdLocacion = idLocacion });
            });
        }

        public Task<List<Locacion>> BuscarPorCriterioAsync(string criterio)
        {
            var query = "SELECT * FROM Locacion WHERE Activo = 1 AND (Nombre LIKE @Criterio OR Direccion LIKE @Criterio)";

            return ConectarAsync(connection =>
            {
                return connection.QueryAsync<Locacion>(query, new { Criterio = $"%{criterio}%" })
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public Task AgregarAsync(Locacion locacion)
        {
            var query = @"
            INSERT INTO Locacion (Nombre, Direccion, Carga, Descarga, Activo)
            VALUES (@Nombre, @Direccion, @Carga, @Descarga, @Activo)";

            // No hay necesidad de serializar los valores manualmente
            return EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, locacion),
                "Locacion",
                "INSERT",
                null,  // No hay valores anteriores porque es un nuevo registro
                locacion  // Pasamos el objeto directamente, el BaseRepositorio lo serializa
            );
        }

        public async Task ActualizarAsync(Locacion locacion)
        {
            // Obtener los valores anteriores antes de la actualización para la auditoría
            var locacionAnterior = await ObtenerPorIdAsync(locacion.IdLocacion);

            var query = @"
            UPDATE Locacion
            SET Nombre = @Nombre, Direccion = @Direccion, Carga = @Carga, Descarga = @Descarga, Activo = @Activo
            WHERE IdLocacion = @IdLocacion";

            // Llamar a EjecutarConAuditoriaAsync para ejecutar la consulta y registrar auditoría
            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, locacion),
                "Locacion",
                "UPDATE",
                locacionAnterior,  // Pasamos los valores anteriores sin serializarlos
                locacion  // Pasamos los valores nuevos sin serializarlos
            );
        }

        public async Task EliminarAsync(int idLocacion)
        {
            var query = "UPDATE Locacion SET Activo = 0 WHERE IdLocacion = @IdLocacion";

            // Obtener los valores anteriores antes de la eliminación
            var locacionAnterior = await ObtenerPorIdAsync(idLocacion);

            // Registrar el cambio de estado como "Eliminado" en la auditoría
            await EjecutarConAuditoriaAsync(
                connection => connection.ExecuteAsync(query, new { IdLocacion = idLocacion }),
                "Locacion",
                "DELETE",
                locacionAnterior,  // Pasamos los valores anteriores sin serializarlos
                null  // No hay valores nuevos ya que solo se desactiva el registro
            );
        }
    }
}