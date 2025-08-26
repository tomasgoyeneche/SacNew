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
            var query = "SELECT * FROM Locacion WHERE Activo = 1 order by nombre";
            return ConectarAsync(async connection =>
                (await connection.QueryAsync<Locacion>(query)).ToList()
            );
        }

        public Task<Locacion?> ObtenerPorIdAsync(int idLocacion)
        {
            return ObtenerPorIdGenericoAsync<Locacion>("Locacion", "IdLocacion", idLocacion);
        }

        public Task<List<Locacion>> BuscarPorCriterioAsync(string criterio)
        {
            var query = "SELECT * FROM Locacion WHERE Activo = 1 AND (Nombre LIKE @Criterio OR Direccion LIKE @Criterio)";
            return ConectarAsync(async connection =>
                (await connection.QueryAsync<Locacion>(query, new { Criterio = $"%{criterio}%" })).ToList()
            );
        }

        public Task AgregarAsync(Locacion locacion)
        {
            var query = @"
            INSERT INTO Locacion (Nombre, Direccion, Carga, Descarga, Activo, Exportacion)
            VALUES (@Nombre, @Direccion, @Carga, @Descarga, @Activo, @Exportacion)";

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
            SET Nombre = @Nombre, Direccion = @Direccion, Carga = @Carga, Descarga = @Descarga, Activo = @Activo, Exportacion = @Exportacion
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

        public Task EliminarAsync(int idLocacion)
        {
            return EliminarGenéricoAsync<Locacion>("Locacion", idLocacion);
        }

        public async Task<List<LocacionSinonimo>> ObtenerTodosSinonimosAsync()
        {
            var query = "SELECT * FROM LocacionSinonimo where Activo = 1";
            return (await ConectarAsync(conn =>
                conn.QueryAsync<LocacionSinonimo>(query))).ToList();
        }

        public async Task AgregarSinonimoAsync(LocacionSinonimo sinonimo)
        {
            var query = "INSERT INTO LocacionSinonimo (IdLocacion, Sinonimo) VALUES (@IdLocacion, @Sinonimo)";
            await ConectarAsync(conn => conn.ExecuteAsync(query, sinonimo));
        }
    }
}