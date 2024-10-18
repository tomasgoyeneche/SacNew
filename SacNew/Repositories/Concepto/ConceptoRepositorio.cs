using Dapper;
using SacNew.Models;
using SacNew.Services;

namespace SacNew.Repositories
{
    internal class ConceptoRepositorio : BaseRepositorio, IConceptoRepositorio
    {
        public ConceptoRepositorio(string connectionString, ISesionService sesionService)
            : base(connectionString, sesionService)
        { }

        public List<Concepto> ObtenerTodosLosConceptos()
        {
            var query = "SELECT * FROM Concepto WHERE Activo = 1";
            return Conectar(connection => connection.Query<Concepto>(query).ToList());
        }

        public async Task<IEnumerable<Concepto>> ObtenerPorTipoAsync(int idTipoConsumo)
        {
            var query = @"
            SELECT * 
            FROM Concepto 
            WHERE IdConsumoTipo = @IdTipoConsumo AND Activo = 1";

            return await ConectarAsync(async connection =>
            {
                var conceptos = await connection.QueryAsync<Concepto>(query, new { IdTipoConsumo = idTipoConsumo });
                return conceptos;
            });
        }

        public Concepto ObtenerPorId(int idConsumo)
        {
            var query = "SELECT * FROM Concepto WHERE IdConsumo = @IdConsumo";
            return Conectar(connection => connection.QueryFirstOrDefault<Concepto>(query, new { IdConsumo = idConsumo }));
        }

        public List<Concepto> BuscarConceptos(string textoBusqueda)
        {
            var query = "SELECT * FROM Concepto WHERE Codigo LIKE @Busqueda OR Descripcion LIKE @Busqueda";
            return Conectar(connection =>
                connection.Query<Concepto>(query, new { Busqueda = $"%{textoBusqueda}%" }).ToList());
        }

        public void AgregarConcepto(Concepto concepto)
        {
            var query = @"
            INSERT INTO Concepto (Codigo, Descripcion, idConsumoTipo, PrecioActual, Vigencia, PrecioAnterior, Activo, IdUsuario, FechaModificacion)
            VALUES (@Codigo, @Descripcion, @IdConsumoTipo, @PrecioActual, @Vigencia, @PrecioAnterior, @Activo, @IdUsuario, @FechaModificacion)";

            Conectar(connection => connection.Execute(query, concepto));
        }

        public void ActualizarConcepto(Concepto concepto)
        {
            var query = @"
            UPDATE Concepto
            SET Codigo = @Codigo, Descripcion = @Descripcion, idConsumoTipo = @idConsumoTipo, PrecioActual = @PrecioActual,
            Vigencia = @Vigencia, PrecioAnterior = @PrecioAnterior, Activo = @Activo, IdUsuario = @IdUsuario, FechaModificacion = @FechaModificacion
            WHERE IdConsumo = @IdConsumo";

            Conectar(connection => connection.Execute(query, concepto));
        }

        public void EliminarConcepto(int idConsumo)
        {
            var query = "UPDATE Concepto SET Activo = 0 WHERE IdConsumo = @IdConsumo";
            Conectar(connection => connection.Execute(query, new { IdConsumo = idConsumo }));
        }
    }
}