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


        public Task AgregarPostaAsync(Posta nuevaPosta)
        {
            return AgregarGenéricoAsync("Posta", nuevaPosta);
        }

        public Task ActualizarPostaAsync(Posta postaActualizada)
        {
            return ActualizarGenéricoAsync("Posta", postaActualizada);
        }


        public async Task EliminarPostaAsync(int idPosta)
        {
            var query = "DELETE FROM Posta WHERE IdPosta = @IdPosta";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { IdPosta = idPosta });
            });
        }
    }
}