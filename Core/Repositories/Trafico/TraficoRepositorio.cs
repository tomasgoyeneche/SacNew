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
    public class TraficoRepositorio : BaseRepositorio, ITraficoRepositorio
    {
        public TraficoRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public Task<Trafico?> ObtenerPorIdAsync(int idTrafico)
        {
            return ObtenerPorIdGenericoAsync<Trafico>("Trafico", "IdTrafico", idTrafico);
        }

        public async Task<List<Trafico>> ObtenerTodosAsync()
        {
            const string query = "SELECT * FROM Trafico WHERE Activo = 1 ORDER BY Nombre";

            return await ConectarAsync(async connection =>
            {
                var resultado = await connection.QueryAsync<Trafico>(query);
                return resultado.ToList();
            });
        }

        public async Task<List<Trafico>> BuscarAsync(string textoBusqueda)
        {
            const string query = @"
            SELECT * 
            FROM Trafico 
            WHERE Nombre LIKE @TextoBusqueda
              AND Activo = 1
            ORDER BY Nombre";

            return await ConectarAsync(async connection =>
            {
                var resultado = await connection.QueryAsync<Trafico>(
                    query,
                    new { TextoBusqueda = $"%{textoBusqueda}%" });

                return resultado.ToList();
            });
        }

        public async Task AgregarAsync(Trafico trafico)
        {
            const string query = @"
            INSERT INTO Trafico (Nombre, Activo)
            VALUES (@Nombre, 1)";

            await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, trafico),
                "Trafico",
                "INSERT",
                null,
                trafico
            );
        }

        public Task ActualizarAsync(Trafico trafico)
        {
            return ActualizarGenéricoAsync("Trafico", trafico);
        }

        public async Task EliminarAsync(int idTrafico)
        {
            const string query = @"
            UPDATE Trafico
            SET Activo = 0
            WHERE IdTrafico = @IdTrafico";

            await EjecutarConAuditoriaAsync(
                conn => conn.ExecuteAsync(query, new { IdTrafico = idTrafico }),
                "Trafico",
                "DELETE (LOGICO)",
                new { IdTrafico = idTrafico },
                new { Activo = 0 }
            );
        }
    }
}
