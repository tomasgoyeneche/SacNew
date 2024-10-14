using Dapper;
using SacNew.Models;
using SacNew.Services;
using System.Configuration;
using System.Data.SqlClient;

namespace SacNew.Repositories
{
    internal class ConceptoProveedorRepositorio : BaseRepositorio, IConceptoProveedorRepositorio
    {
        public ConceptoProveedorRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        { }

        public List<Proveedor> ObtenerTodosLosProveedores()
        {
            var query = @"
        SELECT IdProveedor, Codigo, RazonSocial, NumeroFicha, Activo 
        FROM ConceptoProveedor 
        WHERE Activo = 1";  // Solo proveedores activos

            return Conectar(connection =>
            {
                var proveedores = connection.Query<Proveedor>(query).ToList();
                return proveedores;
            });
        }
    }
}