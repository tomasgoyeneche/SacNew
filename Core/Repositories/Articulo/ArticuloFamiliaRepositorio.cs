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
    public class ArticuloFamiliaRepositorio : BaseRepositorio, IArticuloFamiliaRepositorio
    {
        public ArticuloFamiliaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<ArticuloFamilia>> ObtenerTodasAsync()
        {
            const string query = "SELECT * FROM ArticuloFamilia WHERE Activo = 1";
            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<ArticuloFamilia>(query);
                return result.ToList();
            });
        }

        public Task<ArticuloFamilia?> ObtenerPorIdAsync(int id) =>
            ObtenerPorIdGenericoAsync<ArticuloFamilia>("ArticuloFamilia", "IdArticuloFamilia", id);

        public Task<int> AgregarAsync(ArticuloFamilia familia) =>
            AgregarGenéricoAsync("ArticuloFamilia", familia);

        public Task<int> ActualizarAsync(ArticuloFamilia familia) =>
            ActualizarGenéricoAsync("ArticuloFamilia", familia);

        public Task<int> EliminarAsync(int id) =>
            EliminarGenéricoAsync<ArticuloFamilia>("ArticuloFamilia", id);
    }

}
