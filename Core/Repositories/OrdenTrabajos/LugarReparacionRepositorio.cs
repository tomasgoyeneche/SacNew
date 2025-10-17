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
    public class LugarReparacionRepositorio : BaseRepositorio, ILugarReparacionRepositorio
    {
        private const string Tabla = "LugarReparacion";

        public LugarReparacionRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> AgregarAsync(LugarReparacion entidad)
            => await AgregarGenéricoAsync(Tabla, entidad);

        public async Task<int> ActualizarAsync(LugarReparacion entidad)
            => await ActualizarGenéricoAsync(Tabla, entidad);

        public async Task<int> EliminarAsync(int id)
            => await EliminarGenéricoAsync<LugarReparacion>(Tabla, id);

        public async Task<LugarReparacion?> ObtenerPorIdAsync(int id)
            => await ObtenerPorIdGenericoAsync<LugarReparacion>(Tabla, "IdLugarReparacion", id);

        public async Task<List<LugarReparacion>> ObtenerTodosAsync()
        {
            const string query = "SELECT * FROM LugarReparacion WHERE Activo = 1 ORDER BY Nombre;";
            return await ConectarAsync(async conn =>
            {
                var result = await conn.QueryAsync<LugarReparacion>(query);
                return result.ToList();
            });
        }
    }
}
