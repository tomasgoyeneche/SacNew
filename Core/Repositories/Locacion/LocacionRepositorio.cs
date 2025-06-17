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
            return AgregarGenéricoAsync("Locacion", locacion);
        }

        public Task ActualizarAsync(Locacion locacion)
        {
            return ActualizarGenéricoAsync("Locacion", locacion);
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