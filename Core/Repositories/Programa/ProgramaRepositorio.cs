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
    internal class ProgramaRepositorio : BaseRepositorio, IProgramaRepositorio
    {
        public ProgramaRepositorio(ConnectionStrings connectionStrings, ISesionService sesionService)
            : base(connectionStrings, sesionService) { }

        public async Task<List<Ruteo>> ObtenerRuteoAsync()
        {
            var query = "SELECT * FROM vw_Ruteo";

            return await ConectarAsync(async connection =>
            {
                var ruteo = await connection.QueryAsync<Ruteo>(query);
                return ruteo.ToList();
            });
        }

        public async Task<List<RuteoResumen>> ObtenerResumenAsync()
        {
            var query = "SELECT * FROM vw_RuteoResumen";
            return await ConectarAsync(async connection =>
            {
                var resumen = await connection.QueryAsync<RuteoResumen>(query);
                return resumen.ToList();
            });
        }

        public async Task<Programa?> ObtenerPorIdAsync(int idPrograma)
        {
            return await ObtenerPorIdGenericoAsync<Programa>("Programa", "IdPrograma",idPrograma );
        }

        public async Task RegistrarProgramaAsync(int idPrograma, string motivo, string descripcion, int idUsuario)
        {
            var query = @"
        INSERT INTO ProgramaRegistro (IdPrograma, Motivo, Descripcion, IdUsuario, Fecha)
        VALUES (@IdPrograma, @Motivo, @Descripcion, @IdUsuario, GETDATE())";

            await ConectarAsync(async conn =>
            {
                await conn.ExecuteAsync(query, new
                {
                    IdPrograma = idPrograma,
                    Motivo = motivo,
                    Descripcion = descripcion,
                    IdUsuario = idUsuario
                });
            });
        }

        public async Task ActualizarFechaYRegistrarAsync(
        int idPrograma,
        string campo,           // Ej: "CargaLlegada"
        DateTime? fechaNueva,
        int idNomina,         // null si elimina
        int idUsuario)
        {
            // 1. Obtener programa actual antes de modificar
            Programa? programa = await ObtenerPorIdAsync(idPrograma);
            if (programa == null)
                throw new Exception("Programa no encontrado");

            // 2. Preparar motivo y descripción para la auditoría
            string motivo;
            string descripcion;

            if (fechaNueva == null)
            {
                motivo = $"Borró {campo}";
                descripcion = programa.GetType().GetProperty(campo)?.GetValue(programa)?.ToString() ?? "-";
            }
            else if (programa.GetType().GetProperty(campo)?.GetValue(programa) == null)
            {
                motivo = $"Agregó {campo}";
                descripcion = fechaNueva.Value.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                motivo = $"Editó {campo}";
                descripcion = fechaNueva.Value.ToString("dd/MM/yyyy HH:mm");
            }

            // 3. Determinar si también hay que actualizar el odómetro
            // Campos que tienen odómetro asociado
            var camposConOdometer = new Dictionary<string, string>
            {
                { "CargaLlegada", "CargaLlegadaOdometer" },
                { "CargaIngreso", "CargaIngresoOdometer" },
                { "CargaSalida", "CargaSalidaOdometer" },
                { "EntregaLlegada", "EntregaLlegadaOdometer" },
                { "EntregaIngreso", "EntregaIngresoOdometer" },
                { "EntregaSalida", "EntregaSalidaOdometer" }
            };

            // Preparar SQL dinámico
            string sql;
            object parametros;

            if (camposConOdometer.TryGetValue(campo, out string campoOdometer))
            {
                // Buscar el idNomina relacionado con el programa
                decimal? odometer = null;
                // Buscar el odometer más cercano a la fechaNueva (por ejemplo, el último registro antes o igual a esa fecha)
                if (fechaNueva.HasValue)
                {
                    odometer = await ObtenerOdometerPorNomina(idNomina);
                }

                sql = $@"UPDATE Programa 
                 SET {campo} = @fechaNueva, {campoOdometer} = @odometer 
                 WHERE IdPrograma = @idPrograma";

                parametros = new { fechaNueva, odometer, idPrograma };
            }
            else
            {
                sql = $"UPDATE Programa SET {campo} = @fechaNueva WHERE IdPrograma = @idPrograma";
                parametros = new { fechaNueva, idPrograma };
            }

            await ConectarAsync(conn => conn.ExecuteAsync(sql, parametros));

            // 4. Insertar registro en ProgramaRegistro
            var registro = new
            {
                IdPrograma = idPrograma,
                Motivo = motivo,
                Descripcion = descripcion,
                IdUsuario = idUsuario,
                Fecha = DateTime.Now
            };
                var insert = @"INSERT INTO ProgramaRegistro
            (IdPrograma, Motivo, Descripcion, IdUsuario, Fecha)
            VALUES (@IdPrograma, @Motivo, @Descripcion, @IdUsuario, @Fecha)";
            await ConectarAsync(conn => conn.ExecuteAsync(insert, registro));
        }


        public async Task ActualizarRutaRemitoAsync(int idPrograma, string campoRuta, string ruta)
        {
                var sql = $"UPDATE Programa SET {campoRuta} = @ruta WHERE IdPrograma = @idPrograma";
                await ConectarAsync(conn => conn.ExecuteAsync(sql, new { ruta, idPrograma }));
        }

        public async Task ActualizarCheck(int idPrograma, string campoCheck, string nombreUsuario)
        {
            var sql = $"UPDATE Programa SET {campoCheck} = @nombreUsuario WHERE IdPrograma = @idPrograma";
            await ConectarAsync(conn =>
                conn.ExecuteAsync(sql, new { nombreUsuario, idPrograma }));
        }

        private async Task<decimal?> ObtenerOdometerPorNomina(int idNomina)
        {
            // Busca el odometer más reciente ANTES o IGUAL a la fecha indicada
            var sql = @"
        SELECT TOP 1 odometer 
        FROM wsSitrackNomina 
        WHERE idNomina = @idNomina";
            return await ConectarAsync(async conn =>
                await conn.ExecuteScalarAsync<decimal?>(sql, new { idNomina})
            );
        }

        public Task ActualizarProgramaAsync(Programa programa)
        {
            return ActualizarGenéricoAsync("Programa", programa);
        }

        public async Task ActualizarProgramaOrigenProductoAsync(int idPrograma, int idOrigen, int idProducto)
        {
            var query = @"UPDATE Programa SET IdOrigen = @IdOrigen, IdProducto = @IdProducto WHERE IdPrograma = @IdPrograma";
            await ConectarAsync(conn => conn.ExecuteAsync(query, new { IdPrograma = idPrograma, IdOrigen = idOrigen, IdProducto = idProducto }));
        }

        public async Task ActualizarProgramaTramoDestinoAsync(int idPrograma, int idDestino)
        {
            var query = @"UPDATE ProgramaTramo SET IdDestino = @IdDestino WHERE IdPrograma = @IdPrograma";
            await ConectarAsync(conn => conn.ExecuteAsync(query, new { IdPrograma = idPrograma, IdDestino = idDestino }));
        }

        public async Task<int> InsertarProgramaRetornandoIdAsync(Programa programa)
        {
            var query = @"
        INSERT INTO Programa
        (IdDisponible, IdPedido, IdOrigen, IdProducto, Cupo, AlbaranDespacho, PedidoOr, Observaciones, IdProgramaEstado, FechaCarga, FechaEntrega)
        VALUES
        (@IdDisponible, @IdPedido, @IdOrigen, @IdProducto, @Cupo, @AlbaranDespacho, @PedidoOr, @Observaciones, @IdProgramaEstado, @FechaCarga, @FechaEntrega);
        SELECT CAST(SCOPE_IDENTITY() AS int);";
            return await ConectarAsync(conn => conn.ExecuteScalarAsync<int>(query, programa));
        }

        public async Task InsertarProgramaTramoAsync(ProgramaTramo tramo)
        {
            var query = @"INSERT INTO ProgramaTramo (IdPrograma, IdNomina, IdDestino, FechaInicio, FechaFin)
                  VALUES (@IdPrograma, @IdNomina, @IdDestino, @FechaInicio, @FechaFin)";
            await ConectarAsync(conn => conn.ExecuteAsync(query, tramo));
        }

        public async Task CerrarTramosActivosPorProgramaAsync(int idPrograma)
        {
            var query = @"
        UPDATE ProgramaTramo
        SET FechaFin = GETDATE()
        WHERE IdPrograma = @IdPrograma AND FechaFin IS NULL";
            await ConectarAsync(conn => conn.ExecuteAsync(query, new { IdPrograma = idPrograma }));
        }




        public async Task<List<ProgramaEstado>> ObtenerEstadosDeBajaAsync()
        {
            var query = "SELECT * FROM ProgramaEstado WHERE IdProgramaEstado >= 5 and Activo = 1";
            return (await ConectarAsync(conn => conn.QueryAsync<ProgramaEstado>(query))).ToList();
        }

        public async Task<ProgramaEstado?> ObtenerEstadoDeBajaPorIdAsync(int idMotivo)
        {
            var query = "SELECT * FROM ProgramaEstado WHERE IdProgramaEstado = @idMotivo";
            return await ConectarAsync(conn =>
                conn.QueryFirstOrDefaultAsync<ProgramaEstado>(query, new { idMotivo }));
        }


        public async Task<List<VistaPrograma>> ObtenerVistaProgramasAsync()
        {
            var query = "SELECT * FROM vw_Programa";
            return (await ConectarAsync(conn => conn.QueryAsync<VistaPrograma>(query))).ToList();
        }
    }
}
