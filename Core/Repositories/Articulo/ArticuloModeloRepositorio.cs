using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ArticuloModeloRepositorio : BaseRepositorio, IArticuloModeloRepositorio
    {
        public ArticuloModeloRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<ArticuloModelo>> ObtenerTodasAsync()
        {
            const string query = "SELECT * FROM ArticuloModelo WHERE Activo = 1";
            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<ArticuloModelo>(query);
                return result.ToList();
            });
        }

        public async Task<List<ArticuloModelo>> ObtenerPorMarcaAsync(int idMarca)
        {
            const string query = "SELECT * FROM ArticuloModelo WHERE IdArticuloMarca = @idMarca AND Activo = 1";
            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<ArticuloModelo>(query, new { idMarca });
                return result.ToList();
            });
        }

        public Task<ArticuloModelo?> ObtenerPorIdAsync(int id) =>
            ObtenerPorIdGenericoAsync<ArticuloModelo>("ArticuloModelo", "IdArticuloModelo", id);

        public Task<int> AgregarAsync(ArticuloModelo modelo) =>
            AgregarGenéricoAsync("ArticuloModelo", modelo);

        public Task<int> ActualizarAsync(ArticuloModelo modelo) =>
            ActualizarGenéricoAsync("ArticuloModelo", modelo);

        public Task<int> EliminarAsync(int id) =>
            EliminarGenéricoAsync<ArticuloModelo>("ArticuloModelo", id);
    }
}
