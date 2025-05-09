using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class PostaRepositorio : BaseRepositorio, IPostaRepositorio
    {
        public PostaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Posta>> ObtenerTodasLasPostasAsync()
        {
            var query = "SELECT * FROM Posta";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<Posta>(query).ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task<Posta?> ObtenerPorIdAsync(int idPosta)
        {
            return await ObtenerPorIdGenericoAsync<Posta>("Posta", "idPosta", idPosta);
        }

        public async Task<List<Posta>> BuscarPostasAsync(string textoBusqueda)
        {
            var query = "SELECT * FROM Posta WHERE Codigo LIKE @TextoBusqueda OR Descripcion LIKE @TextoBusqueda";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<Posta>(query, new { TextoBusqueda = $"%{textoBusqueda}%" })
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task AgregarPostaAsync(Posta nuevaPosta)
        {
            var query = @"
            INSERT INTO Posta (Codigo, Descripcion, Direccion, idProvincia)
            VALUES (@Codigo, @Descripcion, @Direccion, @idProvincia)";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, nuevaPosta);
            });
        }

        public async Task ActualizarPostaAsync(Posta postaActualizada)
        {
            var query = @"
            UPDATE Posta
            SET Codigo = @Codigo, Descripcion = @Descripcion, Direccion = @Direccion, idProvincia = @idProvincia
            WHERE IdPosta = @IdPosta";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, postaActualizada);
            });
        }

        public async Task EliminarPostaAsync(int idPosta)
        {
            var query = "UPDATE Posta SET Activo = 0 WHERE IdPosta = @IdPosta";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { IdPosta = idPosta });
            });
        }
    }
}