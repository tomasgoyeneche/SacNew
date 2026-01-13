using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;
using Shared.Models.RequerimientoCompra;

namespace Core.Repositories.RequerimientoCompra
{
    public class RcRepositorio : BaseRepositorio, IRcRepositorio
    {
        public RcRepositorio(ConnectionStrings cs, ISesionService sesion)
            : base(cs, sesion) { }

        public async Task<int> ObtenerProximoNumeroAsync()
        {
            const string sql = @"SELECT ISNULL(MAX(IdRc), 0) + 1 FROM Rc";

            return await ConectarAsyncFo(conn =>
                conn.ExecuteScalarAsync<int>(sql)
            );
        }

        public async Task<RcRcc?> ObtenerPorIdAsync(int idRc)
        {
            const string sql = @"SELECT * FROM Rc WHERE IdRc = @idRc";

            return await ConectarAsyncFo(conn =>
                conn.QueryFirstOrDefaultAsync<RcRcc>(sql, new { idRc })
            );
        }

        public async Task<int> AgregarAsync(RcRcc rc)
        {
            const string sql = @"
        INSERT INTO Rc
        (Fecha, IdProveedor, Emitido, Aprobado, EntregaLugar, EntregaFecha,
         Importe, CondicionPago, Observaciones, IdEstado)
        VALUES
        (@Fecha, @IdProveedor, @Emitido, @Aprobado, @EntregaLugar, @EntregaFecha,
         @Importe, @CondicionPago, @Observaciones, @IdEstado);

        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await ConectarAsyncFo(conn =>
                conn.ExecuteScalarAsync<int>(sql, rc)
            );
        }

        public async Task ActualizarAsync(RcRcc rc)
        {
            const string sql = @"
        UPDATE Rc SET
            Fecha = @Fecha,
            IdProveedor = @IdProveedor,
            Emitido = @Emitido,
            Aprobado = @Aprobado,
            EntregaLugar = @EntregaLugar,
            EntregaFecha = @EntregaFecha,
            Importe = @Importe,
            CondicionPago = @CondicionPago,
            Observaciones = @Observaciones,
            IdEstado = @IdEstado
        WHERE IdRc = @IdRc";

            await ConectarAsyncFo(conn =>
                conn.ExecuteAsync(sql, rc)
            );
        }

        public async Task InsertarRcDetalleAsync(int idRc, string descripcion, int cantidad)
        {
            var query = @"
        INSERT INTO RcDetalle (IdRc, Descripcion, Cantidad)
        VALUES (@IdRc, @Descripcion, @Cantidad)";

            await ConectarAsyncFo(async conn =>
            {
                await conn.ExecuteAsync(query, new { IdRc = idRc, Descripcion = descripcion, Cantidad = cantidad });
            });
        }

        public async Task InsertarRcImputacionAsync(int idRc, int idImputacion, int porcentaje)
        {
            var query = @"
        INSERT INTO RcImputacionDependencia (IdRc, IdImputacionDependencia, Porcentaje)
        VALUES (@IdRc, @IdImputacion, @Porcentaje)";

            await ConectarAsyncFo(async conn =>
            {
                await conn.ExecuteAsync(query, new { IdRc = idRc, IdImputacion = idImputacion, Porcentaje = porcentaje });
            });
        }

        public async Task<List<Dependencia>> ObtenerDependenciasAsync()
        {
            var query = "SELECT IdDependencia, Descripcion FROM Dependencia WHERE Activo = 1";
            return await ConectarAsyncFo(async conn =>
            {
                var result = await conn.QueryAsync<Dependencia>(query);
                return result.ToList();
            });
        }

        public async Task<List<Imputacion>> ObtenerImputacionesPorDependenciaAsync(int idDependencia)
        {
            var query = @"
        SELECT id.idImputacionDependencia as idImputacion, i.Descripcion
        FROM Imputacion i
        INNER JOIN ImputacionDependencia id ON id.IdImputacion = i.IdImputacion
        WHERE id.IdDependencia = @idDependencia AND id.Activo = 1";

            return await ConectarAsyncFo(async conn =>
            {
                var result = await conn.QueryAsync<Imputacion>(query, new { IdDependencia = idDependencia });
                return result.ToList();
            });
        }

        public async Task<RcDetalleDto?> ObtenerDetalleRcAsync(int idRc)
        {
            return await ConectarAsyncFo(async conn =>
            {
                var query = @"
                SELECT * from vw_RcDetalle
                WHERE IdRc = @IdRc";

                return await conn.QueryFirstOrDefaultAsync<RcDetalleDto>(query, new { IdRc = idRc });
            });
        }

        public async Task<List<RcImputacionDetalleDto>> ObtenerImputacionesRcAsync(int idRc)
        {
            return await ConectarAsyncFo(async conn =>
            {
                var query = @"
                SELECT * from vw_RcImputacionDetalle where IdRc = @IdRc";

                return (await conn.QueryAsync<RcImputacionDetalleDto>(query, new { IdRc = idRc })).ToList();
            });
        }

        public async Task<List<RcDetalleRcc>> ObtenerDescripcionesRcAsync(int idRc)
        {
            return await ConectarAsyncFo(async conn =>
            {
                var query = @"
                SELECT IdRcDetalle, IdRc, Descripcion, Cantidad, Activo
                FROM RcDetalle
                WHERE IdRc = @IdRc";

                return (await conn.QueryAsync<RcDetalleRcc>(query, new { IdRc = idRc })).ToList();
            });
        }
    }
}