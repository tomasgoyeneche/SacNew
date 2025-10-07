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
    public class ArticuloMarcaRepositorio : BaseRepositorio, IArticuloMarcaRepositorio
    {
        public ArticuloMarcaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<ArticuloMarca>> ObtenerTodasAsync()
        {
            const string query = "SELECT * FROM ArticuloMarca WHERE Activo = 1";
            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<ArticuloMarca>(query);
                return result.ToList();
            });
        }

        public Task<ArticuloMarca?> ObtenerPorIdAsync(int id) =>
            ObtenerPorIdGenericoAsync<ArticuloMarca>("ArticuloMarca", "IdArticuloMarca", id);

        public Task<int> AgregarAsync(ArticuloMarca marca) =>
            AgregarGenéricoAsync("ArticuloMarca", marca);

        public Task<int> ActualizarAsync(ArticuloMarca marca) =>
            ActualizarGenéricoAsync("ArticuloMarca", marca);

        public Task<int> EliminarAsync(int id) =>
            EliminarGenéricoAsync<ArticuloMarca>("ArticuloMarca", id);
    }
}
