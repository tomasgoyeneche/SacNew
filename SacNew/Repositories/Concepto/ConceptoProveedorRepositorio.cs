using Dapper;
using SacNew.Models;
using SacNew.Services;

namespace SacNew.Repositories
{
    internal class ConceptoProveedorRepositorio : BaseRepositorio, IConceptoProveedorRepositorio
    {
        public ConceptoProveedorRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        { }

        public async Task<List<Proveedor>> ObtenerTodosLosProveedoresAsync()
        {
            var query = @"
        SELECT IdProveedor, Codigo, RazonSocial, NumeroFicha, Activo
        FROM ConceptoProveedor
        WHERE Activo = 1"; // Solo proveedores activos

            return await ConectarAsync(async connection =>
            {
                var proveedores = await connection.QueryAsync<Proveedor>(query);
                return proveedores.ToList(); // Convertimos el resultado a una lista
            });
        }
    }
}