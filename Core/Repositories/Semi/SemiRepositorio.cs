using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class SemiRepositorio : BaseRepositorio, ISemiRepositorio
    {
        public SemiRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
           : base(connectionStrings, sesionService) { }

        public async Task<SemiDto> ObtenerPorIdDtoAsync(int idSemi)
        {
            var query = "SELECT * FROM vw_SemiremolquesDetalles WHERE idSemi = @idSemi";
            return await ConectarAsync(conn => conn.QueryFirstOrDefaultAsync<SemiDto>(query, new { IdSemi = idSemi }));
        }

        public async Task<List<SemiDto>> ObtenerTodosLosSemisDto()
        {
            var query = "SELECT * FROM vw_SemiremolquesDetalles order by patente";

            return await ConectarAsync(async connection =>
            {
                var tractores = await connection.QueryAsync<SemiDto>(query);
                return tractores.ToList();
            });
        }

        public async Task EliminarSemiAsync(int idSemi)
        {
            var query = "Update Semi set Activo = 0 WHERE idSemi = @idSemi";

            await ConectarAsync(connection =>
            {
                return connection.ExecuteAsync(query, new { IdSemi = idSemi });
            });
        }

        public async Task<List<SemiDto>> BuscarSemisAsync(string textoBusqueda)
        {
            var query = "SELECT * FROM vw_SemiremolquesDetalles WHERE Patente LIKE @TextoBusqueda OR Empresa_Nombre LIKE @TextoBusqueda order by patente";

            return await ConectarAsync(connection =>
            {
                return connection.QueryAsync<SemiDto>(query, new { TextoBusqueda = $"%{textoBusqueda}%" })
                                 .ContinueWith(task => task.Result.ToList());
            });
        }

        public async Task<Semi?> ObtenerSemiPorIdAsync(int idSemi)
        {
            var query = @"
            select * from Semi WHERE idSemi = @idSemi";

            return await ConectarAsync(async conn =>
            {
                return await conn.QueryFirstOrDefaultAsync<Semi>(query, new { IdSemi = idSemi });
            });
        }

        public async Task ActualizarSemiAsync(Semi semi)
        {
            var query = @"
            UPDATE Semi
            SET Patente = @Patente, Anio = @Anio, IdMarca = @IdMarca, IdModelo = @IdModelo,
                Tara = @Tara, FechaAlta = @FechaAlta, IdTipoCarga = @IdTipoCarga, Compartimientos = @Compartimientos, IdMaterial = @IdMaterial,
                CertificadoCompatibilidad = @CertificadoCompatibilidad, Cubicacion = @Cubicacion, Configuracion = @Configuracion, Espesor = @Espesor, Inv = @Inv
            WHERE IdSemi = @IdSemi";

            await ConectarAsync(async conn =>
            {
                await conn.ExecuteAsync(query, semi);
            });
        }

        public async Task ActualizarEmpresaSemiAsync(int idSemi, int idEmpresa)
        {
            const string query = "UPDATE Semi SET IdEmpresa = @idEmpresa WHERE IdSemi = @idSemi";

            await ConectarAsync(async connection =>
            {
                await connection.ExecuteAsync(query, new { idSemi, idEmpresa });
            });
        }
    }
}