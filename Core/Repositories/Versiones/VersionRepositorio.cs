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
    public class VersionRepositorio : BaseRepositorio, IVersionRepositorio
    {
        public VersionRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<Versiones?> ObtenerVersionActivaAsync()
        {
            var query = "SELECT TOP 1 * FROM Versiones WHERE Activo = 1 ORDER BY FechaPublicacion DESC";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<Versiones>(query));
        }

        public async Task<Versiones?> ObtenerPorNumeroAsync(string numeroVersion)
        {
            var query = "SELECT * FROM Versiones WHERE NumeroVersion = @numeroVersion";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<Versiones>(query, new { numeroVersion }));
        }

        public async Task InsertarVersionAsync(Versiones version)
        {
            const string query = @"
            INSERT INTO Versiones (NumeroVersion, FechaPublicacion, Notas, Activo)
            VALUES (@NumeroVersion, @FechaPublicacion, @Notas, @Activo)";
            await ConectarAsync(conn => conn.ExecuteAsync(query, version));
        }

        public async Task ActualizarVersionAsync(Versiones version)
        {
            const string query = @"
            UPDATE Versiones
            SET NumeroVersion = @NumeroVersion,
                FechaPublicacion = @FechaPublicacion,
                Notas = @Notas,
                Activo = @Activo
            WHERE Id = @Id";
            await ConectarAsync(conn => conn.ExecuteAsync(query, version));
        }
    }

}
