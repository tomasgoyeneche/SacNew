using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    internal class CiaRepositorio : BaseRepositorio, ICiaRepositorio
    {
        public CiaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        // Metodos de busqueda general o por id
        public async Task<List<Cia>> ObtenerTodasAsync()
        {
            var query = "SELECT IdCia, NombreFantasia FROM Cia WHERE Activo = 1";
            return await ConectarAsync(async connection =>
            {
                return (await connection.QueryAsync<Cia>(query)).ToList();
            });
        }

        // Metodos de busqueda especificos
        public async Task<List<Cia>> ObtenerPorTipoAsync(int idEmpresaSeguroEntidad)
        {
            var query = @"SELECT idCia, NombreFantasia, idEmpresaSeguroEntidad
                           FROM Cia 
                           WHERE Activo = 1 AND idEmpresaSeguroEntidad = @idEmpresaSeguroEntidad
                           ORDER BY NombreFantasia";
            return (await ConectarAsync(conn =>
                conn.QueryAsync<Cia>(query, new { idEmpresaSeguroEntidad })
            )).ToList();
        }
    }
}