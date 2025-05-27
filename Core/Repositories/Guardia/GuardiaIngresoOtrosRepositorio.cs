using Core.Base;
using Core.Services;
using Dapper;
using Shared.Models;

namespace Core.Repositories
{
    public class GuardiaIngresoOtrosRepositorio : BaseRepositorio, IGuardiaIngresoOtrosRepositorio
    {
        public GuardiaIngresoOtrosRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<GuardiaIngresoOtros?> ObtenerPorIdAsync(int id)
        {
            const string query = "SELECT * FROM GuardiaIngresoOtros WHERE IdGuardiaIngresoOtros = @id";
            return await ConectarAsync(conn =>
                conn.QueryFirstOrDefaultAsync<GuardiaIngresoOtros>(query, new { id }));
        }

        public async Task<int> RegistrarIngresoOtrosAsync(GuardiaIngresoOtros ingresoOtros, int idPosta, DateTime fechaIngreso, int idUsuario)
        {
            int numeroPoc = 0;

            await ConectarAsync(async conn =>
            {
                using var tran = conn.BeginTransaction();

                try
                {
                    // 1. Insertar TransitoEspecial
                    string insertIngresoOtros = @"
                INSERT INTO GuardiaIngresoOtros (Apellido, Nombre, Documento, Licencia, Art, Patente, Empresa, Observaciones, Tipo, Activo)
                VALUES (@Apellido, @Nombre, @Documento, @Licencia, @Art, @Patente, @Empresa, @Observaciones, @Tipo, @Activo);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    int idIngresoOtros = await conn.ExecuteScalarAsync<int>(insertIngresoOtros, ingresoOtros, tran);

                    // 2. Insertar GuardiaIngreso
                    var ingreso = new GuardiaIngreso
                    {
                        IdPosta = idPosta,
                        TipoIngreso = 3,
                        IdTe = null,
                        IdNomina = null,
                        IdGuardiaIngresoOtros = idIngresoOtros,
                        IdGuardiaEstado = 1,
                        FechaIngreso = fechaIngreso,
                        Activo = true
                    };

                    string insertIngreso = @"
                INSERT INTO GuardiaIngreso (IdPosta, TipoIngreso, IdNomina, IdTe, IdGuardiaIngresoOtros, IdGuardiaEstado, FechaIngreso, Activo)
                VALUES (@IdPosta, @TipoIngreso, @IdNomina, @IdTe, @IdGuardiaIngresoOtros, @IdGuardiaEstado, @FechaIngreso, @Activo);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    numeroPoc = await conn.ExecuteScalarAsync<int>(insertIngreso, ingreso, tran);

                    // 3. Insertar GuardiaRegistro
                    var registro = new GuardiaRegistro
                    {
                        IdGuardiaIngreso = numeroPoc,
                        IdGuardiaEstado = 1,
                        Observaciones = "Ingreso - Otros",
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

            return numeroPoc;
        }
    }
}