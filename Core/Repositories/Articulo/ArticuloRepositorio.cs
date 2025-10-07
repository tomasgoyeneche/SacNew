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
    public class ArticuloRepositorio : BaseRepositorio, IArticuloRepositorio
    {
        public ArticuloRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        // 🔹 Obtener todos los artículos activos
        public async Task<List<Articulo>> ObtenerArticulosActivosAsync()
        {
            const string query = "SELECT * FROM Articulo WHERE Activo = 1";

            return await ConectarAsync(async connection =>
            {
                var result = await connection.QueryAsync<Articulo>(query);
                return result.ToList();
            });
        }

        // 🔹 Obtener un artículo por Id
        public async Task<Articulo?> ObtenerPorIdAsync(int idArticulo)
        {
            return await ObtenerPorIdGenericoAsync<Articulo>("Articulo", "IdArticulo", idArticulo);
        }

        // 🔹 Agregar un artículo y devolver el nuevo Id
        public async Task<int> AgregarArticuloAsync(Articulo nuevoArticulo)
        {
            return await AgregarGenéricoAsync("Articulo", nuevoArticulo);
        }

        // 🔹 Actualizar un artículo
        public Task<int> ActualizarArticuloAsync(Articulo articuloActualizado)
        {
            return ActualizarGenéricoAsync("Articulo", articuloActualizado);
        }

        // 🔹 Eliminar (soft delete → Activo = 0)
        public Task<int> EliminarArticuloAsync(int idArticulo)
        {
            return EliminarGenéricoAsync<Articulo>("Articulo", idArticulo);
        }
    }
}
