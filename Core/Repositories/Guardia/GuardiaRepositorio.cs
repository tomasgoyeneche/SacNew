using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class GuardiaRepositorio : BaseRepositorio, IGuardiaRepositorio
    {
        public GuardiaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<int> RegistrarIngresoAsync(GuardiaIngreso ingreso, int IdUsuario, string Observacion)
        {
            int idPoc = 0;

            await ConectarAsync(async connection =>
            {
                using var transaction = connection.BeginTransaction();

                try
                {
                    string insertIngreso = @"
                    INSERT INTO GuardiaIngreso (IdPosta, TipoIngreso, IdNomina, IdTe, IdGuardiaIngresoOtros, IdGuardiaEstado, FechaIngreso, Activo)
                    VALUES (@IdPosta, @TipoIngreso, @IdNomina, @IdTe, @IdGuardiaIngresoOtros, @IdGuardiaEstado, @FechaIngreso, @Activo);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    idPoc = await connection.ExecuteScalarAsync<int>(insertIngreso, ingreso, transaction);

                    var registro = new GuardiaRegistro
                    {
                        IdGuardiaIngreso = idPoc,
                        IdGuardiaEstado = ingreso.IdGuardiaEstado,
                        Observaciones = Observacion, // Se puede completar luego
                        IdUsuario = IdUsuario,
                        FechaGuardia = ingreso.FechaIngreso,
                        Activo = true
                    };

                    string insertRegistro = @"
                    INSERT INTO GuardiaRegistro (IdGuardiaIngreso, IdGuardiaEstado, Observaciones, IdUsuario, FechaGuardia, Activo)
                    VALUES (@IdGuardiaIngreso, @IdGuardiaEstado, @Observaciones, @IdUsuario, @FechaGuardia, @Activo);";

                    await connection.ExecuteAsync(insertRegistro, registro, transaction);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            });
            return idPoc;
        }

        public async Task RegistrarSalidaAsync(int idGuardiaIngreso, int idUsuario, DateTime fecha, string observacion)
        {
            await ConectarAsync(async conn =>
            {
                using var tran = conn.BeginTransaction();

                // Actualizar ingreso
                string update = "UPDATE GuardiaIngreso SET IdGuardiaEstado = 10 WHERE IdGuardiaIngreso = @id";
                await conn.ExecuteAsync(update, new { id = idGuardiaIngreso }, tran);

                // Insertar registro
                string insert = @"
            INSERT INTO GuardiaRegistro (IdGuardiaIngreso, IdGuardiaEstado, Observaciones, IdUsuario, FechaGuardia, Activo)
            VALUES (@IdGuardiaIngreso, @IdEstado, @Obs, @UserId, @Fecha, 1)";
                await conn.ExecuteAsync(insert, new
                {
                    IdGuardiaIngreso = idGuardiaIngreso,
                    IdEstado = 10,
                    Obs = observacion,
                    UserId = idUsuario,
                    Fecha = fecha
                }, tran);

                tran.Commit();
            });
        }

        public async Task<int> RegistrarIngresoTransitoEspecialAsync(TransitoEspecial te, int idPosta, DateTime fechaIngreso, int idUsuario)
        {
            int idIngreso = 0;

            await ConectarAsync(async conn =>
            {
                using var tran = conn.BeginTransaction();

                try
                {
                    // 1. Insertar TransitoEspecial
                    string insertTe = @"
                INSERT INTO TE (RazonSocial, Cuit, Apellido, Nombre, Documento, Licencia, Art, Tractor, Semi, Seguro, Zona, Activo)
                VALUES (@RazonSocial, @Cuit, @Apellido, @Nombre, @Documento, @Licencia, @Art, @Tractor, @Semi, @Seguro, @Zona, @Activo);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    int idTe = await conn.ExecuteScalarAsync<int>(insertTe, te, tran);

                    // 2. Insertar GuardiaIngreso
                    var ingreso = new GuardiaIngreso
                    {
                        IdPosta = idPosta,
                        TipoIngreso = 2,
                        IdTe = idTe,
                        IdNomina = null,
                        IdGuardiaIngresoOtros = null,
                        IdGuardiaEstado = 1,
                        FechaIngreso = fechaIngreso,
                        Activo = true
                    };

                    string insertIngreso = @"
                INSERT INTO GuardiaIngreso (IdPosta, TipoIngreso, IdNomina, IdTe, IdGuardiaIngresoOtros, IdGuardiaEstado, FechaIngreso, Activo)
                VALUES (@IdPosta, @TipoIngreso, @IdNomina, @IdTe, @IdGuardiaIngresoOtros, @IdGuardiaEstado, @FechaIngreso, @Activo);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    idIngreso = await conn.ExecuteScalarAsync<int>(insertIngreso, ingreso, tran);

                    // 3. Insertar GuardiaRegistro
                    var registro = new GuardiaRegistro
                    {
                        IdGuardiaIngreso = idIngreso,
                        IdGuardiaEstado = 1,
                        Observaciones = "Ingreso - TE",
                        IdUsuario = idUsuario,
                        FechaGuardia = fechaIngreso,
                        Activo = true
                    };

                    string insertRegistro = @"
                INSERT INTO GuardiaRegistro (IdGuardiaIngreso, IdGuardiaEstado, Observaciones, IdUsuario, FechaGuardia, Activo)
                VALUES (@IdGuardiaIngreso, @IdGuardiaEstado, @Observaciones, @IdUsuario, @FechaGuardia, @Activo);";

                    await conn.ExecuteAsync(insertRegistro, registro, tran);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            });

            return idIngreso;
        }

        public async Task RegistrarCambioEstadoAsync(int idGuardiaIngreso, int idUsuario, DateTime fecha, int nuevoEstado, string observacion)
        {
            await ConectarAsync(async conn =>
            {
                using var tran = conn.BeginTransaction();

                // Actualizar el estado en GuardiaIngreso
                const string update = @"UPDATE GuardiaIngreso
                                SET IdGuardiaEstado = @IdEstado
                                WHERE IdGuardiaIngreso = @IdIngreso";

                await conn.ExecuteAsync(update, new
                {
                    IdEstado = nuevoEstado,
                    IdIngreso = idGuardiaIngreso
                }, tran);

                // Insertar el nuevo registro en GuardiaRegistro
                const string insert = @"INSERT INTO GuardiaRegistro
                                (IdGuardiaIngreso, IdGuardiaEstado, Observaciones, IdUsuario, FechaGuardia, Activo)
                                VALUES
                                (@IdIngreso, @IdEstado, @Observacion, @IdUsuario, @Fecha, 1)";

                await conn.ExecuteAsync(insert, new
                {
                    IdIngreso = idGuardiaIngreso,
                    IdEstado = nuevoEstado,
                    Observacion = observacion,
                    IdUsuario = idUsuario,
                    Fecha = fecha
                }, tran);

                tran.Commit();
            });
        }

        public async Task EliminarRegistroAsync(int idRegistro)
        {
            const string sql = @"UPDATE GuardiaRegistro SET Activo = 0 WHERE IdGuardiaRegistro = @id";
            await ConectarAsync(conn => conn.ExecuteAsync(sql, new { id = idRegistro }));
        }

        public async Task ActualizarEstadoIngresoAsync(int idIngreso, int nuevoEstado)
        {
            const string sql = @"UPDATE GuardiaIngreso SET IdGuardiaEstado = @estado WHERE IdGuardiaIngreso = @id";
            await ConectarAsync(conn => conn.ExecuteAsync(sql, new { estado = nuevoEstado, id = idIngreso }));
        }

        public async Task EliminarIngresoCompletoAsync(int idIngreso)
        {
            await ConectarAsync(async conn =>
            {
                using var tran = conn.BeginTransaction();

                await conn.ExecuteAsync("UPDATE GuardiaRegistro SET Activo = 0 WHERE IdGuardiaIngreso = @id", new { id = idIngreso }, tran);
                await conn.ExecuteAsync("UPDATE GuardiaIngreso SET Activo = 0 WHERE IdGuardiaIngreso = @id", new { id = idIngreso }, tran);

                tran.Commit();
            });
        }

        public async Task<List<GuardiaDto>> ObtenerGuardiasPorPostaAsync(int idPosta)
        {
            const string query = "SELECT * FROM vw_Guardia where idPosta = @idPosta";
            return await ConectarAsync(conn =>
                conn.QueryAsync<GuardiaDto>(query, new { idPosta }))
                .ContinueWith(t => t.Result.ToList());
        }

        public async Task<GuardiaEstado?> ObtenerEstadoPorIdAsync(int idEstado)
        {
            const string query = "SELECT * FROM GuardiaEstado WHERE IdGuardiaEstado = @idEstado";

            return await ConectarAsync(conn =>
                conn.QueryFirstOrDefaultAsync<GuardiaEstado>(query, new { idEstado })
            );
        }

        public async Task<List<GuardiaHistorialDto>> ObtenerHistorialPorIngresoAsync(int idGuardiaIngreso)
        {
            const string query = "SELECT * FROM vw_GuardiaHistorial WHERE IdGuardiaIngreso = @idGuardiaIngreso  order by fechaGuardia desc";

            return await ConectarAsync(conn =>
                conn.QueryAsync<GuardiaHistorialDto>(query, new { idGuardiaIngreso }))
                .ContinueWith(t => t.Result.ToList());
        }

        public async Task<(int unidades, int tractores, int semis, int choferes)> ObtenerResumenEnParadorAsync(int idPosta)
        {
            var guardias = await ConectarAsync(conn =>
                conn.QueryAsync<GuardiaDto>("SELECT * FROM vw_Guardia where idPosta = @idPosta", new { idPosta })
            );

            int unidades = 0, tractores = 0, semis = 0, choferes = 0;

            foreach (var guardia in guardias)
            {
                List<GuardiaHistorialDto> historial = await ObtenerHistorialPorIngresoAsync(guardia.IdGuardiaIngreso);
                List<GuardiaHistorialDto> eventos = historial
                    .OrderBy(h => h.FechaGuardia)
                    .ToList();

                bool choferActivo = true;
                bool tractorActivo = true;
                bool semiActivo = true;

                for (int i = 0; i < eventos.Count; i++)
                {
                    GuardiaHistorialDto ev = eventos[i];

                    switch (ev.IdGuardiaEstado)
                    {
                        case 2: // salida tractor
                            choferActivo = false;
                            tractorActivo = false;
                            break;

                        case 3: // salida chofer
                            choferActivo = false;
                            break;

                        case 5: // salida completa
                            choferActivo = false;
                            tractorActivo = false;
                            semiActivo = false;
                            break;

                        case 6: // reingreso
                            choferActivo = true;
                            tractorActivo = true;
                            semiActivo = true;
                            break;
                    }
                }

                switch (guardia.TipoIngreso)
                {
                    case 1: // Nómina
                        if (tractorActivo && choferActivo) unidades++;
                        if (tractorActivo) tractores++;
                        if (semiActivo) semis++;
                        if (choferActivo) choferes++;
                        break;

                    case 2: // Tránsito Especial
                        if (tractorActivo && choferActivo) unidades++;
                        if (tractorActivo) tractores++;
                        if (semiActivo) semis++;
                        if (choferActivo) choferes++;
                        break;

                    case 3: // Otros
                        if (choferActivo) choferes++;
                        break;
                }
            }

            return (unidades, tractores, semis, choferes);
        }

        public async Task<bool> EstaEnParadorAsync(string patente)
        {
            const string query = @"
        SELECT COUNT(*) FROM vw_Guardia
        WHERE  Tractor = @patente";

            var count = await ConectarAsync(conn =>
                conn.ExecuteScalarAsync<int>(query, new { patente }));

            return count > 0;
        }
    }
}